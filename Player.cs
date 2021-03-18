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


        Dot[] Snake;
        public int Count { get; set; }
        int Speed = 10;
        private Direction CurDir = Direction.Up;
        public Direction curdir { get => CurDir; set => CurDir = value; }
        Pen Pen;
        private Direction curDir;

        public Player(PlayerColor color)
        {
            Snake = new Dot[999];
            Count = 3;
            for (int i = 0; i < Count; i++)
            {
                Snake[i] = new Dot(400, 200 + Dot.SIZE * i);
            }
            if (color == PlayerColor.Green)
            {
                Pen = new Pen(Color.FromArgb(255, 0, 255, 0), 10);
            }
            else
            {
                Pen = new Pen(Color.FromArgb(255, 0, 0, 255), 10);
            }
        }

        public void Tick()
        {
            Move(CurDir);
            foreach (Food food in Engine.FoodList)
            {
                if (Snake[0].X == food.x && Snake[0].Y == food.y)
                {
                    switch (1) //NOTE: CHANGE LATER
                    {
                        case 1:
                            Count++;
                            break;

                        case 2:
                            Count++;
                            break;

                        case -1:
                            if (Count > 1)
                                Count--;
                            break;
                    }


                }
            }

        }

        public void Render(Graphics g)
        {
            for (int i = 0; i < Count; i++)
            {
                g.DrawRectangle(Pen, Snake[i].X, Snake[i].Y, Dot.SIZE, Dot.SIZE);
            }
        }
        public void Move(Direction d)
        {

            for (int i = Count - 1; i > 0; i--) //Move tail
            {

                Snake[i].X = Snake[i - 1].X;
                Snake[i].Y = Snake[i - 1].Y;

            }

            switch (d) // Move head
            {
                case Direction.Up:
                    Snake[0].Y -= Speed;
                    break;
                case Direction.Down:
                    Snake[0].Y += Speed;
                    break;
                case Direction.Left:
                    Snake[0].X -= Speed;
                    break;
                case Direction.Right:
                    Snake[0].X += Speed;
                    break;
            }

        }
    }





}
