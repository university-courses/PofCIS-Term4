using System.Collections.Generic;
using System.IO;

using Task1CS.Interfaces;

namespace Task1CS.Classes
{
	public class Square : IShape
	{
		public void WriteToStream(ref StreamWriter streamWriter)
		{
			
		}

		public bool ReadFromStream(ref StreamReader streamReader)
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
