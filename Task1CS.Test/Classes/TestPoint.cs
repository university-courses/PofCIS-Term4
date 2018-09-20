using System.Collections.Generic;

using Xunit;
using Task1CS.Classes;

namespace Task1CS.Test.Classes
{
	public class TestPoint
	{
		[Theory]
		[MemberData(nameof(PointData.ConstructorData), MemberType = typeof(PointData))]
		public void TestConstructor(double x, double y, Point expected)
		{
			var actual = new Point(x, y);
			Assert.Equal(expected.X, actual.X);
			Assert.Equal(expected.Y, actual.Y);
		}
		
		[Theory]
		[MemberData(nameof(PointData.CalcDistanceData), MemberType = typeof(PointData))]
		public void TestCalcDistance(double x1, double y1, double x2, double y2, string expected)
		{
			var actual = Point.CalcDistance(new Point(x1, y1), new Point(x2, y2));
			Assert.Equal(expected, $"{actual:F10}");
		}

		private class PointData
		{
			public static IEnumerable<object[]> ConstructorData => new List<object[]>
			{
				new object[]
				{
					1, 2, new Point(1, 2) 
				},
				new object[]
				{
					0, 0, new Point(0, 0)
				},
				new object[]
				{
					1, 1, new Point(1, 1)
				}
			};
			
			public static IEnumerable<object[]> CalcDistanceData => new List<object[]>
			{
				new object[]
				{
					1, 2, 0, 0, $"{2.23606797749979:F10}"
				},
				new object[]
				{
					0, 0, 1, 1, $"{1.4142135623731:F10}"
				},
				new object[]
				{
					1, 1, 3, 3, $"{2.82842712474619:F10}"
				},
				new object[]
				{
					1, 1, 1, 1, $"{0.0:F10}"
				}
			};
		}
	}
}