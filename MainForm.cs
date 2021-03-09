using System;
using System.Windows.Forms;

namespace Snake
{
    public class MainForm : Form
    {
        public MainForm() : base()
        {
            Text = "Snake";
            Width = 800;
            Height = 600;
            DoubleBuffered = true;
        }
    }
}
