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
        public void TestPointIsInHexagon(Point point, Polygon hexagon)
        {
            
        }

        private class ConstructorData
        {
            public static IEnumerable<object[]> SuccessData => new List<object[]>
            {
                new object[]
                {
                   new Point(0, 0), new Polygon()
                },
                
            };
        }
    }
}
