using System.IO;
using Task1CS.Interfaces;
using Task1CS.Classes;
using System.Collections.Generic;

namespace Task1CS
{
	internal static class Program
	{
		public static void Main(string[] args)
		{
			var streamReader = new StreamReader("../../Data/ShapesData.txt");
			var list = new List<IShape>();

			while (!streamReader.EndOfStream)
			{
				var triangle = new Triangle();
				if (triangle.ReadFromStream(ref streamReader))
				{
					list.Add(triangle);	
				}
				else
				{
					//add circle and square.
				}
			}

			var streamWriter = new StreamWriter("../../Data/ResultData.txt");
			foreach (var shape in list)
			{
				shape.WriteToStream(ref streamWriter);
			}
			streamWriter.Close();
        }
	}
}
