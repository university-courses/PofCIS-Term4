using System;
using System.Collections.Generic;
using Task1CS.Classes;
using Xunit;

namespace Task1CS.Test.Classes
{
	public class TestCircle
	{
		[Theory]
		[MemberData(nameof(CircleData.CalcRadiusData), MemberType = typeof(CircleData))]
		public void TestCalcRadius( Circle actual, double expected)
		{
			
			Assert.Equal(expected, actual.CalcRadius());
			Assert.Equal(expected, actual.CalcRadius());
		}
		
		[Theory]
		[MemberData(nameof(CircleData.ParseData), MemberType = typeof(CircleData))]
		public void TestParse( string line, bool expected)
		{
			var actual = new Circle();
			Assert.Equal(expected, actual.Parse(line));
			Assert.Equal(expected, actual.Parse(line));
		}
		
		private class CircleData
		{
			public static IEnumerable<object[]> CalcRadiusData => new List<object[]>
			{
				new object[]
				{
					new Circle(new Point[]
					{
						new Point(0, 0), new Point(0, 1)
					}),
					1
				},
				new object[]
				{
					new Circle(new Point[]
					{
						new Point(0, 0), new Point(1, 1)
					}),
					Math.Sqrt(2)
				},
				new object[]
				{
					new Circle(new Point[]
					{
						new Point(0, 0), new Point(4, 4)
					}),
					Math.Sqrt(32)
				},
				new object[]
				{
					new Circle(new Point[]
					{
						new Point(0, 0), new Point(5, 0)
					}),
					5
				},
				new object[]
				{
					new Circle(new Point[]
					{
						new Point(1, 0), new Point(0, 1)
					}),
					Math.Sqrt(2)
				},
			};

			public static IEnumerable<object[]> ParseData => new List<object[]>
			{
				new object[]
				{
					"Circle{0 0 1 1}", true	
				},
				
				new object[]
				{
				"Circle {0 0 1 1}", false	
				},
				
				new object[]
				{
					"Circle { 0 0 1 1}", false	
				},
				
				new object[]
				{
					"Circle{ 0 0 1 1}", true	
				},
				
				new object[]
				{
					"Circle{0 0 1 1 }", true	
				},
				
				new object[]
				{
				"Triangle{5 0 0 1 1 5}", false	
				},
				new object[]
				{
					"0, 0, 1, 1", false	
				},
				new object[]
				{
					"", false	
				},
				new object[]
				{
					"Circle{0, 0, 1, 0}", false	
				},
				new object[]
				{
					"Circle{0 0 0 0}", false	
				},
			};

		}
		
	}
}