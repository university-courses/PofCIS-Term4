using System;

namespace Task1CS.Classes
{
    /// <summary>
    /// Class to represent a point in two-dimensional space.
    /// </summary>
    public class Point
    {
		/// <summary>
        /// Gets first coordinate.
        /// </summary>
        public double X { get; }

	    /// <summary>
	    /// Gets second coordinate.
	    /// </summary>
        public double Y { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref = "Point"/> class.
        /// </summary>
        /// <param name="x">x-axis value.</param>
        /// <param name="y">y-axis value.</param>
        public Point(double x, double y)
        {
	        this.X = x;
	        this.Y = y;
        }
        
        /// <summary>
        /// Function to calculate distance between the two points. 
        /// </summary>
        /// <param name="first">First point.</param>
        /// <param name="second">Second point.</param>
        /// <returns>Distance between two points.</returns>
        public static double CalcDistance(Point first, Point second)
        {
            return Math.Sqrt(Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2));
        }

        /// <summary>
        /// Function that compares coordinates of two points to check if they are the same
        /// </summary>
        /// <param name="point">Point to compare with.</param>
        /// <returns>True if both coordinates are the same.</returns>
        public bool Equals(Point point)
        {
            return (Math.Abs(this.X - point.X) < 0.01) && (Math.Abs(this.Y - point.Y) < 0.01);
        }
    }
}
