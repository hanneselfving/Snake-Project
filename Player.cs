using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace SnakeProjekt
{
    public class Player
    {

        int[,] arr = new int[999, 999];
        List<Dot> Dots = new List<Dot>();
        //float Speed;
        Dot head = new Dot();

        static Pen Pen = new Pen(Color.FromArgb(255, 0, 0, 0), 10);
        public enum Direction { Left, Right, Down, Up }

        public void Tick()
        {
            var dot = new Dot();
            Dots.Add(dot); //Placeholder for eating food

        }

        public void Render(Graphics g)
        {
            head.X = 400;
            head.Y = 250;
            g.DrawRectangle(Pen, head.X, head.Y, 10, 10);
        }

        public void Move(Direction d)
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
