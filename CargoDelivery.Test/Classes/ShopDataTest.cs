using Xunit;

using System;
using System.Xml;
using System.Collections.Generic;

using CargoDelivery.Classes.OrderData;

namespace CargoDelivery.Test.Classes
{
	public class ShopDataTest
	{
		[Theory]
		[MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
		public void TestConstructorWithMultParams(string name, Address address)
		{
			var actual = new ShopData(name, address);
			Assert.Equal(name, actual.Name);
			Assert.Equal(address.City, actual.Address.City);
			Assert.Equal(address.Street, actual.Address.Street);
			Assert.Equal(address.BuildingNumber, actual.Address.BuildingNumber);
		}
		
		[Theory]
		[MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
		public void TestConstructorXmlParam(string name, Address address)
		{
			var doc = new XmlDocument();
			var element = CreateXmlElement(doc, name, address);
			var actual = new ShopData(element);
			var addr = element.SelectSingleNode("Address");
			Assert.Equal(element.Attributes["Name"].Value, actual.Name);
			Assert.Equal(addr?.Attributes?["City"].Value, actual.Address.City);
			Assert.Equal(addr?.Attributes?["Street"].Value, actual.Address.Street);
			Assert.Equal(addr?.Attributes?["BuildingNumber"].Value, actual.Address.BuildingNumber.ToString());
		}
		
		[Fact]
		public void TestConstructorThrowsNullReferenceExcpetion()
		{
			var doc = new XmlDocument();
			Assert.Throws<NullReferenceException>(() => new ShopData(null));
			Assert.Throws<NullReferenceException>(() => new ShopData(doc.CreateElement("main")));
			var element = doc.CreateElement("main");
			var attr = doc.CreateAttribute("Name");
			attr.Value = "Some name";
			element.Attributes.Append(attr);
			Assert.Throws<NullReferenceException>(() => new ShopData(element));
		}
		
		[Theory]
		[MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
		public void TestToXml(string name, Address address)
		{
			var addr = new ShopData(name, address);
			var actual = addr.ToXml();
			var addrElem = actual.Element("Address");
			Assert.Equal(name, actual.Attribute("Name")?.Value);
			Assert.Equal(address.City, addrElem?.Attribute("City")?.Value);
			Assert.Equal(address.Street, addrElem?.Attribute("Street")?.Value);
			Assert.Equal(address.BuildingNumber.ToString(), addrElem?.Attribute("BuildingNumber")?.Value);
		}
		
		private static XmlElement CreateXmlElement(XmlDocument doc, string name, Address address)
		{
			var xmlAttributeCollection = new List<XmlAttribute>();
			
			var attribute = doc.CreateAttribute("City");
			attribute.Value = address.City;
			xmlAttributeCollection.Add(attribute);
			
			attribute = doc.CreateAttribute("Street");
			attribute.Value = address.Street;
			xmlAttributeCollection.Add(attribute);
			
			attribute = doc.CreateAttribute("BuildingNumber");
			attribute.Value = address.BuildingNumber.ToString();
			xmlAttributeCollection.Add(attribute);
			
			var addressElement = doc.CreateElement("Address");
			foreach (var attr in xmlAttributeCollection)
			{
				addressElement.Attributes.Append(attr);
			}
			
			xmlAttributeCollection.Clear();
			attribute = doc.CreateAttribute("Name");
			attribute.Value = name;

			var mainElement = doc.CreateElement("main");
			mainElement.Attributes.Append(attribute);
			mainElement.AppendChild(addressElement);
			
			return mainElement;
		}

		private class TestData
		{
			public static IEnumerable<object[]> Data => new List<object[]>
			{
				new object[]
				{
					"ShopName1", new Address("City1", "Street1", 11) 
				},
				new object[]
				{
					"ShopName2", new Address("City2", "Street2", 12) 
				},
				new object[]
				{
					"ShopName3", new Address("City3", "Street3", 13) 
				},
				new object[]
				{
					"ShopName4", new Address("City4", "Street4", 14) 
				},
				new object[]
				{
					"ShopName5", new Address("City5", "Street5", 15) 
				}
			};
		}
	}
}
