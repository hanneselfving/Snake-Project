using System.Windows.Forms;
using System.Drawing;
using System.Timers;

namespace SnakeProjekt
{
    public enum FoodType
    {
        standard, valuable, diet
    }

    abstract public class Food
    {

        public static readonly int SIZE = 5;
        private int x, y;
        private FoodType type;
        private Pen pen;
        private SolidBrush brush;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public FoodType Type { get => type; set => type = value; }
        protected SolidBrush Brush { get => brush; set => brush = value; }
        protected Pen Pen { get => pen; set => pen = value; }

        public Food(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        abstract public void Tick();
        public void Render(Graphics g)
        {
            g.FillEllipse(Brush, X + Dot.SIZE/2 - SIZE/2, Y + Dot.SIZE/2 - SIZE/2, SIZE, SIZE);
           // g.FillRectangle(Brush, X, Y, Dot.SIZE, Dot.SIZE);
            
        }


    }


    public class Standard : Food
    {
        
        public Standard(int x, int y) : base(x,y)
        {
            this.Brush = new SolidBrush(Color.FromArgb(255,100,100));
            this.Type = FoodType.standard;
        }

        public override void Tick()
        {
            
        }
    }

    public class Valuable : Food
    {

        public Valuable(int x, int y) : base(x, y)
        {
            this.Brush = new SolidBrush(Color.FromArgb(100, 255, 100));
            this.Type = FoodType.valuable;
        }

        public override void Tick()
        {
  
        }
    }

    public class Diet : Food
    {
        public Diet(int x, int y) : base(x, y)
        {
            this.Brush = new SolidBrush(Color.FromArgb(100, 100, 255));
            this.Type = FoodType.diet;
        }

        public override void Tick()
        {
    
        }

    }
}
