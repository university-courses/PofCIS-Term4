using System;
using System.IO;
using System.Text.RegularExpressions;
using Task1CS.Interfaces;

namespace Task1CS.Classes
{
	public class Triangle : IShape
	{
		private Point _pointA;
		private Point _pointB;
		private Point _pointC;

		public double CalcPerimeter()
		{
			return Point.CalcDistance(_pointA, _pointB) + 
				   Point.CalcDistance(_pointB, _pointC) + 
				   Point.CalcDistance(_pointC, _pointA);
		}

		public double CalcSquare()
		{
			var halfPerimeter = CalcPerimeter() / 2;
			return Math.Sqrt(halfPerimeter * (halfPerimeter - Point.CalcDistance(_pointA, _pointB))
										   * (halfPerimeter - Point.CalcDistance(_pointB, _pointC))
										   * (halfPerimeter - Point.CalcDistance(_pointC, _pointA)));
		}

		public void ReadFromFile(ref StreamReader streamReader)
		{
			var line = streamReader.ReadLine();
			if (line == null)
			{
				throw new IOException("can't read data from stream");
			}
			var data = line.Split(' ');

			_pointA = new Point(data[1], data[2]);
			_pointB = new Point(data[3], data[4]);
			_pointC = new Point(data[5], data[6]);
	
		}

		public void WriteToFile(ref StreamWriter streamWriter)
		{
			streamWriter.WriteLine("Triangle :");
			streamWriter.WriteLine($"A({_pointA.X}, {_pointA.Y})");
			streamWriter.WriteLine($"B({_pointB.X}, {_pointB.Y})");
			streamWriter.WriteLine($"C({_pointC.X}, {_pointC.Y})");
		}
	}
}
