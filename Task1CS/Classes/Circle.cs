using System;
using System.IO;
using System.Collections.Generic;
using Task1CS.Interfaces;

namespace Task1CS.Classes
{ 
     /// <inheritdoc />
     /// <summary>
     /// Class to represent circle shape.
     /// </summary>
    public class Circle : IShape
	{
		/// <summary>
        /// Array of points to set a circle.
        /// </summary>
		private Point[] points;

		/// <summary>
        /// Value to represent a radius of a circle.
        /// </summary>
		private double radius;

		/// <summary>
        /// Value of points, required to set a circle. 
        /// </summary>
		private const int PointsCount = 2;

		/// <summary>
		/// Initializes a new instance of the <see cref = "Circle"/> class.
		/// </summary>
		public Circle()
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref = "Circle"/> class.
        /// </summary>
        /// <param name="points">Array of points.
        /// First point mut be center,
        /// second point from the circumference.</param>
        /// <exception cref="NullReferenceException">Throws if array of points is null.</exception>
        /// <exception cref="InvalidDataException">Trows if both points are the same.</exception>
        public Circle(Point[] points)
		{
			if (points == null)
			{
				throw new NullReferenceException("points is null");
			}
			
			if (points[0].Equals(points[1]))
			{
				throw new InvalidDataException("no circle entered");
			}

			this.points = points;
			this.radius = this.CalcRadius();
		}

		/// <summary>
		/// Function to calculate radius of the circle. 
		/// </summary>
		/// <returns>Radius of the circle.</returns>
		/// <exception cref="NullReferenceException">Throws if array of points is null.</exception>
		public double CalcRadius()
		{
			if (this.points == null)
			{
				throw new NullReferenceException("points not set");
			}

			return Point.CalcDistance(this.points[0], this.points[1]);
		}

		/// <inheritdoc />
		/// <summary>
		/// Function to parse string of data into the element of circle class.
		/// </summary>
		/// <param name="line">Line to parse.</param>
		/// <returns>True if line was parsed,
		/// false if line was not parsed, or if parsed points are the same.</returns>
		public bool Parse(string line)
		{
			var parsedPoints = Helpers.ParseShapePoints(
				line, @"Circle\{\s*((-?\d+\s+){3}-?\d+)\s*\}", Helpers.Const.CoordinatesPerPoint, PointsCount);

			if (parsedPoints == null)
			{
				return false;
			}

			if (parsedPoints[0].Equals(parsedPoints[1]))
			{
				return false;
			}

			this.points = parsedPoints;
			this.radius = this.CalcRadius();

			return true;
		}

		/// <inheritdoc />
		/// <summary>
		/// Function that calculates square of the circle.
		/// </summary>
		/// <returns>Square.</returns>
		/// <exception cref="T:System.NullReferenceException">Throws if array of points is null.</exception>
		public double CalcSquare()
		{
			if (this.points == null)
			{
				throw new NullReferenceException("points not set");
			}

			return Math.PI * Math.Pow(this.radius, 2);
		}

		/// <inheritdoc />
		/// <summary>
		/// Function that calculates perimeter of the circle.
		/// </summary>
		/// <returns>Perimeter.</returns>
		/// <exception cref="T:System.NullReferenceException">Throws if array of points is null.</exception>
		public double CalcPerimeter()
		{
			if (this.points == null)
			{
				throw new NullReferenceException("points not set");
			}

			return 2 * Math.PI * this.radius;
		}

		/// <inheritdoc />
		/// <summary>
		/// Function to get points of center and from the circumference.
		/// </summary>
		/// <returns>Array of points.</returns>
		/// <exception cref="T:System.NullReferenceException">Throws if array of points is null.</exception>
		public IEnumerable<Point> GetPoints()
		{
			if (this.points == null)
			{
				throw new NullReferenceException("points not set");
			}

			return this.points;
		}
		
		/// <summary>
		/// Function to convert element of circle class to string.
		/// </summary>
		/// <returns>Text interpretation of the element of circle class.</returns>
		/// <exception cref="NullReferenceException">Throws if array of points is null.</exception>
		public override string ToString()
		{
			if (this.points == null)
			{
				throw new NullReferenceException("points not set");
			}

			var result = $"Circle:\n  Radius: {this.radius}\n  Points:";
			for (var i = 0; i < this.points.Length; i++)
			{
				result += $"\n    Point {i}: x={this.points[i].X}, y={this.points[i].Y}";
			}

			return result;
		}
	}
}
