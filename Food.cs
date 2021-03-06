using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

namespace SnakeProjekt
{
    abstract public class Food
    {
        protected int x, y, size = 5;
        public bool Expired = false;
        protected System.Timers.Timer Timer;

        abstract public void Tick();
        abstract public void Render(PaintEventArgs g);


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

        Pen pen = new Pen(Color.FromArgb(255, 0, 0));
        public override void Render(PaintEventArgs g)
        {
            g.Graphics.DrawEllipse(pen, x, y, size, size);
            pen.Dispose();
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

        Pen pen = new Pen(Color.FromArgb(0, 255, 0));
        public override void Render(PaintEventArgs g)
        {
            g.Graphics.DrawEllipse(pen, x, y, size, size);

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

        Pen pen = new Pen(Color.FromArgb(0, 0, 255));
        public override void Render(PaintEventArgs g)
        {
            g.Graphics.DrawEllipse(pen, x, y, size, size);

        }
    }
}

