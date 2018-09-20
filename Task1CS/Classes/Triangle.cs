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
    public class Triangle : IShape
    {
        /// <summary>
        /// Array of points to set a triangle.
        /// </summary>
        private Point[] points;

        /// <summary>
        /// Value of points, required to set a circle. 
        /// </summary>
        private const int PointsCount = 3;

        /// <summary>
        /// Initializes a new instance of the <see cref = "Triangle"/> class.
        /// </summary>
        public Triangle()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "Triangle"/> class.
        /// </summary>
        /// <param name="points">Array of points to set the triangle.</param>
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

            this.points = points;
        }

        /// <summary>
        /// Function to check whether given coordinates will set a triangle
        /// </summary>
        /// <param name="points">Array of points to set the square.</param>
        /// <returns>True if square can be set with given points
        /// false if array of points is null, of if the amount of points does not equal the amount required</returns>
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

        /// <inheritdoc />
        /// <summary>
        /// Function to calculate the perimeter of the triangle
        /// </summary>
        /// <returns>Perimeter of the shape.</returns>
        public double CalcPerimeter()
        {
            if (this.points == null)
            {
                throw new NullReferenceException("points not set");
            }

            return this.points.Select((current, i) => Point.CalcDistance(current, this.points[(i + 1) % this.points.Length])).Sum();
        }

        /// <inheritdoc />
        /// <summary>
        /// Function to get points of vertices of the triangle.
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

        /// <inheritdoc />
        /// <summary>
        /// Function that calculates square of the triangle.
        /// </summary>
        /// <returns>Square.</returns>
        /// <exception cref="T:System.NullReferenceException">Throws if array of points is null.</exception>
        public double CalcSquare()
        {
            if (this.points == null)
            {
                throw new NullReferenceException("points not set");
            }

            var halfPerimeter = this.CalcPerimeter() / 2;
            var expression = halfPerimeter;
            for (var i = 0; i < this.points.Length; i++)
            {
                expression *= halfPerimeter - Point.CalcDistance(this.points[i], this.points[(i + 1) % this.points.Length]);
            }

            return Math.Sqrt(expression);
        }

        /// <inheritdoc />
        /// <summary>
        /// Function to parse string of data into the element of triangle class.
        /// </summary>
        /// <param name="line">Line to parse.</param>
        /// <returns>True if line was parsed,
        /// false if line was not parsed, or if parsed points are not valid.</returns>
        public bool Parse(string line)
        {
            var parsedPoints = Helpers.ParseShapePoints(
                line, @"Triangle\{\s*((-?\d+\s+){5}-?\d+)\s*\}", Helpers.Const.CoordinatesPerPoint, PointsCount);

            if (parsedPoints == null)
            {
                return false;
            }

            if (!PointsAreValid(parsedPoints))
            {
                return false;
            }

            this.points = parsedPoints.ToArray();

            return true;
        }

        /// <summary>
        /// Function to convert element of triangle class to string.
        /// </summary>
        /// <returns>Text interpretation of the element of triangle class.</returns>
        /// <exception cref="NullReferenceException">Throws if array of points is null.</exception>
        public override string ToString()
        {
            if (this.points == null)
            {
                throw new NullReferenceException("points not set");
            }

            var result = "Triangle:";
            for (var i = 0; i < this.points.Length; i++)
            {
                result += $"\n  Point {i}: x={this.points[i].X}, y={this.points[i].Y}";
            }

            return result;
        }
    }
}
