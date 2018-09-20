using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Task1CS.Interfaces;

namespace Task1CS.Classes
{
	/// <inheritdoc />
	/// <summary>
	/// Class to represent square shape.
	/// </summary>
	public class Square : IShape
	{
		/// <summary>
		/// Array of points to set a square.
		/// </summary>
		private Point[] _points;

		/// <summary>
		/// Value of points, required to set a circle. 
		/// </summary>
		private const int PointsCount = 4;

		/// <summary>
		/// Initializes a new instance of the <see cref = "Square"/> class.
		/// </summary>
		public Square()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref = "Square"/> class.
		/// </summary>
		/// <param name="points">Array of points to set the square.</param>
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

		/// <summary>
		/// Function to check whether given coordinates will set a square.
		/// </summary>
		/// <param name="points">Array of points to set the square.</param>
		/// <returns>True if square can be set with given points
		/// false if array of points is null, of if the amount of points does not equal the amount required.</returns>
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
		
		/// <inheritdoc />
		/// <summary>
		/// Function to calculate the perimeter of the square.
		/// </summary>
		/// <returns>Perimeter of the shape.</returns>
		public double CalcPerimeter()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}

			return Point.CalcDistance(_points[0], _points[1]) * 4;
		}
		
		/// <inheritdoc />
		/// <summary>
		/// Function to get points of vertices of the square.
		/// </summary>
		/// <returns>Array of points.</returns>
		/// <exception cref="T:System.NullReferenceException">Throws if array of points is null.</exception>
		public IEnumerable<Point> GetPoints()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}

			return _points;
		}

		/// <inheritdoc />
		/// <summary>
		/// Function that calculates square of the square.
		/// </summary>
		/// <returns>Square.</returns>
		/// <exception cref="T:System.NullReferenceException">Throws if array of points is null.</exception>
		public double CalcSquare()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}

			return Math.Pow(Point.CalcDistance(_points[0], _points[1]), 2);
		}

		/// <summary>
		/// Function to convert element of square class to string.
		/// </summary>
		/// <returns>Text interpretation of the element of square class.</returns>
		/// <exception cref="NullReferenceException">Throws if array of points is null.</exception>
		public override string ToString()
		{
			if (_points == null)
			{
				throw new NullReferenceException("points not set");
			}

			var result = "Square:";
			for (var i = 0; i < _points.Length; i++)
			{
				result += $"{Environment.NewLine}  Point {i}: x={_points[i].X}, y={_points[i].Y}";
			}

			return result;
		}

		/// <inheritdoc />
		/// <summary>
		/// Function to parse string of data into the element of square class.
		/// </summary>
		/// <param name="line">Line to parse.</param>
		/// <returns>True if line was parsed,
		/// false if line was not parsed, or if parsed points are not valid.</returns>
		public bool Parse(string line)
		{
			var parsedPoints = Helpers.ParseShapePoints(
				line, @"Square\{\s*((-?\d+\s+){7}-?\d+)\s*\}", Helpers.Const.CoordinatesPerPoint, PointsCount);

			if (parsedPoints == null)
			{
				return false;
			}

			if (!PointsAreValid(parsedPoints))
			{
				return false;
			}

			_points = parsedPoints.ToArray();

			return true;
		}
	}
}
