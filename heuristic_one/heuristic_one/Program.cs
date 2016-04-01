using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace heuristic_one
{
    class Program
    {
        public static int DEBUG_LEVEL { get; set; } = 1;
        static void Main(string[] args)
        {
            //kezdjunk neki :) 
            Alg a1 = new Alg();
            List<Coords> list1 = new List<Coords>();

            //test params
            a1.line(30, 20, 0, 0, list1);

            if (Program.DEBUG_LEVEL == 1) {
                Console.SetCursorPosition(0, 20);
                Console.WriteLine("\nBejárt pontok:\n----------------");
                foreach (var item in list1){
                    Console.WriteLine("X:" + item.GetX() + " Y:" + item.GetY());
                }
            }

            Console.Read();
            Console.Clear();

            Random rnd = new Random();

            int[,] InputMatrix = new int[640, 640];                 // GUI's matrix map
            int[,] OutputMatrix = new int[640, 640];                // H1's own matrix map

            for (int i = 0; i < 640; i++) {
                for (int j = 0; j < 640; j++) {
                    InputMatrix[i, j] = rnd.Next(2);
                    if (Program.DEBUG_LEVEL == 2) Console.Write(InputMatrix[i, j] + " ");
                }
                if (Program.DEBUG_LEVEL == 2) Console.Write("\n");
            }

            int radius = 3;
            //TODO: inspect the first 'radius' element whether they are obstacles or not
            bool GotStucked = false;
            for (int i = 0; i < (list1.Count - radius); i++) {
                if (Program.DEBUG_LEVEL == 1) Console.WriteLine("Inspecting X: " + list1[i + radius].GetX() + " Y: " 
                    + list1[i + radius].GetY() + " Value: " + InputMatrix[list1[i + radius].GetX(), list1[i + radius].GetY()]);
                //if a non zero element found far ahead...
                if (InputMatrix[list1[i + radius].GetX(), list1[i + radius].GetY()] != 0){
                    GotStucked = true;
                    break;
                }
            }
            Console.WriteLine(GotStucked);
        }
   }
}
