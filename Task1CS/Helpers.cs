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
		
		public struct Const
		{
			private const string DataRoot = "../../Data/";
			public const string InputDataRoot = DataRoot + "Input/";
			public const string OutputDataRoot = DataRoot + "Output/";
		}
	}
}