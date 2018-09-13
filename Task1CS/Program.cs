using System;
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
			var streamReader = new StreamReader("../../ShapesData.txt");
			var list = new List<IShape>();

			while (!streamReader.EndOfStream)
			{
				var triangle = new Triangle();
				if (triangle.ReadFromFile(ref streamReader))
				{
					list.Add(triangle);	
				}
				else
				{
					//add circle and square.
				}
			}

			var streamWriter = new StreamWriter("../../ResultData.txt");
			foreach (var shape in list)
			{
				shape.WriteToFile(ref streamWriter);
			}
			streamWriter.Close();
        }
	}
}
