using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1CS.Interfaces;

namespace Task1CS.Classes
{
    public class Triangle : IShape, IFileManager
    {
        Point pointA;
        Point pointB;
        Point pointC;




        //TODO: change location of this function, as it will be necessary for other classes.
        double getLineLength(Point firstPoint, Point lastPoint)
        {
            return Math.Sqrt(Math.Pow(firstPoint.X - lastPoint.X, 2) + Math.Pow(firstPoint.Y - lastPoint.Y, 2));
        }

        public double PerimeterCount()
        {
            return getLineLength(pointA, pointB) + getLineLength(pointB, pointC) + getLineLength(pointC, pointA);
        }

        public double SquareCount()
        {
            var halfPerimeter = PerimeterCount() / 2;
            return Math.Sqrt(halfPerimeter * (halfPerimeter - getLineLength(pointA, pointB))
                                           * (halfPerimeter - getLineLength(pointB, pointC))
                                           * (halfPerimeter - getLineLength(pointC, pointA)));
        }


        public void ReadFile(StreamReader streamReader)
        {
            var line = streamReader.ReadLine();
            string[] data = line.Split(' ');

            pointA.X = Convert.ToDouble(data[1]);
            pointA.Y = Convert.ToDouble(data[2]);

            pointB.X = Convert.ToDouble(data[3]);
            pointB.Y = Convert.ToDouble(data[4]);

            pointC.X = Convert.ToDouble(data[5]);
            pointC.Y = Convert.ToDouble(data[6]);

        }

        public void WriteFile(StreamWriter streamWriter)
        {
            streamWriter.WriteLine("Triangle :");
            streamWriter.WriteLine($"A({pointA.X}, {pointA.Y})");
            streamWriter.WriteLine($"B({pointB.X}, {pointB.Y})");
            streamWriter.WriteLine($"C({pointC.X}, {pointC.Y})");
        }
    }
}
