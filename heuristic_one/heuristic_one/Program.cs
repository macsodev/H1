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

            //test params
            a1.line(0, 0, 39, 14, list1);

            //Larger window
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);


            Console.WriteLine("\nBejárt pontok:\n----------------");
            foreach (var item in list1) {
                Console.WriteLine("X:" + item.GetX()+" Y:"+item.GetY());
            }
        }
   
   }
}
