using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SnakeProjekt
{
	public class Engine
	{
		MainForm Form = new MainForm();
		Timer Timer = new Timer();

		public void Run()
		{
            Form.Paint += Render;
			Timer.Tick += TimerEventHandler;
			Timer.Interval = 1000/25;
			Timer.Start();

			Application.Run(Form);
		}

		void TimerEventHandler(Object obj, EventArgs args)
		{
			Form.Refresh();
		}

		void Render(Object obj, PaintEventArgs args)
		{
		
		}

	}
}
