using Xunit;

using System;
using System.IO;
using System.Xml;
using System.Globalization;
using System.Collections.Generic;

using CargoDelivery.Classes.OrderData;

namespace CargoDelivery.Test.Classes
{
	public class GoodsDataTest
	{
		[Theory]
		[MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
		public void TestConstructorWithMultParams(uint code, double weight)
		{
			var actual = new GoodsData(code, weight);
			Assert.Equal(code, actual.Code);
			Assert.Equal(weight, actual.Weight);
		}
		
		[Theory]
		[MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
		public void TestConstructorXmlParam(uint code, double weight)
		{
			var doc = new XmlDocument();
			var element = doc.CreateElement(string.Empty, "fi", string.Empty);
			var attributes = CreateAttributes(doc, code, weight);

			foreach (var attr in attributes)
			{
				element.Attributes.Append(attr);
			}
			
			var actual = new GoodsData(element.Attributes);
			Assert.Equal(element.Attributes["Code"].Value, actual.Code.ToString());
			Assert.Equal(element.Attributes["Weight"].Value, actual.Weight.ToString(CultureInfo.InvariantCulture));
		}
		
		[Fact]
		public void TestConstructorThrowsNullReferenceExcpetion()
		{
			Assert.Throws<NullReferenceException>(() => new GoodsData(null));
		}
		
		[Fact]
		public void TestConstructorThrowsInvalidDataException()
		{
			var doc = new XmlDocument();
			var xmlAttributeCollection = new List<XmlAttribute>();
			var attribute = doc.CreateAttribute("Code");
			attribute.Value = "-100";
			xmlAttributeCollection.Add(attribute);
			attribute = doc.CreateAttribute("Weight");
			attribute.Value = 10.1.ToString(CultureInfo.InvariantCulture);
			xmlAttributeCollection.Add(attribute);
			var element = doc.CreateElement(string.Empty, "fi", string.Empty);
			foreach (var attr in xmlAttributeCollection)
			{
				element.Attributes.Append(attr);
			}
			Assert.Throws<InvalidDataException>(() => new GoodsData(element.Attributes));
			
			xmlAttributeCollection.Clear();
			attribute = doc.CreateAttribute("Code");
			attribute.Value = "100";
			xmlAttributeCollection.Add(attribute);
			attribute = doc.CreateAttribute("Weight");
			attribute.Value = "Some weight";
			xmlAttributeCollection.Add(attribute);
			element = doc.CreateElement(string.Empty, "fi", string.Empty);
			foreach (var attr in xmlAttributeCollection)
			{
				element.Attributes.Append(attr);
			}
			Assert.Throws<InvalidDataException>(() => new GoodsData(element.Attributes));
		}
		
		[Theory]
		[MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
		public void TestToXml(uint code, double weight)
		{
			var gd = new GoodsData(code, weight);
			var actual = gd.ToXml();
			Assert.Equal(code.ToString(), actual.Attribute("Code")?.Value);
			Assert.Equal(weight.ToString(CultureInfo.InvariantCulture), actual.Attribute("Weight")?.Value);
		}
		
		private static IEnumerable<XmlAttribute> CreateAttributes(XmlDocument doc, uint code, double weight)
		{
			var xmlAttributeCollection = new List<XmlAttribute>();
			var attribute = doc.CreateAttribute("Code");
			attribute.Value = code.ToString();
			xmlAttributeCollection.Add(attribute);
			attribute = doc.CreateAttribute("Weight");
			attribute.Value = weight.ToString(CultureInfo.InvariantCulture);
			xmlAttributeCollection.Add(attribute);
			return xmlAttributeCollection;
		}

		private class TestData
		{
			public static IEnumerable<object[]> Data => new List<object[]>
			{
				new object[]
				{
					11, 10.1
				},
				new object[]
				{
					12, 10.2
				},
				new object[]
				{
					13, 10.3
				},
				new object[]
				{
					14, 10.4
				},
				new object[]
				{
					15, 10.5
				},
				new object[]
				{
					16, 10.6
				},
				new object[]
				{
					17, 10.7
				},
				new object[]
				{
					18, 10.8
				},
				new object[]
				{
					19, 10.9
				},
				new object[]
				{
					20, 10.10
				}
			};
		}
	}
}
