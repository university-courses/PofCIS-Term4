using System.Collections.Generic;

using Task1CS.Classes;

namespace Task1CS.Interfaces
{
	/// <inheritdoc />
	/// <summary>
	/// Interface for shapes. 
	/// </summary>
	public interface IShape : IParsable
	{
        /// <summary>
        /// Function used to calculate shape's square.
        /// </summary>
        /// <returns>Square of the shape.</returns>
        double CalcSquare();
        
        /// <summary>
        /// Function used to calculate shape's perimeter.
        /// </summary>
        /// <returns>Perimeter of shape.</returns>
        double CalcPerimeter();
		
        /// <summary>
		/// Function used to get shape's points.
		/// </summary>
		/// <returns>Array of points.</returns>
		IEnumerable<Point> GetPoints();
	}
}
