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
        
		// Draw a line from Start point in Direction (degrees) until obstacle
        public Coords Bresenham2(Coords Start, double Direction)
        {
            List<Coords> L = new List<Coords>();
            int x = Start.GetX();
            int y = Start.GetY();
            int dx, dy;
            double St, Steepness;
            
			dy = 0; // Horizontal line
			dx = 0; // Vertical line

            // Right or left
            if (Direction < 90 || Direction > 270) dx = 1;
            else if (Direction > 90 && Direction < 270) dx = -1;

            // Up or down
            if (Direction < 180) dy = -1;
            else if (Direction > 180) dy = 1;
            
			// 90 or 270 degrees are invalid on a tangent
			if(Direction == 90 || Direction == 270) {
				Steepness = 1;
			} else {
	            Steepness = Math.Abs(Math.Tan(Direction / 180.0 * Math.PI));
			}
			
            if (Steepness <= 1) {
                St = y + (dy * Steepness);
                x += dx;
                y += (int)Math.Round(dy * Steepness);
            } else {
                St = x + (dx / Steepness);
                x += (int)Math.Round(dx / Steepness);
                y += dy;
            }

            // Drawing
            while (x >= 0 && y >= 0 && x < GetWidth() && y < GetHeight()
                && Matrix[y, x] == Empty)
            {
                Matrix[y, x] = 'x';
                L.Add(new Coords(x,y));

                if (Steepness <= 1) {
                    x += dx;
                    St += dy * Steepness;
                    y = (int)Math.Round(St);
                } else {
                    St += dx / Steepness;
                    x = (int)Math.Round(St);
                    y += dy;
                }
            }

            if (L.Count > 0) return L.ElementAt(L.Count - 1);
            else return Start;
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
