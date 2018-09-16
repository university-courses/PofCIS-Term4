using System.IO;
using System.Collections.Generic;
using System.Linq;

using Task1CS.Classes;
using Task1CS.Interfaces;

namespace Task1CS.BL
{
	public static class Task
	{
		public static void Execute()
		{
			if (!Directory.Exists(Helpers.Const.OutputDataRoot))
			{
				Directory.CreateDirectory(Helpers.Const.OutputDataRoot);
			}
			
			// read shapes from file to List collection
			var shapes = ReadFromFile(Helpers.Const.InputDataRoot + "Shapes.txt");

			// sort by squares in ascending order
			var sortedBySquares = shapes.OrderBy(shape => shape.CalcSquare());

			// write sorted data to file1
			WriteToFile(Helpers.Const.OutputDataRoot + "SortedBySquares.txt", sortedBySquares);

			// find shapes which are located in the third quarter of coordinate system
			var shapesFromThirdQuarter = new List<IShape>();
			foreach (var shape in shapes)
			{
				if (IsInForthQuarter(shape))
				{
					shapesFromThirdQuarter.Add(shape);
				} 
			}

			// sort this collection by their perimeters in descending order
			var sortedByPerimeters = shapesFromThirdQuarter.OrderByDescending(shape => shape.CalcPerimeter());

			// write sorted data to a file2
			WriteToFile(Helpers.Const.OutputDataRoot + "SortedByPerimeters.txt", sortedByPerimeters);
		}
		
		public static List<IShape> ReadFromFile(string fileName)
		{
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

		public static void WriteToFile(string fileName, IEnumerable<IShape> shapes)
		{
			var writer = new StreamWriter(fileName);
			foreach (var shape in shapes)
			{
				shape.WriteToStream(ref writer);
			}

			writer.Close();
		}

		public static bool IsInForthQuarter(IShape shape)
		{
			return shape.GetPoints().All(point => !(point.X > 0) && !(point.Y > 0));
		}
	}
}
