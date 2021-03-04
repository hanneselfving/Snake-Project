using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Snake
{
    public class Food
    {
        private int x, y;

        Random r = new Random();
        private int windowHight;
        private int windowWidth;
        
        //Hej
        //På
        //Dig

    }        


    public class Standard : Food
    {
        int point = 1;
        int lengthAdd = 1;
    }

    public class Valuable : Food
    {
        int point = 5;
        int lengthAdd = 2;
    }

    public class Diet : Food
    {
        int point = 1;
        int lengthAdd = -1;
    }
}
