
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
		public bool Running { get => running; set => running = value;}

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
				args.Graphics.DrawString("Press F to run", drawFont, drawBrush, 305, 10);
				args.Graphics.DrawString("Controls: WASD and IJKL", drawFont, drawBrush, 265, 30);
				args.Graphics.DrawString("Press T to toggle number of players", drawFont, drawBrush, 265, 50);
				//args.Graphics.DrawString($"W:{Form.Size.Width} H:{Form.Size.Height}", drawFont, drawBrush, 300, 50);
			
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
			Random r = new Random();

			for (int i = FoodList.Count; i < 15; i++)
			{
				bool newplace = false;

				int x;
				int y;
				do
				{
					x = r.Next(Form.Width-50);
					y = r.Next(Form.Height-75);

					if (FoodList.Count != 0)
					{
						foreach (Food food in FoodList)
						{
							if (food.x + 100 > x && x < food.x - 100)
								newplace = false;
							else
								newplace = true;

							if (food.y + 100 > y && y < food.y - 100)
								newplace = false;
							else
								newplace = true;
						}
					}

					else
						newplace = true;

                } while (newplace == false);

				int ran = r.Next(10);

				if (ran < 8)
				{
					Standard food = new Standard();
					food.x = x;
					food.y = y;
					FoodList.AddFirst(food);
				}

				else if (ran >= 8 && ran < 9)
				{
					Valuable food = new Valuable();
					food.x = x;
					food.y = y;
					FoodList.AddFirst(food);
				}

				else if (ran == 9)
				{
					Diet food = new Diet();
					food.x = x;
					food.y = y;
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
					if(Running) { 
					Running = false; //Reset method
						}
                    else { 
					Running = true;  //Reset method
					}
					break;
				case Keys.T:
					if(!Running)
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

		public void Reset()
        {
			running = false;
			Player1 = new Player(PlayerColor.Blue, this);
			if(Player2 != null)
				Player2 = new Player(PlayerColor.Green, this);
        }

		public void CollidePlayer()
        {
			if(Player2  != null) {
				for (int k = 0; k < Player2.Count; k++)
				{
					if (Player1.Snake[0].DotRect.IntersectsWith(Player2.Snake[k].DotRect))
					{
						Reset();
					}
				}

				for (int k = 0; k < Player2.Count; k++)
				{
					if (Player2.Snake[0].DotRect.IntersectsWith(Player1.Snake[k].DotRect))
					{
						Reset();
					}
				}
			}
        }

	}
}
