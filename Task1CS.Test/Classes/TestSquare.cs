using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Xunit;

using Task1CS.Classes;

namespace Task1CS.Test.Classes
{
	public class TestSquare
	{
		[Theory]
		[MemberData(nameof(ConstructorData.Success), MemberType = typeof(ConstructorData))]
		public void TestConstructorSuccess(Point[] input, Point[] expected)
		{
			var square = new Square(input);
			var actual = square.GetPoints().ToArray();
			Assert.NotNull(actual);
			Assert.Equal(actual.Count(), expected.Length);
			for (var i = 0; i < expected.Length; i++)
			{
				Assert.Equal(actual[i].X, expected[i].X);
				Assert.Equal(actual[i].Y, expected[i].Y);
			}
		}

		[Fact]
		public void TestConstructorThrowsNullReferenceException()
		{
			Assert.Throws<NullReferenceException>(() => new Square(null));
		}
		
		[Theory]
		[MemberData(nameof(ConstructorData.Throws), MemberType = typeof(ConstructorData))]
		public void TestConstructorThrows(Point[] input)
		{
			Assert.Throws<InvalidDataException>(() => new Square(input));
		}

		private class ConstructorData
		{
			public static IEnumerable<object[]> Success => new List<object[]>
			{
				new object[]
				{
					new[]
					{
						new Point(1, 1), 
						new Point(2, 1), 
						new Point(2, 2), 
						new Point(1, 2)
					},
					new[]
					{
						new Point(1, 1), 
						new Point(2, 1), 
						new Point(2, 2), 
						new Point(1, 2)	
					},
				},
				new object[]
				{
					new[]
					{
						new Point(0, 0), 
						new Point(5, 0), 
						new Point(5, 5), 
						new Point(0, 5)
					},
					new[]
					{
						new Point(0, 0), 
						new Point(5, 0), 
						new Point(5, 5), 
						new Point(0, 5)	
					},
				}
			};

