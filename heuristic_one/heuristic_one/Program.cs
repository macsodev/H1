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
            List<Coords> list1 = new List<Coords>();

            //test params
            Alg.line(30, 20, 0, 0, list1);

            //Larger window
            //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            //Console.SetWindowPosition(0, 0);

            Console.SetCursorPosition(0, 20);
            Console.WriteLine("\nBejárt pontok:\n----------------");
            foreach (var item in list1) {
                Console.WriteLine("X:" + item.GetX()+" Y:"+item.GetY());
            }

            Console.Read();
        }
   
   }
}
