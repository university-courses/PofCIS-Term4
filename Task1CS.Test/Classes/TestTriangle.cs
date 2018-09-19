using System;
using System.Collections.Generic;
using Task1CS.Classes;
using Xunit;

namespace Task1CS.Test.Classes
{
    public class TestTriangle
    {
        [Theory]
        [MemberData(nameof(TriangleData.CalcPerimetrData), MemberType = typeof(TriangleData))]
        public void TestCalcPerimetr(Triangle actual, double expected)
        {
            Assert.Equal(expected, actual.CalcPerimeter());
        }

        [Theory]
        [MemberData(nameof(TriangleData.CalcSquareData), MemberType = typeof(TriangleData))]
        public void TestCalcSquare(Triangle actual, double expected)
        {
            Assert.Equal(expected, actual.CalcSquare());
        }


        private class TriangleData
        {
            public static IEnumerable<object[]> CalcPerimetrData => new List<object[]>
            {
                new object[]
                {
                    new Triangle(new Point[]
                    {
                        new Point(0, 0), new Point(1, 1),new Point(3, 0)
                    }),
                    Math.Sqrt(2) + 3 + Math.Sqrt(5)
                },
                new object[]
                {
                    new Triangle(new Point[]
                    {
                        new Point(0, 0), new Point(1, 1), new Point(0, 3)
                    }),
                    Math.Sqrt(2) + 3 + Math.Sqrt(5)
                },
                new object[]
                {
                    new Triangle(new Point[]
                    {
                        new Point(0, 0), new Point(0, 3), new Point(4,0)
                    }),
                    6
                },
            };

            public static IEnumerable<object[]> CalcSquareData => new List<object[]>
            {
                new object[]
                {
                    new Triangle(new Point[]
                    {
                        new Point(0, 0), new Point(0, 2),new Point(2, 0)
                    }),
                    2
                },
                new object[]
                {
                    new Triangle(new Point[]
                    {
                        new Point(0, 0), new Point(0, 2), new Point(10, 0)
                    }),
                    10
                },
                new object[]
                {
                    new Triangle(new Point[]
                    {
                        new Point(0, 0), new Point(0, 3), new Point(4,0)
                    }),
                    12
                },
            };


            public static IEnumerable<object[]> ParseData => new List<object[]>
            {
                new object[]
                {
                    "Triangle{0 0 1 1 2 0}", true
                },

                new object[]
                {
                "Triangle {0 0 1 1 2 0}", false
                },

                new object[]
                {
                    "Triangle { 0 0 1 1 2 0}", false
                },

                new object[]
                {
                    "Triangle{ 0 0 1 1 2 0}", true
                },

                new object[]
                {
                    "Triangle{0 0 1 1 2 0 }", true
                },
                
                new object[]
                {
                    "0, 0, 1, 1, 2, 0", false
                },
                new object[]
                {
                    "", false
                },
                new object[]
                {
                    "Triangle{0, 0, 1, 1, 2, 0}", false
                },
            };

        }
    }
}
