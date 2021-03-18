
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SnakeProjekt
{
	public class Engine
	{
		MainForm Form = new MainForm();
		Timer Timer = new Timer();
		const int FPS = 30;

		public static Player Player1, Player2;
		public static LinkedList<Food> FoodList = new LinkedList<Food>();

		public Engine()
        {
			Player1 = new Player(PlayerColor.Blue);
			Player2 = new Player(PlayerColor.Green);
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
			Player2.Render(args.Graphics);
			foreach(Food food in FoodList)
            {
				//food.Render(args.Graphics);
            }

		}

		void Tick()
		{
			Player1.Tick();
			Player2.Tick();
			foreach(Food food in FoodList)
            {
				food.Tick();
            }
		}

		// Handle the KeyDown.
		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
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
			}
		}



	}
}
