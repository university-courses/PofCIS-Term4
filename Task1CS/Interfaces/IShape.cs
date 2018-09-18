using System.Collections.Generic;

using Task1CS.Classes;

namespace Task1CS.Interfaces
{
	public interface IShape : IParsable
	{
		double CalcSquare();
		double CalcPerimeter();
		IEnumerable<Point> GetPoints();
	}
}
