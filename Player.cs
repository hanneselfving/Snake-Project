using System.Drawing;

namespace SnakeProjekt
{
    public enum Direction { Left, Right, Down, Up };
    public enum PlayerColor { Green, Blue };

    public class Player
    {

        private int speed = Dot.SIZE;
        private int score = 0;
        private Direction curdir = Direction.Up;

        public Dot[] Snake { get; set; }
        public int Count { get; set; }
        public Direction Curdir { get => curdir; set => curdir = value; }
        public int Score { get => score; set => score = value; }
        SolidBrush PlayerBrush;
        Engine Engine;

        public Player(PlayerColor color, Engine engine)
        {
            Snake = new Dot[999];
            Engine = engine;
            Count = 3;
            int SpawnX = Engine.WIDTH/2 - Dot.SIZE;
      
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

            Move(Curdir);
            Food food = Engine.food;

                if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                {
                    if(food.Type == FoodType.standard)
                    {
                            Snake[Count] = new Dot(Snake[Count-1].X, Snake[Count-1].Y);
                            Count++;
                            Score++;
                        
                    }
                    else if(food.Type == FoodType.valuable)
                    {
                            
                            Snake[Count] = new Dot(Snake[Count-1].X, Snake[Count-1].Y);
                            Count++;
                            Score+=5;

                    }
                    else if(food.Type == FoodType.diet)
                    {
                            
                            Snake[Count] = new Dot(Snake[Count-1].X, Snake[Count-1].Y);
                            Count++;

                    } 
                    Engine.SpawnFood();
                }

            Engine.CollideSelf();
            Engine.CheckCollideWall();
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
                    Snake[0].Y -= speed;
                    break;
                case Direction.Down:
                    Snake[0].Y += speed;
                    break;
                case Direction.Left:
                    Snake[0].X -= speed;
                    break;
                case Direction.Right:
                    Snake[0].X += speed;
                    break;
            }

        }


    }

}
