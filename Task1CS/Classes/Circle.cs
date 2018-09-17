using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Task1CS.Interfaces;

namespace Task1CS.Classes
{
	public class Circle : IShape
	{
		private Point[] _points;
		private double _radius;
		
		private const int PointsCount = 2;
		private const int CoordinatesPerPoint = 2;

		public Circle()
		{
			_points = new Point[PointsCount];
			_radius = CalcRadius();
		}
		
		public Circle(Point[] points)
		{
			if (points == null)
			{
				throw new NullReferenceException("points is null");
			}
			
			if (points[0] == points[1])
			{
				throw new InvalidDataException("no circle entered");
			}

			_points = points;
			_radius = CalcRadius();
		}

		private double CalcRadius()
		{
			return Point.CalcDistance(_points[0], _points[1]);
		}
		
		public void WriteToStream(ref StreamWriter streamWriter)
		{
			streamWriter.WriteLine(this);
		}

		public bool ReadFromStream(ref StreamReader streamReader)
		{
			var line = streamReader?.ReadLine();
			if (line == null)
			{
				throw new IOException("can't read data from stream");
			}

			var result = Regex.Match(line, @"Circle\{\s*((-?\d+\s+){3}(-?\d+){1})\s*\}");
			if (!result.Success)
			{
				return false;
			}

			var data = result.Groups[1].ToString().Split(' ');
			if (data.Length != PointsCount * CoordinatesPerPoint)
			{
				throw new InvalidDataException("invalid circle points count");
			}
			
			var points = new List<Point>();  

			for (var i = 0; i < PointsCount * CoordinatesPerPoint; i += CoordinatesPerPoint)
			{
				points.Add(new Point(Helpers.StringToDouble(data[i]), Helpers.StringToDouble(data[i + 1])));
			}

			if (points[0]==points[1])
			{
				return false;
			}

			_points = points.ToArray();

			return true;
		}

		public double CalcSquare()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}
			return Math.PI * Math.Pow(_radius, 2);
		}

		public double CalcPerimeter()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}
			return 2 * Math.PI * _radius;
		}

		public IEnumerable<Point> GetPoints()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}
			return _points;
		}
	}
}
