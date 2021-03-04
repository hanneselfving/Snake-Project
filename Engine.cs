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

		Player Player1, Player2;

		public void Run()
		{
            Form.Paint += Render;
			Timer.Tick += TimerEventHandler;
			Timer.Interval = 1000/FPS;
			Timer.Start();

			KeyDown += Move;

			Application.Run(Form);




		}

		void TimerEventHandler(Object obj, EventArgs args)
		{
			Form.Refresh();
		}

		void Render(Object obj, PaintEventArgs args)
		{
		


		}

		void Tick()
        {

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
