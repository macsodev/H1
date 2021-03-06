﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace heuristic_one
{
	/// <summary>
	/// Ez az osztály a lehetséges utak adatait tárolja és
	/// kiszámítja az ahhoz tartozó jósági tényezőt (súlyt).
	/// </summary>
	class Path
	{
		public Point	Start;
		public Point	End;
		public double	Length;
		public double	Rotation;
		//public double	Újonnan lefedett terület súlyának átlaga
		public double	Importance;

		public Path(Point Start, Point End) {
			this.Start = Start;
			this.End = End;
			this.Length = Auxilary.Distance(Start, End);
			this.Rotation = Math.Abs(this.Start.theta - this.End.theta);
			this.Importance = I();
		}

		private double I() {
			double res;

			res = this.Length + this.Rotation / 4.0;    // Hossz + elfordulás/4
			//res -= Újonnan lefedett terület súlyának átlaga

			return res;
		}

	}
}
