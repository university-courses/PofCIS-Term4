using Xunit;

using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

using CargoDelivery.Classes.OrderData;

namespace CargoDelivery.Test.Classes
{
	public class AddressTest
	{
		[Theory]
		[MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
		public void TestConstructorWithMultParams(string city, string street, uint bNum)
		{
			var actual = new Address(city, street, bNum);
			Assert.Equal(city, actual.City);
			Assert.Equal(street, actual.Street);
			Assert.Equal(bNum, actual.BuildingNumber);
		}
		
		[Theory]
		[MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
		public void TestConstructorXmlParam(string city, string street, uint bNum)
		{
			var doc = new XmlDocument();
			var element = doc.CreateElement(string.Empty, "fi", string.Empty);
			var attributes = CreateAttributes(doc, city, street, bNum);

			foreach (var attr in attributes)
			{
				element.Attributes.Append(attr);
			}
			
			var actual = new Address(element.Attributes);
			Assert.Equal(element.Attributes["City"].Value, actual.City);
			Assert.Equal(element.Attributes["Street"].Value, actual.Street);
			Assert.Equal(element.Attributes["BuildingNumber"].Value, actual.BuildingNumber.ToString());
		}
		
		[Fact]
		public void TestConstructorThrowsNullReferenceExcpetion()
		{
			Assert.Throws<NullReferenceException>(() => new Address(null));
		}
		
		[Fact]
		public void TestConstructorThrowsInvalidDataException()
		{
			var doc = new XmlDocument();
			var xmlAttributeCollection = new List<XmlAttribute>();
			var attribute = doc.CreateAttribute("City");
			attribute.Value = "SomeCity";
			xmlAttributeCollection.Add(attribute);
			attribute = doc.CreateAttribute("Street");
			attribute.Value = "SomeStreet";
			xmlAttributeCollection.Add(attribute);
			attribute = doc.CreateAttribute("BuildingNumber");
			attribute.Value = "-10";
			xmlAttributeCollection.Add(attribute);
			var element = doc.CreateElement(string.Empty, "fi", string.Empty);
			foreach (var attr in xmlAttributeCollection)
			{
				element.Attributes.Append(attr);
			}
			Assert.Throws<InvalidDataException>(() => new Address(element.Attributes));
		}
		
		[Theory]
		[MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
		public void TestToXml(string city, string street, uint bNum)
		{
			var addr = new Address(city, street, bNum);
			var actual = addr.ToXml();
			Assert.Equal(city, actual.Attribute("City")?.Value);
			Assert.Equal(street, actual.Attribute("Street")?.Value);
			Assert.Equal(bNum.ToString(), actual.Attribute("BuildingNumber")?.Value);
		}
		
		private static IEnumerable<XmlAttribute> CreateAttributes(XmlDocument doc, string city, string street, uint bNum)
		{
			var xmlAttributeCollection = new List<XmlAttribute>();
			
			var attribute = doc.CreateAttribute("City");
			attribute.Value = city;
			xmlAttributeCollection.Add(attribute);
			
			attribute = doc.CreateAttribute("Street");
			attribute.Value = street;
			xmlAttributeCollection.Add(attribute);
			
			attribute = doc.CreateAttribute("BuildingNumber");
			attribute.Value = bNum.ToString();
			xmlAttributeCollection.Add(attribute);
			
			return xmlAttributeCollection;
		}

		private class TestData
		{
			public static IEnumerable<object[]> Data => new List<object[]>
			{
				new object[]
				{
					"City1", "Street1", 10
				},
				new object[]
				{
					"City2", "Street2", 11
				},
				new object[]
				{
					"City3", "Street3", 12
				},
				new object[]
				{
					"City4", "Street4", 13
				},
				new object[]
				{
					"City5", "Street5", 14
				},
				new object[]
				{
					"City6", "Street6", 15
				},
				new object[]
				{
					"City7", "Street7", 16
				},
				new object[]
				{
					"City8", "Street8", 17
				},
				new object[]
				{
					"City9", "Street9", 18
				},
				new object[]
				{
					"City10", "Street10", 19
				}
			};
		}
	}
}
