using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace SnakeProjekt
{
    public class Player
    {

        int[,] arr = new int[999,999];
        List<Dot> Dots = new List<Dot>();
        float Speed;
        public enum Direction { Left, Right, Down, Up }

        public void Tick()
        {
           

        }

        public void Render(PaintEventArgs g)
        {
            Dots head = new Dots();
            head.X = 10;
            head.Y = 10;
        }


    }




}
