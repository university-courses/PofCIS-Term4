using System.IO;
using System.Linq;
using System.Collections.Generic;

using Xunit;
using Task1CS.BL;
using Task1CS.Classes;
using Task1CS.Interfaces;

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
			File.WriteAllText(TestDataDir + "TestShapes.txt", @"Triangle{4 2 1 3 0 0}
Triangle{-2 -1 -5 -1 -2 -5}
Triangle{2 3 0 0 8 8}
Circle{0 0 2 2}
Circle{-5 -5 -2 -2}");
			
			// testing
			var actual = Task.ReadFromFile(input);
			Assert.Equal(expected.Count, actual.Count);
			
			for (var i = 0; i < actual.Count; i++)
			{
				var actualPoints = actual[i].GetPoints().ToArray();
				var expectedPoints = expected[i].GetPoints().ToArray();
				
				Assert.NotNull(actualPoints);
				Assert.NotNull(expectedPoints);
				
				for (var j = 0; j < actualPoints.Length; j++)
				{
					Assert.Equal(expectedPoints[j].X, actualPoints[j].X);
					Assert.Equal(expectedPoints[j].Y, actualPoints[j].Y);
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
						}),
						new Circle(new []
						{
							new Point(0, 0), 
							new Point(2, 2), 
						}),
						new Circle(new []
						{
							new Point(-5, -5), 
							new Point(-2, -2), 
						})
					} 
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
		
		[Theory]
		[MemberData(nameof(WriteToFileData.SuccessData), MemberType = typeof(WriteToFileData))]
		public void TestWriteToFileSuccess(string inputFile, List<IShape> inputShapes, string expected)
		{
			// creating test directory
			Directory.CreateDirectory(TestDataDir);
			
			// testing
			Task.WriteToFile(inputFile, inputShapes);
			
			Assert.True(File.Exists(inputFile));
			
			var fileData = File.ReadAllText(inputFile);
			
			Assert.Equal(expected, fileData);
			
			// removing garbage
			File.Delete(inputFile);
			Directory.Delete(TestDataDir);
			
		}
		
		private class WriteToFileData
		{
			public static IEnumerable<object[]> SuccessData => new List<object[]>
			{
				new object[]
				{
					TestDataDir + "TestResult1.txt",
					new List<IShape>
					{
						new Triangle(new []
						{
							new Point(2, 3), 
							new Point(0, 0), 
							new Point(8, 8), 
						}),
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
						new Circle(new []
						{
							new Point(-5, -5), 	
							new Point(-2, -2), 	
						})
					},
					@"Triangle:
  Point 0: x=2, y=3
  Point 1: x=0, y=0
  Point 2: x=8, y=8
Triangle:
  Point 0: x=4, y=2
  Point 1: x=1, y=3
  Point 2: x=0, y=0
Triangle:
  Point 0: x=-2, y=-1
  Point 1: x=-5, y=-1
  Point 2: x=-2, y=-5
Circle:
  Radius: 4.24264068711928
  Points:
	Point 0: x=-5, y=-5
	Point 1: x=-2, y=-2
"
				},
				new object[]
				{
					TestDataDir + "TestResult2.txt",
					new List<IShape>
					{
						new Triangle(new []
						{
							new Point(-2, -1), 
							new Point(-5, -1), 
							new Point(-2, -5), 
						})
					},
					@"Triangle:
  Point 0: x=-2, y=-1
  Point 1: x=-5, y=-1
  Point 2: x=-2, y=-5
"
				}
			};
		}
	}
}
