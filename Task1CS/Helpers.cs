using System.IO;

namespace Task1CS
{
	/// <summary>
	/// Contains helper method 'StringToDouble(string)'.
	/// </summary>
	public static class Helpers
	{
		/// <summary>
		/// Converts string number to double.
		/// </summary>
		/// <param name="strNum">String which contains floating point number.</param>
		/// <returns></returns>
		/// <exception cref="InvalidDataException">Throws if string contains non-numeric symbol(s).</exception>
		public static double StringToDouble(string strNum)
		{
			if (!double.TryParse(strNum, out var doubleNum))
			{
				throw new InvalidDataException("string contains non-number chars");
			}

			return doubleNum;
		}
		/// <summary>
		/// Holds constant values.
		/// </summary>
		public struct Const
		{
			/// <summary>
			/// Path to input/output data directory.
			/// </summary>
			private const string DataRoot = "../../Data/";
			
			/// <summary>
			/// Path to input files directory.
			/// </summary>
			public const string InputDataRoot = DataRoot + "Input/";
			
			/// <summary>
			/// Path to output files directory.
			/// </summary>
			public const string OutputDataRoot = DataRoot + "Output/";
		}
	}
}
