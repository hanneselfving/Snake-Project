using System.Windows.Forms;
using System.Drawing;
using System.Timers;

namespace SnakeProjekt
{
    abstract public class Food
    {
        public float x, y;
        public int lengthAdd, point, size = 5;
        public bool Expired = false;
        protected System.Timers.Timer Timer;

        abstract public void Tick();
        abstract public void Render(Graphics g);


    }


    public class Standard : Food
    {
        int point = 1;
        int lengthAdd = 1;

        public override void Tick()
        {
            Timer = new System.Timers.Timer(15);
            Timer.AutoReset = false;
            Timer.Enabled = true;
            Timer.Interval = 1000;
            Timer.Start();

            if (Timer.Enabled == false)
                Expired = true;

            if (Expired == true)
            {
                Timer.Stop();
                Timer.Dispose();
            }


        }

        Pen pen = new Pen(Color.FromArgb(255, 100, 100), 10);
        public override void Render(Graphics g)
        {
            g.DrawEllipse(pen, x + (float)25 / 2, y + (float)25 / 2, size, size);
        }

    }

    public class Valuable : Food
    {
        int point = 5;
        int lengthAdd = 2;

        public override void Tick()
        {
            Timer = new System.Timers.Timer(5);
            Timer.AutoReset = false;
            Timer.Enabled = true;
            Timer.Interval = 1000;
            Timer.Start();

            if (Timer.Enabled == false)
                Expired = true;

            if (Expired == true)
            {
                Timer.Stop();
                Timer.Dispose();
            }


        }

        Pen pen = new Pen(Color.FromArgb(100, 255, 100), 10);
        public override void Render(Graphics g)
        {
            g.DrawEllipse(pen, x + (float)25 / 2, y + (float)25 / 2, size, size);

        }
    }

    public class Diet : Food
    {
        int point = 1;
        int lengthAdd = -1;

        public override void Tick()
        {
            Timer = new System.Timers.Timer(5);
            Timer.AutoReset = false;
            Timer.Enabled = true;
            Timer.Interval = 1000;
            Timer.Start();

            if (Timer.Enabled == false)
                Expired = true;

            if (Expired == true)
            {
                Timer.Stop();
                Timer.Dispose();
            }

        }

        Pen pen = new Pen(Color.FromArgb(100, 100, 255), 10);
        public override void Render(Graphics g)
        {
            g.DrawEllipse(pen, x + (float)25 / 2, y + (float)25 / 2, size, size);

        }
    }
}
