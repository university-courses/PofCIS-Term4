using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Task1CS.Classes;

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
		/// Reads points from stream using specific parameters.
		/// </summary>
		/// <param name="reader">Reference to a stream reader.</param>
		/// <param name="regexStr">Regular expression for matching shape's points.</param>
		/// <param name="coordsPerP">Coordinates per point, e.g. (x, y)</param>
		/// <param name="pCount">Amount of shape's points.</param>
		/// <returns>If shape is matched returns an array of points, otherwise returns null.</returns>
		/// <exception cref="IOException">Throws if can not read from stream.</exception>
		/// <exception cref="InvalidDataException">Throws if points count is invalid.</exception>
		public static Point[] ReadPointsFromStream(ref StreamReader reader, string regexStr, int coordsPerP, int pCount)
		{
			var line = reader?.ReadLine();
			if (line == null)
			{
				throw new IOException("can't read data from stream");
			}

			var result = Regex.Match(line, regexStr);
			if (!result.Success)
			{
				return null;
			}

			var data = result.Groups[1].ToString().Split(' ');
			if (data.Length != pCount * coordsPerP)
			{
				throw new InvalidDataException("invalid triangle points count");
			}
			
			var points = new List<Point>();  

			for (var i = 0; i < pCount * coordsPerP; i += coordsPerP)
			{
				points.Add(new Point(StringToDouble(data[i]), StringToDouble(data[i + 1])));
			}

			return points.ToArray();
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
