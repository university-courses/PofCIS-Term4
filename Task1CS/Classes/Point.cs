using System;
using System.IO;

namespace Task1CS.Classes
{
    public struct Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point(string x, string y)
        {
            X = _stringToDouble(x);
            Y = _stringToDouble(y);
        }

        private static double _stringToDouble(string strNum)
        {
            if (!double.TryParse(strNum, out var doubleNum))
            {
                throw new InvalidDataException("string contains non-number chars");
            }

            return doubleNum;
        }
        
        public static double CalcDistance(Point first, Point second)
        {
            return Math.Sqrt(Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2));
        }
    }
}
