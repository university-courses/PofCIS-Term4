using System.Collections.Generic;
using System.IO;

using Xunit;

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
	}
}
