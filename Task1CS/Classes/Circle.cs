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

		public double CalcRadius()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}

			return Point.CalcDistance(_points[0], _points[1]);
		}
		
		public void WriteToStream(ref StreamWriter streamWriter)
		{
			streamWriter.WriteLine(this);
		}

		public bool ReadFromStream(ref StreamReader streamReader)
		{
			var points = Helpers.ReadPointsFromStream(
				ref streamReader,@"Circle\{\s*((-?\d+\s+){3}-?\d+)\s*\}", CoordinatesPerPoint, PointsCount
			);

			if (points == null)
			{
				return false;
			}

			if (points[0] == points[1])
			{
				return false;
			}

			_points = points;
			_radius = CalcRadius();

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
		
		public override string ToString()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}
			var result = $"Circle:\n  Radius: {_radius}\n  Points:";
			for (var i = 0; i < _points.Length; i++)
			{
				result += $"\n    Point {i}: x={_points[i].X}, y={_points[i].Y}";
			}

			return result;
		}
	}
}
