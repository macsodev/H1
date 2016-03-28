using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace heuristic_one
{
    class Program
    {
        static void Main(string[] args)
        {
            //kezdjunk neki :) 
            Alg a1 = new Alg();
            List<Coords> list1 = new List<Coords>();

            a1.line(0, 0, 34, 25, list1);

        }
   
   }
}
