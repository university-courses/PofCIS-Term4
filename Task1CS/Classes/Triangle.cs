using System;
using System.IO;
using Task1CS.Interfaces;

namespace Task1CS.Classes
{
    public class Triangle : IShape
    {
        private Point _pointA;
        private Point _pointB;
        private Point _pointC;
        
        //TODO: change location of this function, as it will be necessary for other classes.
        double getLineLength(Point startPoint, Point endPoint)
        {
            return Math.Sqrt(Math.Pow(startPoint.X - endPoint.X, 2) + Math.Pow(startPoint.Y - endPoint.Y, 2));
        }

        public double CalcPerimeter()
        {
            return getLineLength(_pointA, _pointB) + getLineLength(_pointB, _pointC) + getLineLength(_pointC, _pointA);
        }

        public double CalcSquare()
        {
            var halfPerimeter = CalcPerimeter() / 2;
            return Math.Sqrt(halfPerimeter * (halfPerimeter - getLineLength(_pointA, _pointB))
                                           * (halfPerimeter - getLineLength(_pointB, _pointC))
                                           * (halfPerimeter - getLineLength(_pointC, _pointA)));
        }


        public void ReadFromFile(ref StreamReader streamReader)
        {
            var line = streamReader.ReadLine();

            if (line == null)
            {
                throw new Exception("can't read data from stream");
                // TODO: implement StreamReaderException
            }
            
            var data = line.Split(' ');

            _pointA.X = Convert.ToDouble(data[1]);
            _pointA.Y = Convert.ToDouble(data[2]);

            _pointB.X = Convert.ToDouble(data[3]);
            _pointB.Y = Convert.ToDouble(data[4]);

            _pointC.X = Convert.ToDouble(data[5]);
            _pointC.Y = Convert.ToDouble(data[6]);
        }

        public void WriteToFile(ref StreamWriter streamWriter)
        {
            streamWriter.WriteLine("Triangle :");
            streamWriter.WriteLine($"A({_pointA.X}, {_pointA.Y})");
            streamWriter.WriteLine($"B({_pointB.X}, {_pointB.Y})");
            streamWriter.WriteLine($"C({_pointC.X}, {_pointC.Y})");
        }
    }
}
