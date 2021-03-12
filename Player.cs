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

        
        LinkedList<Dot> Snake = new LinkedList<Dot>();
        Pen Pen;
        Dot Head;

        public Player(PlayerColor color)
        {
            this.Head = new Dot(400, 250);
            if (color == PlayerColor.Green)
            {
                Pen = new Pen(Color.FromArgb(255, 0, 255, 0), 10);
            }
            else
            {
                Pen = new Pen(Color.FromArgb(255, 0, 0, 255), 10);
            }

            Snake.Add(Head);
        }

        public void Tick()
        {
            foreach (Food food in Engine.FoodList)
            {
                if (Head.X == food.x && Head.Y == food.Y)
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
            foreach (Dot dot in Snake)
            {
                g.DrawRectangle(Pen, dot.X, dot.Y, 10, 10);
            }
        }

        public void Move(Direction d)
        {
            for (int i = Snake.Count; i > 0; i--)
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
                            if (Head.X >= 10)
                            {
                                Head.X -= 10;
                            }
                            break;

                        case Direction.Right:
                            if (Head.X < 1000)
                            {
                                Head.X += 10;
                            }
                            break;

                        case Direction.Up:
                            if (Head.Y < 1000)
                            {
                                Head.Y -= 10;
                            }
                            break;

                        case Direction.Down:
                            if (Head.Y >= 10)
                            {
                                Head.Y += 10;
                            }
                            break;
                    }

                }

            }

        }

    }
}
