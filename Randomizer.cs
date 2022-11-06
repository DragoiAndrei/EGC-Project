using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;

namespace EGC_Project
{
    class Randomizer
    {
        private Random r;
        private int LOW_INT_VAL = -25;
        private int HIGH_INT_VAL = 25;

        public Randomizer()
        {
            r = new Random();
        }

        /// <summary>
        /// This method returns an random color when requested.
        /// </summary>
        /// <returns>The color, randomly generated!</returns>
        public Color RandomColor()
        {
            int genR = r.Next(0, 255);
            int genG = r.Next(0, 255);
            int genB = r.Next(0, 255);
            //Console.WriteLine(Convert.ToString(genR) +" "+ Convert.ToString(genG)+" "+ Convert.ToString(genB));
            Color col = Color.FromArgb(genR, genG, genB);

            return col;
        }

        public int RandomInt()
        {
            int i = r.Next(LOW_INT_VAL, HIGH_INT_VAL);

            return i;
        }
        public int toStringRGB()
        {
            int i = r.Next(LOW_INT_VAL, HIGH_INT_VAL);

            return i;
        }
    }
}
