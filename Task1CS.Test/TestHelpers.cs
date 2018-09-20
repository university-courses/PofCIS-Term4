using System.IO;
using System.Collections.Generic;

using Xunit;
using Task1CS.Classes;

namespace Task1CS.Test
{
	public class TestHelpers
	{
		[Theory]
		[MemberData(nameof(StringToDoubleData.SuccessData), MemberType = typeof(StringToDoubleData))]
		public void TestStringToDoubleSuccess(string input, double expected)
		{
			Assert.Equal(expected, Helpers.StringToDouble(input));
		}
		
		[Theory]
		[MemberData(nameof(StringToDoubleData.ThrowsData), MemberType = typeof(StringToDoubleData))]
		public void TestStringToDoubleThrows(string input)
		{
			Assert.Throws<InvalidDataException>(() => Helpers.StringToDouble(input));
		}

		private class StringToDoubleData
		{
			public static IEnumerable<object[]> SuccessData => new List<object[]>
			{
				new object[] {"10", 10.0},
				new object[] {"0,1", 0.1},
				new object[] {"0", 0.0},
				new object[] {"-10", -10.0},
				new object[] {"1", 1.0},
				new object[] {"-1,0757", -1.0757},
				new object[] {"10987654,977", 10987654.977},
			};
			
			public static IEnumerable<object[]> ThrowsData => new List<object[]>
			{
				new object[] {" "},
				new object[] {"a"},
				new object[] {"10.0f"},
				new object[] {"-"},
				new object[] {"-f"},
			};
		}
		
		[Theory]
		[MemberData(nameof(ReadPointsFromFileData.SuccessData), MemberType = typeof(ReadPointsFromFileData))]
		public void TestReadShapePointsSuccess(string input, string regex, int cpp, int pc, Point[] expected)
		{
			var actual = Helpers.ParseShapePoints(input, regex, cpp, pc);

			for (var i = 0; i < actual.Length; i++)
			{
				Assert.Equal(expected[i].X, actual[i].X);
				Assert.Equal(expected[i].Y, actual[i].Y);
			}
		}
		
		[Fact]
		public void TestParseShapePointsThrowsIoException()
		{
			Assert.Throws<IOException>(() => Helpers.ParseShapePoints(null, "", 0, 0));
		}
		
		[Fact]
		public void TestParseShapePointsThrowsInvalidDataException()
		{
			Assert.Throws<InvalidDataException>(() => Helpers.ParseShapePoints("Triangle{4 2 1 3 0 0}", @"Triangle\{\s*((-?\d+\s+){5}-?\d+)\s*\}", 1, 1));
		}
		
		private class ReadPointsFromFileData
		{
			public static IEnumerable<object[]> SuccessData => new List<object[]>
			{
				new object[]
				{
					"Triangle{-2 -1 -5 -1 -2 -5}",
					@"Triangle\{\s*((-?\d+\s+){5}-?\d+)\s*\}", 2, 3,
					new[]
					{
						new Point(-2, -1), 
						new Point(-5, -1), 
						new Point(-2, -5), 
					}, 
				},
				new object[]
				{
					"Circle{-5 -5 -2 -2}",
					@"Circle\{\s*((-?\d+\s+){3}-?\d+)\s*\}", 2, 2,
					new[]
					{
						new Point(-5, -5), 
						new Point(-2, -2), 
					}, 
				},
			};
		}
	}
}
