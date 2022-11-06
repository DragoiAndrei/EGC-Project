using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            window3d wnd = new window3d();
            wnd.Run(30.0, 0.0);
        }
    }
}
