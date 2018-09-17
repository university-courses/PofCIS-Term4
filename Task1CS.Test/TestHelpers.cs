using System.IO;
using System.Collections.Generic;

using Xunit;

using Task1CS.Classes;

namespace Task1CS.Test
{
	public class TestHelpers
	{
		private const string TestDataDir = "TestData\\";
		
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
				new object[] {"0.1", 0.1},
				new object[] {"0", 0.0},
				new object[] {"-10", -10.0},
				new object[] {"1", 1.0},
				new object[] {"-1.0757", -1.0757},
				new object[] {"10987654.977", 10987654.977},
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
		public void TestReadShapePointsSuccess(string dataToWrite, string regex, int cpp, int pc, Point[] expected)
		{
			// creating test data file
			Directory.CreateDirectory(TestDataDir);
			File.WriteAllText(TestDataDir + "TestReadShapesPoints.txt", dataToWrite);
			
			// testing
			var stream = new StreamReader(TestDataDir + "TestReadShapesPoints.txt");
			var actual = Helpers.ReadShapePoints(ref stream, regex, cpp, pc);

			for (var i = 0; i < actual.Length; i++)
			{
				Assert.Equal(expected[i].X, actual[i].X);
				Assert.Equal(expected[i].Y, actual[i].Y);
			}
			
			// removing garbage
			File.Delete(TestDataDir + "TestReadShapesPoints.txt");
			Directory.Delete(TestDataDir);
			
		}
		
		private class ReadPointsFromFileData
		{
			public static IEnumerable<object[]> SuccessData => new List<object[]>
			{
				new object[]
				{
					"Triangle{-2 -1 -5 -1 -2 -5}",
					@"Triangle\{\s*((-?\d+\s+){5}-?\d+)\s*\}", 2, 3,
					new Point[]
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
					new Point[]
					{
						new Point(-5, -5), 
						new Point(-2, -2), 
					}, 
				},
			};
		}
	}
}
