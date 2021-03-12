using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace SnakeProjekt
{
    public enum Direction { Left, Right, Down, Up };
    public enum PlayerColor { Green, Blue };

    public class Player
    {

        int[,] arr = new int[999, 999];
        List<Dot> Snake = new List<Dot>();
        Pen Pen;

        public Player(PlayerColor color)
        {
            Dot head = new Dot(400, 250);
            if (color == PlayerColor.Green)
            {
                Pen = new Pen(Color.FromArgb(255, 0, 255, 0), 10);
            }
            else
            {
                Pen = new Pen(Color.FromArgb(255, 0, 0, 255), 10);
            }

            Snake.Add(head);
        }

        public void Tick()
        {
            foreach (food in Food)
            {
                if (head.X == food.x && head.Y == food.Y)
                {
                    switch (food.lengthAdd)
                    {
                        case 1:
                            var dot = new Dot(Snake[Snake.Count - 1].X, Snake[Snake.Count - 1].Y);
                            Snake.Add(dot);
                            break;

                        case 2:
                            var dot = new Dot(Snake[Snake.Count - 1].X, Snake[Snake.Count - 1].Y);
                            Snake.Add(dot);
                            var dot = new Dot(Snake[Snake.Count - 1].X, Snake[Snake.Count - 1].Y);
                            Snake.Add(dot);
                            break;

                        case -1:
                            if (Snake.Count > 1)
                                Snake.RemoveAt(Snake.Count - 1);
                            break;
                    }


                }
            }

        }

        public void Render(Graphics g)
        {


            foreach (Dot in Snake)
            {
                g.DrawRectangle(Pen, Dot.X, Dot.Y, 10, 10);
            }
        }

        public void Move(Direction d)
        {
            for (i = Snake.Count; i > 0; i--)
            {
                if (i != 0)
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
                else
                {
                    switch (d)
                    {
                        case Direction.Left:
                            if (head.X >= 10)
                            {
                                head.X -= 10;
                            }
                            break;

                        case Direction.Right:
                            if (head.X < 1000)
                            {
                                head.X += 10;
                            }
                            break;

                        case Direction.Up:
                            if (head.Y < 1000)
                            {
                                head.Y -= 10;
                            }
                            break;

                        case Direction.Down:
                            if (head.Y >= 10)
                            {
                                head.Y += 10;
                            }
                            break;
                    }

                }

            }

        }

    }
}
