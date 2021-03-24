
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
		const int FPS = 30;

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
				//food.Render(args.Graphics);
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

		void Tick()
		{
			if(Running) { 
			Player1.Tick();
			Player2.Tick();
			Player1.CollidePlayer(Player2);
			Player2.CollidePlayer(Player1);

			foreach (Food food in FoodList)
            {
				food.Tick();
            }
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
					Player1.curdir = Direction.Left;
					break;
				case Keys.D:
					Player1.curdir = Direction.Right;
					break;
				case Keys.W:
					Player1.curdir = Direction.Up;
					break;
				case Keys.S:
					Player1.curdir = Direction.Down;
					break;

				case Keys.J:
					Player2.curdir = Direction.Left;
					break;
				case Keys.L:
					Player2.curdir = Direction.Right;
					break;
				case Keys.I:
					Player2.curdir = Direction.Up;
					break;
				case Keys.K:
					Player2.curdir = Direction.Down;
					break;

			}
		}

		public void Reset()
        {
			running = false;
        }

	}
}
