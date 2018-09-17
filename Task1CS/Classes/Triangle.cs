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
		private Point[] _points;

		private const int PointsCount = 3;
		private const int CoordinatesPerPoint = 2;

		public Triangle()
		{
			_points = new Point[PointsCount];
		}
		
		public Triangle(Point[] points)
		{
			if (points == null)
			{
				throw new NullReferenceException("points is null");
			}
			
			if (points.Length != PointsCount)
			{
				throw new InvalidDataException("invalid triangle points count");
			}

			if (!PointsAreValid(points))
			{
				throw new InvalidDataException("invalid triangle");
			}

			_points = points;
		}

		public static bool PointsAreValid(IReadOnlyList<Point> points)
		{
			if (points == null)
			{
				throw new NullReferenceException("points is null");
			}
			var a = Point.CalcDistance(points[0], points[1]); 
			var b = Point.CalcDistance(points[1], points[2]); 
			var c = Point.CalcDistance(points[2], points[0]);
			return a < b + c && b < a + c && c < b + a;
		}
		
		public double CalcPerimeter()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}
			return _points.Select((current, i) => Point.CalcDistance(current, _points[(i + 1) % _points.Length])).Sum();
		}

		public IEnumerable<Point> GetPoints()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}
			return _points;
		}

		public double CalcSquare()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}
			var halfPerimeter = CalcPerimeter() / 2;
			var expression = halfPerimeter;
			for (var i = 0; i < _points.Length; i++)
			{
				expression *= halfPerimeter - Point.CalcDistance(_points[i], _points[(i + 1) % _points.Length]);
			}
			return Math.Sqrt(expression);
		}

		public bool ReadFromStream(ref StreamReader streamReader)
		{
			var points = Helpers.ReadPointsFromStream(
				ref streamReader, @"Triangle\{\s*((-?\d+\s+){5}-?\d+)\s*\}", CoordinatesPerPoint, PointsCount
			);
			
			if (points == null)
			{
				return false;
			}

			if (!PointsAreValid(points))
			{
				return false;
			}

			_points = points.ToArray();

			return true;
		}

		public void WriteToStream(ref StreamWriter streamWriter)
		{
			streamWriter.WriteLine(this);
		}
		
		public override string ToString()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}
			var result = "Triangle:";
			for (var i = 0; i < _points.Length; i++)
			{
				result += $"\n  Point {i}: x={_points[i].X}, y={_points[i].Y}";
			}

			return result;
		}
	}
}