			public static IEnumerable<object[]> Throws => new List<object[]>
			{
				new object[]
				{
					new[]
					{
						new Point(1, 1), 
						new Point(2, 1), 
						new Point(2, 2), 
						new Point(1, 3)
					}
				},
				new object[]
				{
					new[]
					{
						new Point(1, 1), 
						new Point(2, 1), 
						new Point(2, 2), 
						new Point(1, 2),
						new Point(0, 0)
					}
				}
			};
		}

		
		[Theory]
		[MemberData(nameof(PointsAreValidData.Success), MemberType = typeof(PointsAreValidData))]
		public void TestPointsAreValid(Point[] points, bool expected)
		{
			var actual = Square.PointsAreValid(points);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestPointsAreValidThrowsNullReferenceException()
		{
			Assert.Throws<NullReferenceException>(() => Square.PointsAreValid(null));
		}

		private class PointsAreValidData
		{
			public static IEnumerable<object[]> Success => new List<object[]>
			{
				new object[]
				{
					new[]
					{
						new Point(1, 1), 
						new Point(2, 1), 
						new Point(3, 3), 
						new Point(1, 3)
					},
					false
				},
				new object[]
				{
					new[]
					{
						new Point(0, 0), 
						new Point(5, 0), 
						new Point(5, 5), 
						new Point(0, 5)
					},
					true
				},
				new object[]
				{
					new[]
					{
						new Point(1, 1), 
						new Point(2, 1), 
						new Point(2, 2), 
						new Point(1, 3)
					},
					false
				},
				new object[]
				{
					new[]
					{
						new Point(1, 1), 
						new Point(2, 1), 
						new Point(2, 2), 
						new Point(1, 2),
						new Point(0, 0)
					},
					false
				}
			};
		}
		
		
		[Theory]
		[MemberData(nameof(CalcPerimeterData.Success), MemberType = typeof(CalcPerimeterData))]
		public void TestCalcPerimeter(Square square, double expected)
		{
			var actual = square.CalcPerimeter();
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestCalcPerimeterThrowsNullReferenceException()
		{
			Assert.Throws<NullReferenceException>(() => new Square().CalcPerimeter());
		}
		
		private class CalcPerimeterData
		{
			public static IEnumerable<object[]> Success => new List<object[]>
			{
				new object[]
				{
					new Square(new[]
					{
						new Point(1, 1), 
						new Point(2, 1), 
						new Point(2, 2), 
						new Point(1, 2)
					}),
					4.0
				},
				new object[]
				{
					new Square(new[]
					{
						new Point(-1, -2),
						new Point(3, -2),  
						new Point(3, 2),
						new Point(-1, 2)
					}),
					16.0
				}
			};
		}
		
		
		[Fact]
		public void TestGetPointsThrowsNullReferenceException()
		{
			Assert.Throws<NullReferenceException>(() => new Square().GetPoints());
		}

		[Theory]
		[MemberData(nameof(GetPointsData.Success), MemberType = typeof(GetPointsData))]
		public void TestGetPointsSuccess(Point[] points, Point[] expected)
		{
			var square = new Square(points);
			var actual = square.GetPoints().ToArray();
			Assert.Equal(actual.Length, expected.Length);
			for (var i = 0; i < expected.Length; i++)
			{
				Assert.Equal(expected[i].X, actual[i].X);
				Assert.Equal(expected[i].Y, actual[i].Y);
			}
		}

		private class GetPointsData
		{
			public static IEnumerable<object[]> Success => new List<object[]>
			{
				new object[]
				{
					new[]
					{
						new Point(1, 1), 
						new Point(2, 1), 
						new Point(2, 2), 
						new Point(1, 2)
					},
					new[]
					{
						new Point(1, 1), 
						new Point(2, 1), 
						new Point(2, 2), 
						new Point(1, 2)
					}
				}
			};
		}
		
		
		[Theory]
		[MemberData(nameof(CalcSquareData.Success), MemberType = typeof(CalcSquareData))]
		public void TestCalcSquare(Square square, double expected)
		{
			var actual = square.CalcSquare();
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestCalcSquareThrowsNullReferenceException()
		{
			Assert.Throws<NullReferenceException>(() => new Square().CalcSquare());
		}
		
		private class CalcSquareData
		{
			public static IEnumerable<object[]> Success => new List<object[]>
			{
				new object[]
				{
					new Square(new[]
					{
						new Point(1, 1), 
						new Point(2, 1), 
						new Point(2, 2), 
						new Point(1, 2)
					}),
					1.0
				},
				new object[]
				{
					new Square(new[]
					{
						new Point(-2, -2),
						new Point(3, -2),  
						new Point(3, 3),
						new Point(-2, 3)
					}),
					25.0
				}
			};
		}


		[Theory]
		[MemberData(nameof(ToStringData.Success), MemberType = typeof(ToStringData))]
		public void TestToString(Square square, string expected)
		{
			var actual = square.ToString();
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestToStringThrowsNullReferenceException()
		{
			Assert.Throws<NullReferenceException>(() => new Square().ToString());
		}
		
		private class ToStringData
		{
			public static IEnumerable<object[]> Success => new List<object[]>
			{
				new object[]
				{
					new Square(new[]
					{
						new Point(1, 1), 
						new Point(2, 1), 
						new Point(2, 2), 
						new Point(1, 2)
					}),
					"Square:\n  Point 0: x=1, y=1\n  Point 1: x=2, y=1\n  Point 2: x=2, y=2\n  Point 3: x=1, y=2"
				},
				new object[]
				{
					new Square(new[]
					{
						new Point(-2, -2),
						new Point(3, -2),  
						new Point(3, 3),
						new Point(-2, 3)
					}),
					"Square:\n  Point 0: x=-2, y=-2\n  Point 1: x=3, y=-2\n  Point 2: x=3, y=3\n  Point 3: x=-2, y=3"
				}
			};
		}


		[Theory]
		[MemberData(nameof(ParseData.Success), MemberType = typeof(ParseData))]
		public void TestParseSuccess(string input, Square expected)
		{
			var actualSquare = new Square();
			Assert.True(actualSquare.Parse(input));
			var actualPoints = actualSquare.GetPoints().ToArray();
			var expectedPoints = expected.GetPoints().ToArray();
			Assert.Equal(actualPoints.Length, expectedPoints.Length);
			for (var i = 0; i < actualPoints.Length; i++)
			{
				Assert.Equal(expectedPoints[i].X, actualPoints[i].X);
				Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y);
			}
		}

		[Theory]
		[MemberData(nameof(ParseData.ReturnsFalse), MemberType = typeof(ParseData))]
		public void TestParseReturnsFalse(string input)
		{
			Assert.False(new Square().Parse(input));
		}
		
		private class ParseData
		{
			public static IEnumerable<object[]> Success => new List<object[]>
			{
				new object[]
				{
					"Square{1 1 2 1 2 2 1 2}",
					new Square(new []
					{
						new Point(1, 1), 
						new Point(2, 1), 
						new Point(2, 2), 
						new Point(1, 2) 
					})
				},
				new object[]
				{
					"Square{-2 -2 3 -2 3 3 -2 3}",
					new Square(new []
					{
						new Point(-2, -2),
						new Point(3, -2),  
						new Point(3, 3),
						new Point(-2, 3)
					}), 
				}
			};

			public static IEnumerable<object[]> ReturnsFalse => new List<object[]>
			{
				new object[]
				{
					"Square{1 2 1 2 2 1 2}"
				},
				new object[]
				{
					"SSquare{1 2 1 2 2 1 2}"
				},
				new object[]
				{
					"Square{+-1 2 1 2 2 1 2}"
				},
				new object[]
				{
					"Square{1 1 2 1 2 2 1 3}"
				}
			};
		}
	}
}
