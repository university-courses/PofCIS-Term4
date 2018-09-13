using System.IO;

namespace Task1CS
{
	public static class Helpers
	{
		public static double StringToDouble(string strNum)
		{
			if (!double.TryParse(strNum, out var doubleNum))
			{
				throw new InvalidDataException("string contains non-number chars");
			}

			return doubleNum;
		}
	}
}