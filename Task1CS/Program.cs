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
            StreamReader streamReader = new StreamReader("../../ShapesData.txt");
            var list = new List<IShape>();

            while (!streamReader.EndOfStream)
            {
                string shapeType = Convert.ToString(48 - streamReader.Read());
                if (shapeType == "0")
                {
                    var triangle = new Triangle();
                    triangle.ReadFromFile(ref streamReader);
                    list.Add(triangle);
                }
                else
                {
                    //add circle and square.
                }
            }

            StreamWriter streamWriter = new StreamWriter("../../ResultData.txt");
            foreach (var shape in list)
            {
               //write 
               //TODO: fix it.
            }
            streamWriter.Close();
        }
	}
}
