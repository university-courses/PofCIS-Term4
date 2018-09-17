using System.IO;
using System.Collections.Generic;
using System.Linq;

using Task1CS.Classes;
using Task1CS.Interfaces;

namespace Task1CS.BL
{
	public static class Task
	{
		/// <summary>
		/// Solve the main task:
		///  - reads shapes from file to List;
		///  - sorts the collection by their squares in ascending order;
		///  - outputs the result to a 'SortedBySquares.txt' file;
		///  - finds shapes which are located in the third quarter of coordinate system;
		///  - sorts found shapes by their perimeters in descending order;
		///  - outputs sorted shapes to a 'SortedByPerimeters.txt' file.
		/// </summary>
		public static void Execute()
		{
			if (!Directory.Exists(Helpers.Const.OutputDataRoot))
			{
				Directory.CreateDirectory(Helpers.Const.OutputDataRoot);
			}
			
			var shapes = ReadFromFile(Helpers.Const.InputDataRoot + "Shapes.txt");

			var sortedBySquares = shapes.OrderBy(shape => shape.CalcSquare());

			WriteToFile(Helpers.Const.OutputDataRoot + "SortedBySquares.txt", sortedBySquares);

			var shapesFromThirdQuarter = new List<IShape>();
			foreach (var shape in shapes)
			{
				if (IsInThirdQuarter(shape))
				{
					shapesFromThirdQuarter.Add(shape);
				} 
			}

			var sortedByPerimeters = shapesFromThirdQuarter.OrderByDescending(shape => shape.CalcPerimeter());

			WriteToFile(Helpers.Const.OutputDataRoot + "SortedByPerimeters.txt", sortedByPerimeters);
		}
		
		/// <summary>
		/// Reads a list of shapes from file.
		/// </summary>
		/// <param name="fileName">Path to file with shapes data.</param>
		/// <returns>List of shapes</returns>
		/// <exception cref="IOException">Throws if given file does not exist.</exception>
		public static List<IShape> ReadFromFile(string fileName)
		{
			if (!File.Exists(fileName))
			{
				throw new IOException("file does not exist");
			}

			var reader = new StreamReader(fileName);
			var list = new List<IShape>();

			while (!reader.EndOfStream)
			{
				var triangle = new Triangle();
				var square = new Square();
				var circle = new Circle();
				if (triangle.ReadFromStream(ref reader))
				{
					list.Add(triangle);	
				}
				else if (square.ReadFromStream(ref reader))
				{
					list.Add(square);
				}
				else if (circle.ReadFromStream(ref reader))
				{
					list.Add(circle);
				}
			}

			return list;
		}

		/// <summary>
		/// Writes a list of shapes to file.
		/// </summary>
		/// <param name="fileName">Path to file.</param>
		/// <param name="shapes">A list of shapes.</param>
		public static void WriteToFile(string fileName, IEnumerable<IShape> shapes)
		{
			var writer = new StreamWriter(fileName);
			foreach (var shape in shapes)
			{
				shape.WriteToStream(ref writer);
			}

			writer.Close();
		}

		/// <summary>
		/// Checks if shape is located in the third quarter of coordinate system.
		/// </summary>
		/// <param name="shape">Shape to check</param>
		/// <returns>True if shape is located in the third quarter, otherwise returns false.</returns>
		public static bool IsInThirdQuarter(IShape shape)
		{
			return shape.GetPoints().All(point => !(point.X > 0) && !(point.Y > 0));
		}
	}
}
