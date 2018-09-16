using System.Collections.Generic;
using System.IO;

using Task1CS.Interfaces;

namespace Task1CS.Classes
{
	public class Square : IShape
	{
		public void WriteToStream(ref StreamWriter streamWriter)
		{
			throw new System.NotImplementedException();
		}

		public bool ReadFromStream(ref StreamReader streamReader)
		{
			throw new System.NotImplementedException();
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
