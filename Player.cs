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

        public Dot[] Snake{ get; set; }
        public int Count { get; set; }
        int Speed = 25;
        int score = 0;
        private Direction CurDir = Direction.Up;
        public Direction curdir { get => CurDir; set => CurDir = value; }
        public int Score { get => score; set => score = value; }
        SolidBrush PlayerBrush;
        Engine Engine;

        public Player(PlayerColor color, Engine engine)
        {
            Snake = new Dot[999];
            Engine = engine;
            Count = 3;
            int SpawnX = 380;
      
            if (color == PlayerColor.Green)
            {
                for (int i = 0; i < Count; i++)
                {
                    Snake[i] = new Dot(SpawnX, 200 + Dot.SIZE * i);
                }
                PlayerBrush = new SolidBrush(Color.Green);
            }
            else
            {
                for (int i = 0; i < Count; i++)
                {
                    Snake[i] = new Dot(SpawnX, 300 + Dot.SIZE * i);
                }
                PlayerBrush = new SolidBrush(Color.Blue);
            }
        }

        public void Tick()
        {

            Move(CurDir);
            foreach (Food food in Engine.FoodList)
            {
                if (Snake[0].X == food.x && Snake[0].Y == food.y)
                {
                    switch (1) //NOTE: CHANGE LATER§§
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
            CollideWall();
            Engine.CollidePlayer();
        }

        public void Render(Graphics g)
        {
            for (int i = 0; i < Count; i++)
            {
                g.FillRectangle(PlayerBrush, Snake[i].X, Snake[i].Y, Dot.SIZE, Dot.SIZE);
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

        public void CollideWall()
        {
            if(Snake[0].X < 0 || Snake[0].X > 770 || Snake[0].Y < 0 || Snake[0].Y > 545)
            {
                Engine.DoGameOver();
            }
        }

    }

}
