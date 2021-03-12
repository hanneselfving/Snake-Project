
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
			Timer.Interval = 1000 / FPS;
			Timer.Start();

			Application.Run(Form);




		}

		void TimerEventHandler(Object obj, EventArgs args)
		{
			Form.Refresh();
		}

		void Render(Object obj, PaintEventArgs args)
		{
			Player1.Render(args.Graphics);
			Player2.Render(args.Graphics);
			foreach(Food food in FoodList)
            {
				food.Render(args.Graphics);
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
					//left
					break;
				case Keys.D:
					//right
					break;
				case Keys.W:
					//up
					break;
				case Keys.S:
					//down
					break;
			}
		}



	}
}
