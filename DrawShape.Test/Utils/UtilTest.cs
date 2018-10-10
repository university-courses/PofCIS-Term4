using Xunit;

using System;
using System.IO;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Shapes;
using DrawShape.Utils;
using DrawShape.Classes;

namespace DrawShape.Test.Utils
{
    public class UtilTest
    {
        [Theory]
        [MemberData(nameof(ConstructorData.SuccessData), MemberType = typeof(ConstructorData))]
        public void TestOnSegment(System.Windows.Point p, System.Windows.Point q, System.Windows.Point r)
        {
            Assert.True(Util.OnSegment(p, q, r));
        }

        [Theory]
        [MemberData(nameof(ConstructorData.SuccessDataPoints), MemberType = typeof(ConstructorData))]
        public void TestAreSidesIntersected(System.Windows.Point firstSidePointOne,
                                            System.Windows.Point firstSidePointTwo,
                                            System.Windows.Point secondSidePointOne,
                                            System.Windows.Point secondSidePointTwo)
        {
            Assert.True(Util.AreSidesIntersected(firstSidePointOne,firstSidePointTwo,secondSidePointOne,secondSidePointTwo));
        }

        private class ConstructorData
        {
            public static IEnumerable<object[]> SuccessData => new List<object[]>
            {
                new object[]
                {
                   new System.Windows.Point(10,0),
                   new System.Windows.Point(18,0),
                   new System.Windows.Point(30,0)
                },
                new object[]
                {
                    new System.Windows.Point(0,10),
                    new System.Windows.Point(0,18),
                    new System.Windows.Point(0,30)
                }
            };

            public static IEnumerable<object[]> SuccessDataPoints => new List<object[]>
            {
                new object[]
                {
                    new System.Windows.Point(0,0),
                    new System.Windows.Point(30,0),
                    new System.Windows.Point(2,12),
                    new System.Windows.Point(5,-12)
                }

            };


        }
    }
}
