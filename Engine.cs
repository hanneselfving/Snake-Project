﻿
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
		SolidBrush drawBrush = new SolidBrush(Color.Black);

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
			if(!running)
            {
				args.Graphics.DrawString("Press F to run", drawFont, drawBrush, 325, 10);
				args.Graphics.DrawString("Controls: WASD and IJKL", drawFont, drawBrush, 290, 30);
				//args.Graphics.DrawString($"W:{Form.Size.Width} H:{Form.Size.Height}", drawFont, drawBrush, 300, 50);
			
            }
			Player1.Render(args.Graphics);
			Player2.Render(args.Graphics);
			if(Running) { 
			foreach(Food food in FoodList)
            {
				//food.Render(args.Graphics);
            }
				drawBrush.Color = Color.FromArgb(255, 0, 255, 0);
				args.Graphics.DrawString($"{Player1.Score}", drawFont, drawBrush, 5, 5);
				drawBrush.Color = Color.FromArgb(255, 0, 0, 255);
				args.Graphics.DrawString($"{Player2.Score}", drawFont, drawBrush, 759, 5);
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
            else
            {
                Player1 = new Player(PlayerColor.Blue, this);
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
					Running = false;
						}
                    else { 
					Running = true;
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

	}
}
