using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Task1CS.Interfaces;

namespace Task1CS.Classes
{
	public class Triangle : IShape
	{
		private readonly Point[] _points;

		private const int PointsCount = 3;
		private const int CoordinatesPerPoint = 2;

		public Triangle()
		{
			_points = new Point[PointsCount];
		}
		
		public Triangle(Point[] points)
		{
			if (points.Length != PointsCount)
			{
				throw new InvalidDataException("invalid triangle points count");
			}

			if (!TriangleIsValid(points))
			{
				throw new InvalidDataException("invalid triangle");
			}

			_points = points;
		}

		private static bool TriangleIsValid(IReadOnlyList<Point> points)
		{
			var a = Point.CalcDistance(points[0], points[1]); 
			var b = Point.CalcDistance(points[1], points[2]); 
			var c = Point.CalcDistance(points[2], points[0]);
			return a >= b + c || b >= a + c || c >= b + a;
		}
		
		public double CalcPerimeter()
		{
			return _points.Select((current, i) => Point.CalcDistance(current, _points[(i + 1) % _points.Length])).Sum();
		}
		
		public double CalcSquare()
		{
			var halfPerimeter = CalcPerimeter() / 2;
			var expression = halfPerimeter;
			for (var i = 0; i < _points.Length; i++)
			{
				expression *= halfPerimeter - Point.CalcDistance(_points[i], _points[(i + 1) % _points.Length]);
			}
			return Math.Sqrt(expression);
		}

		public bool ReadFromFile(ref StreamReader streamReader)
		{
			var line = streamReader.ReadLine();
			if (line == null)
			{
				throw new IOException("can't read data from stream");
			}

			var result = Regex.Match(line, @"Triangle\{\s*((\d+\s+){5}(\d+){1})\s*\}");
			if (!result.Success)
			{
				return false;
			}

			var data = result.Groups[1].ToString().Split(' ');
			if (data.Length != PointsCount * CoordinatesPerPoint)
			{
				throw new InvalidDataException("invalid triangle points count");
			}

			for (var i = 0; i < PointsCount; i += CoordinatesPerPoint)
			{
				_points[i / CoordinatesPerPoint] = new Point(Helpers.StringToDouble(data[i]), Helpers.StringToDouble(data[i + 1]));
			}

			return true;
		}

		public void WriteToFile(ref StreamWriter streamWriter)
		{
			streamWriter.WriteLine(this);
		}
		
		public override string ToString()
		{
			var result = "Triangle :";
			for (var i = 0; i < _points.Length; i++)
			{
				result += $"\n  Point {i}: x={_points[i].X}, y={_points[i].Y}";
			}

			return result;
		}
	}
}
