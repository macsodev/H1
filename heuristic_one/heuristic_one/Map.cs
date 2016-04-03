using System;
using System.Collections.Generic;   // List
using System.IO;    // StreamReader, IOException, SeekOrigin
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace heuristic_one
{
    class Map
    {
        private int[,] Matrix;
        private int Empty;      // Csak teszteléshez
        private int Barrirer;   // Csak teszteléshez
        //private Way     W;

        public Map(int Empty, int Barrirer)
        {
            this.Empty = Empty;
            this.Barrirer = Barrirer;
        }

        // A megadott nevű pontosvesszővel elválasztott csv fájlból olvas be adatokat.
        public bool ReadFromFile(String FileName)
        {
            StreamReader sr;   // Változó a fájl olvasásához
            string line;       // Változó egy sor beolvasásához
            int lines = 1;     // A csv sorainak száma
            int colunms = 1;   // A csv oszlopainak száma
            int i = 0;         // Ciklusváltozó
            int j;             // Ciklusváltozó

            // Fájl megnyitása
            try
            {
                sr = new StreamReader(FileName);
            }
            catch (IOException)
            {
                return false;
            }

            // Oszlopok és sorok számlálása
            line = sr.ReadLine();
            // Az első sorban megszámolja a pontosvesszőket.
            while (i < line.Length) if (line[i++] == ';') ++colunms;
            // A második sortól kezdve megszámolja a sorokat.
            while ((line = sr.ReadLine()) != null) ++lines;

            // Vissza a fájl elejére
            sr.BaseStream.Seek(0, SeekOrigin.Begin);

            // Helyfoglalás a térkép mátrixához
            Matrix = new int[lines, colunms];

            // Adatok beolvasása a mátrixba
            i = 0;
            line = sr.ReadLine();
            while (i < lines)
            {
                j = 0;
                while (j < colunms)
                {
                    // Adatok beolvasása a ;-k átugrásával
                    if (line[j * 2] == '0')
                    {
                        Matrix[i, j] = Empty;
                    }
                    else
                    {
                        Matrix[i, j] = Barrirer;
                    }
                    ++j;
                }
                ++i;
                line = sr.ReadLine();
            }

            sr.Close();
            return true;
        }

        public int GetWidth()
        {
            return Matrix.GetLength(1);
        }

        public int GetHeight()
        {
            return Matrix.GetLength(0);
        }

        // Vonal rajzolása két pont között amíg akadályba nem ütközik
        public void PrintLine(Coords Starting_point, Coords Endpoint)
        {
            List<Coords> Line = new List<Coords>();

            // A vonal pontjainak megállapítása
            Alg.line(
                Starting_point.GetX(),
                Starting_point.GetY(),
                Endpoint.GetX(),
                Endpoint.GetY(),
                Line
            );

            // Rajzoláshoz
            int x = Line.ElementAt(0).GetX();
            int y = Line.ElementAt(0).GetY();
            for (int i = 1; i < Line.Count && Matrix[y, x] == Empty; ++i)
            {
                Matrix[y, x] = '@';
                x = Line.ElementAt(i).GetX();
                y = Line.ElementAt(i).GetY();
            }

        }
        
        public override String ToString()
        {
            StringBuilder s = new StringBuilder();

            for (int i = 0; i < GetHeight(); ++i)
            {
                for (int j = 0; j < GetWidth(); ++j)
                {
                    s.Insert(s.Length, (char)Matrix[i, j] + " ");
                }
                s.Insert(s.Length, '\n');
            }
            return s.ToString();
        }

    }
}
