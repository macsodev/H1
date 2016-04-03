using System;
using System.Collections.Generic;   // List
using System.IO;    // StreamReader, IOException, SeekOrigin
using System.Linq;  // ElementAt
using System.Text;  // StringBuilder
using System.Threading.Tasks;

namespace heuristic_one
{
    class Map
    {
        private int[,] Matrix;
        private int Empty;      // Test only
        private int Barrirer;   // Test only
        //private Way     W;

        public Map(int Empty, int Barrirer)
        {
            this.Empty = Empty;
            this.Barrirer = Barrirer;
        }

        // Reads data from CSV file with the specified name
        public bool ReadFromFile(String FileName)
        {
            StreamReader sr;   // Variable to read from file
            string line;       // A line of the file
            int lines = 1;     // Number of lines in CSV
            int colunms = 1;   // Number of columns in CSV
            int i = 0;         // Index variable
            int j;             // Index variable

            // Opening file
            try
            {
                sr = new StreamReader(FileName);
            }
            catch (IOException)
            {
                return false;
            }

            // Columns and rows count
            line = sr.ReadLine();
            while (i < line.Length) if (line[i++] == ';') ++colunms;
            while ((line = sr.ReadLine()) != null) ++lines;

            // Rewind
            sr.BaseStream.Seek(0, SeekOrigin.Begin);

            // Allocating memory
            Matrix = new int[lines, colunms];

            // Reading data to matrix
            i = 0;
            line = sr.ReadLine();
            while (i < lines)
            {
                j = 0;
                while (j < colunms)
                {
                    if (line[j * 2] == '0')
                        Matrix[i, j] = Empty;
                    else
                        Matrix[i, j] = Barrirer;
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

        // Draw a line between two points until an obstacle
        public void PrintLine(Coords Starting_point, Coords Endpoint)
        {
            List<Coords> Line = new List<Coords>();

            // Finding the points of the line
            Alg.line(
                Starting_point.GetX(),
                Starting_point.GetY(),
                Endpoint.GetX(),
                Endpoint.GetY(),
                Line
            );

            // Drawing
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
