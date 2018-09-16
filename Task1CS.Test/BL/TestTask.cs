using System.Collections.Generic;
using System.IO;
using System.Linq;

using Xunit;

using Task1CS.Classes;
using Task1CS.Interfaces;
using Task1CS.BL;

namespace Task1CS.Test.BL
{
	public class TestTask
	{
		private const string TestDataDir = "TestData\\";
		
		[Theory]
		[MemberData(nameof(ReadFromFileData.SuccessData), MemberType = typeof(ReadFromFileData))]
		public void TestReadFromFileSuccess(string input, List<IShape> expected)
		{
			// creating test data file
			Directory.CreateDirectory(TestDataDir);
			File.WriteAllText(TestDataDir + "TestShapes.txt", @"
Triangle{4 2 1 3 0 0}
Triangle{-2 -1 -5 -1 -2 -5}
Triangle{2 3 0 0 8 8}");
			
			// testing
			var actual = Task.ReadFromFile(input);
			Assert.Equal(actual.Count, expected.Count);
			
			for (var i = 0; i < actual.Count; i++)
			{
				var actualPoints = actual[i].GetPoints().ToArray();
				var expectedPoints = expected[i].GetPoints().ToArray();
				
				Assert.NotNull(actualPoints);
				Assert.NotNull(expectedPoints);
				
				for (var j = 0; j < actualPoints.Length; j++)
				{
					Assert.Equal(actualPoints[j].X, expectedPoints[j].X);
					Assert.Equal(actualPoints[j].Y, expectedPoints[j].Y);
				}	
			}
			
			// removing garbage
			File.Delete(TestDataDir + "TestShapes.txt");
			Directory.Delete(TestDataDir);
			
		}
		
		[Theory]
		[MemberData(nameof(ReadFromFileData.ThrowsIoExceptionData), MemberType = typeof(ReadFromFileData))]
		public void TestReadFromFileThrowsIoException(string input)
		{
			Assert.Throws<IOException>(() => Task.ReadFromFile(input));
		}

		private class ReadFromFileData
		{
			public static IEnumerable<object[]> SuccessData => new List<object[]>
			{
				new object[]
				{
					TestDataDir + "TestShapes.txt",
					new List<IShape>
					{
						new Triangle(new []
						{
							new Point(4, 2), 
							new Point(1, 3), 
							new Point(0, 0), 
						}),
						new Triangle(new []
						{
							new Point(-2, -1), 
							new Point(-5, -1), 
							new Point(-2, -5), 
						}),
						new Triangle(new []
						{
							new Point(2, 3), 
							new Point(0, 0), 
							new Point(8, 8), 
						})
					}, 
				}
			};
			
			public static IEnumerable<object[]> ThrowsIoExceptionData => new List<object[]>
			{
				new object[] {"file.txt"},
				new object[] {"file.xml"},
				new object[] {"some-other-file.json"}
			};
		}
		
		[Theory]
		[MemberData(nameof(IsInThirdQuarterData.SuccessData), MemberType = typeof(IsInThirdQuarterData))]
		public void TestIsInThirdQuarter(IShape input, bool expected)
		{
			Assert.Equal(expected, Task.IsInThirdQuarter(input));
		}

		private class IsInThirdQuarterData
		{
			public static IEnumerable<object[]> SuccessData => new List<object[]>
			{
				new object[]
				{
					new Triangle(new []
					{
						new Point(4, 2), 
						new Point(1, 3), 
						new Point(0, 0), 
					}),
					false
				},
				new object[]
				{
					new Triangle(new []
					{
						new Point(-2, -1), 
						new Point(-5, -1), 
						new Point(-2, -5), 
					}),
					true
				},
				new object[]
				{
					new Triangle(new []
					{
						new Point(-2, -3), 
						new Point(0, 0), 
						new Point(-8, -8), 
					}),
					true
				},
			};
		}
	}
}
