
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace SnakeProjekt
{
	public class Engine
	{
		MainForm Form = new MainForm();
		Timer Timer = new Timer();
		const int FPS = 10;

		bool running = false;
		bool gameOver = false;
		public bool Running { get => running; set => running = value;}
		public bool GameOver { get => gameOver; set => gameOver = value;}

		public static Player Player1, Player2;
		public static LinkedList<Food> FoodList = new LinkedList<Food>();

		 // Create font and brush.
		Font drawFont = new Font("Times New Roman", 16);
		SolidBrush drawBrush = new SolidBrush(Color.White);

		public Engine()
        {
			Player1 = new Player(PlayerColor.Blue, this);
			Player2 = new Player(PlayerColor.Green, this);
        }


		public void Run()
		{
			Form.Paint += Render;
			Timer.Tick += TimerEventHandler;
			Form.KeyDown += OnKeyDown;
			Timer.Interval = 1000 / FPS;
			Timer.Start();
			CreatFood();
			
			Application.Run(Form);

		}

		void TimerEventHandler(Object obj, EventArgs args)
		{
			Tick();
			Form.Refresh();
		}

		void Render(Object obj, PaintEventArgs args)
		{
			Player1.Render(args.Graphics);
			if(Player2 != null) {
				Player2.Render(args.Graphics);
			}

			if(!running)
            {
				if(GameOver) 
				{ 
					if(Player1.Score == Player2.Score)
                    {
						args.Graphics.DrawString("Tie!", drawFont, drawBrush, 320, 10);
                    }
					else if(Player1.Score > Player2.Score)
                    {
						args.Graphics.DrawString("Blue Player is Victorious!", drawFont, drawBrush, 300, 10);
                    }
					if(Player1.Score < Player2.Score)
                    {
						args.Graphics.DrawString("Green Player is Victorious!", drawFont, drawBrush, 300, 10);
                    }
					args.Graphics.DrawString("Press F to continue", drawFont, drawBrush, 305, 30);
				}
				else 
				{ 
				args.Graphics.DrawString("Press F to run", drawFont, drawBrush, 305, 10);
				args.Graphics.DrawString("Controls: WASD and IJKL", drawFont, drawBrush, 265, 30);
				args.Graphics.DrawString("Press T to toggle number of players", drawFont, drawBrush, 265, 50);
				//args.Graphics.DrawString($"W:{Form.Size.Width} H:{Form.Size.Height}", drawFont, drawBrush, 300, 50);
				}

            }
			
			if(Running) { 
			foreach(Food food in FoodList)
            {
				food.Render(args.Graphics);
            }
				drawBrush.Color = Color.Blue;
				args.Graphics.DrawString($"{Player1.Score}", drawFont, drawBrush, 5, 5);

				if(Player2  != null) { 
				drawBrush.Color = Color.Green;
				args.Graphics.DrawString($"{Player2.Score}", drawFont, drawBrush, 759, 5);
				}
				drawBrush.Color = Color.White;
			}

		}
		
void CreatFood()
		{
			for (int i = FoodList.Count; i < 15; i++)
			{

				Random r = new Random();


				int maxWidth = Form.Width % 25;
				int maxHeight = Form.Height % 25;
				maxWidth = Form.Width - maxWidth;
				maxHeight = Form.Height - maxHeight;

				float x;
				float y;
				bool newplace = false;

				if (FoodList.Count != 0)
				{
					do
					{
						y = r.Next((maxHeight / 25) + 1) * 25;
						x = r.Next((maxWidth / 25) + 1) * 25;

						foreach (Food food in FoodList)
						{
							if (food.x == x && food.y == y)
							{
								newplace = false;
								break;
							}

							else
								newplace = true;
						}

					} while (newplace == false);
				}

                else
                {
					y = r.Next((maxHeight / 25) + 1) * 25;
					x = r.Next((maxWidth / 25) + 1) * 25;
				}

				int type = r.Next(10);

				if (type < 8)
				{
					Standard food = new Standard();
					food.x = x;
					food.y = y;
					food.point = 1;
					food.lengthAdd = 1;
					FoodList.AddFirst(food);
				}

				else if (type >= 8 && type < 9)
				{
					Valuable food = new Valuable();
					food.x = x;
					food.y = y;
					food.point = 5;
					food.lengthAdd = 2;
					FoodList.AddFirst(food);
				}

				else if (type == 9)
				{
					Diet food = new Diet();
					food.x = x;
					food.y = y;
					food.point = 1;
					food.lengthAdd = -1;
					FoodList.AddFirst(food);
				}
			}
		}
		
		void Tick()
		{
			if(Running) { 
			Player1.Tick();
			if(Player2 != null)
				Player2.Tick();

			foreach (Food food in FoodList)
            {
				food.Tick();
            }
				
			if (FoodList.Count < 15)
				CreatFood();
			}
        }

		private void togglePlayerNumber()
        {
			if(Player2  != null)
                   {
						Player2 = null;
                   }
			else
                   {
						Player2 = new Player(PlayerColor.Green, this);
                   }
        }

		// Handle the KeyDown.
		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F:
					if(GameOver || Running)
                    {
						Reset();
                    }
                    else 
					{ 
					Running = true; //Replace with StartRunning-Method (perhaps) 
					}
					break;
				case Keys.T:
					if(!Running && !GameOver)
                    { 
						togglePlayerNumber();
                    }
					break;

				case Keys.A:
					if(Player1.curdir != Direction.Right)
						Player1.curdir = Direction.Left;
					break;
				case Keys.D:
					if (Player1.curdir != Direction.Left)
						Player1.curdir = Direction.Right;
					break;
				case Keys.W:
					if (Player1.curdir != Direction.Down)
						Player1.curdir = Direction.Up;
					break;
				case Keys.S:
					if (Player1.curdir != Direction.Up)
						Player1.curdir = Direction.Down;
					break;

				case Keys.J:
					if (Player2.curdir != Direction.Right)
						Player2.curdir = Direction.Left;
					break;
				case Keys.L:
					if (Player2.curdir != Direction.Left)
						Player2.curdir = Direction.Right;
					break;
				case Keys.I:
					if (Player2.curdir != Direction.Down)
						Player2.curdir = Direction.Up;
					break;
				case Keys.K:
					if (Player2.curdir != Direction.Up)
						Player2.curdir = Direction.Down;
					break;

			}
		}

		public void DoGameOver()
        {
			Running = false;
			GameOver = true;

        }

		public void Reset()
        {
			Running = false;
			GameOver = false;
			Player1 = new Player(PlayerColor.Blue, this);
			if(Player2 != null)
				Player2 = new Player(PlayerColor.Green, this);
        }

		public void CollidePlayer()
        {
			bool HasCollided1 = false;
			bool HasCollided2 = false;

			if (Player2 != null)
				{
					for (int k = 1; k < Player1.Count; k++)
				{
					if (Player1.Snake[0].DotRect.IntersectsWith(Player1.Snake[k].DotRect))
					{
						Player1.Count = 0;
					}
				}

				for (int k = 1; k < Player2.Count; k++)
				{
					if (Player2.Snake[0].DotRect.IntersectsWith(Player2.Snake[k].DotRect))
					{
						Player2.Count = 0;
					}
				}

				for (int k = 0; k < Player2.Count; k++)
				{
					if (Player1.Snake[0].DotRect.IntersectsWith(Player2.Snake[k].DotRect))
					{
						HasCollided1 = true;
						if (Player2.Count == 0)
							DoGameOver();
					}
				}

				for (int k = 0; k < Player2.Count; k++)
				{
					if (Player2.Snake[0].DotRect.IntersectsWith(Player1.Snake[k].DotRect))
					{
						HasCollided2 = true;
						if (Player1.Count == 0)
							DoGameOver(); ;
					}
				}

				if (HasCollided1 && Player1.Count > 0)
				{
					Player1.Count = 0;
					Player2.Score += 5;
					HasCollided1 = false;
				}
				if (HasCollided2 && Player2.Count > 0)
				{
					Player2.Count = 0;
					Player1.Score += 5;
					HasCollided2 = false;
				}
			}

        }

        public void CollideWall()
		{
			if (Player1 != null)
			{
				if (Player1.Snake[0].X < 0 || Player1.Snake[0].X > 765 || Player1.Snake[0].Y < 0 || Player1.Snake[0].Y > 565)
				{
					Player1.Count = 0;
					if(Player2 == null)
                    {
						DoGameOver();
                    }
					if (Player2 != null)
					{
						if (Player2.Count == 0)
							DoGameOver();
					}
				}
			}

			if(Player2 != null)
			{
				if (Player2.Snake[0].X < 0 || Player2.Snake[0].X > 790|| Player2.Snake[0].Y < 0 || Player2.Snake[0].Y > 590)
				{
					Player2.Count = 0;
					if (Player2 == null)
					{
						DoGameOver();
					}
					if (Player1 != null)
					{
						if (Player1.Count == 0)
							DoGameOver();
					}
				}
			}
		}

	}
}
