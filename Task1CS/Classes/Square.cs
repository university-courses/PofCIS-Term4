using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

using Task1CS.Interfaces;

namespace Task1CS.Classes
{
	public class Square : IShape
	{
		private Point[] _points;

		private const int PointsCount = 4;

		public Square()
		{
		}

		public Square(Point[] points)
		{
			if (points == null)
			{
				throw new NullReferenceException("points is null");
			}

			if (points.Length != PointsCount)
			{
				throw new InvalidDataException("invalid square points count");
			}

			if (!PointsAreValid(points))
			{
				throw new InvalidDataException("invalid square");
			}

			_points = points;
		}

		public static bool PointsAreValid(IReadOnlyList<Point> points)
		{
			if (points == null)
			{
				throw new NullReferenceException("points is null");
			}

			if (points.Count != PointsCount)
			{
				return false;
			}

			var a = Point.CalcDistance(points[0], points[1]);
			var b = Point.CalcDistance(points[1], points[2]);
			var c = Point.CalcDistance(points[2], points[3]);
			var d = Point.CalcDistance(points[3], points[0]);
			return a.Equals(b) && b.Equals(c) && c.Equals(d) && d.Equals(a);
		}

		public double CalcPerimeter()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}

			return Point.CalcDistance(_points[0], _points[1]) * 4;
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

			return Math.Pow(Point.CalcDistance(_points[0], _points[1]), 2);
		}

		public override string ToString()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}
			var result = "Square:";
			for (var i = 0; i < _points.Length; i++)
			{
				result += $"\n  Point {i}: x={_points[i].X}, y={_points[i].Y}";
			}

			return result;
		}
		
		public bool Parse(string line)
		{
			var points = Helpers.ParseShapePoints(
				line, @"Square\{\s*((-?\d+\s+){7}-?\d+)\s*\}", Helpers.Const.CoordinatesPerPoint, PointsCount
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

	}
}
