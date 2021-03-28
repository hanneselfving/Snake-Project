
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace SnakeProjekt
{
	public class Engine
	{
		private MainForm Form = new MainForm();
		private Timer Timer = new Timer();
		private const int FPS = 10;
		public static int WIDTH = 800;
		public static int HEIGHT = 600;

		private bool running = false;
		private bool gameOver = false;

		private Font drawFont = new Font("Times New Roman", 16);
		private SolidBrush drawBrush = new SolidBrush(Color.White);

		public static Player Player1, Player2;
		public static Food food = null;

		public bool Running { get => running; set => running = value; }
		public bool GameOver { get => gameOver; set => gameOver = value; }

		public Engine()
        {
			Player1 = new Player(PlayerColor.Blue, this);
			Player2 = new Player(PlayerColor.Green, this);
			SpawnFood();
        }


		public void Run()
		{
			Form.Paint += Render;
			Timer.Tick += TimerEventHandler;
			Form.KeyDown += OnKeyDown;
			Timer.Interval = 1000 / FPS;
			Timer.Start();
			
			Application.Run(Form);

		}

		void TimerEventHandler(Object obj, EventArgs args)
		{
			Tick();
			Form.Refresh();
		}

		void Render(Object obj, PaintEventArgs args)
		{

				if(Running) { 
			
				if(food != null)
				food.Render(args.Graphics);
            
				drawBrush.Color = Color.Blue;
				args.Graphics.DrawString($"{Player1.Score}", drawFont, drawBrush, 5, 5);

				if(Player2  != null) { 
				drawBrush.Color = Color.Green;
				args.Graphics.DrawString($"{Player2.Score}", drawFont, drawBrush, 759, 5);
				}
				drawBrush.Color = Color.White;
			}

			Player1.Render(args.Graphics);
			if(Player2 != null) {
				Player2.Render(args.Graphics);
			}



			if(!running)
            {
				if(GameOver && Player2 != null) 
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
			
		

		}
		
		public void SpawnFood()
		{
			food = null;
			Random rand = new Random();
			 int x = 0, y = 0;
			SetFoodPosition(ref x, ref y);
			switch (rand.Next(0, 3))
			{
				case (int)FoodType.standard:
					food = new Standard(x, y);
					break;
				case (int)FoodType.valuable:
					food = new Valuable(x, y);
					break;
				case (int)FoodType.diet:
					food = new Diet(x, y);
					break;

			}
				Console.WriteLine(food.X);
				Console.WriteLine(food.Y);
	
		}

		void SetFoodPosition(ref int x, ref int y)
        {
			Random rand = new Random();

			x = rand.Next(0,WIDTH/Dot.SIZE) * Dot.SIZE;
			y = rand.Next(0, HEIGHT/Dot.SIZE) * Dot.SIZE;

		}
		
		void Tick()
		{
			if(Running) 
			{ 
				Player1.Tick();
				if(Player2 != null)
					Player2.Tick();

					if(food != null)
					food.Tick();

				
			}
        }

		private void TogglePlayerNumber()
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
						TogglePlayerNumber();
                    }
					break;

				case Keys.A:
					if(Player1.Curdir != Direction.Right)
						Player1.Curdir = Direction.Left;
					break;
				case Keys.D:
					if (Player1.Curdir != Direction.Left)
						Player1.Curdir = Direction.Right;
					break;
				case Keys.W:
					if (Player1.Curdir != Direction.Down)
						Player1.Curdir = Direction.Up;
					break;
				case Keys.S:
					if (Player1.Curdir != Direction.Up)
						Player1.Curdir = Direction.Down;
					break;

				case Keys.J:
					if (Player2.Curdir != Direction.Right)
						Player2.Curdir = Direction.Left;
					break;
				case Keys.L:
					if (Player2.Curdir != Direction.Left)
						Player2.Curdir = Direction.Right;
					break;
				case Keys.I:
					if (Player2.Curdir != Direction.Down)
						Player2.Curdir = Direction.Up;
					break;
				case Keys.K:
					if (Player2.Curdir != Direction.Up)
						Player2.Curdir = Direction.Down;
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

        public void CheckCollideWall()
		{
			if (Player1 != null)
			{
				if (Player1.Snake[0].X < 0 || Player1.Snake[0].X > Form.ClientRectangle.Width-Dot.SIZE || Player1.Snake[0].Y < 0 || Player1.Snake[0].Y > Form.ClientRectangle.Height-Dot.SIZE)
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
				if (Player2.Snake[0].X < 0 || Player2.Snake[0].X > Form.ClientRectangle.Width - Dot.SIZE|| Player2.Snake[0].Y < 0 || Player2.Snake[0].Y > Form.ClientRectangle.Height - Dot.SIZE)
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
