using System.Collections.Generic;

using Task1CS.Interfaces;

namespace Task1CS.Classes
{
	public class Square : IShape
	{
		public bool Parse(string line)
		{
			return false;
		}

		public double CalcSquare()
		{
			throw new System.NotImplementedException();
		}

		public double CalcPerimeter()
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<Point> GetPoints()
		{
			throw new System.NotImplementedException();
		}
	}
}
