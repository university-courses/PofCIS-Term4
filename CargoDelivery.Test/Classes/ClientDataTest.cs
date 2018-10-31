using Xunit;

using System;
using System.Xml;
using System.Collections.Generic;

using CargoDelivery.Classes.OrderData;

namespace CargoDelivery.Test.Classes
{
	public class ClientDataTest
	{
		[Theory]
		[MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
		public void TestConstructorWithMultParams(string fName, string lName, string email, string pNumber, Address address)
		{
			var actual = new ClientData(fName, lName, email, pNumber, address);
			Assert.Equal(fName, actual.FirstName);
			Assert.Equal(lName, actual.LastName);
			Assert.Equal(email, actual.Email);
			Assert.Equal(pNumber, actual.PhoneNumber);
			Assert.Equal(address.City, actual.Address.City);
			Assert.Equal(address.Street, actual.Address.Street);
			Assert.Equal(address.BuildingNumber, actual.Address.BuildingNumber);
		}
		
		[Theory]
		[MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
		public void TestConstructorXmlParam(string fName, string lName, string email, string pNumber, Address address)
		{
			var doc = new XmlDocument();
			var element = CreateXmlElement(doc, fName, lName, email, pNumber, address);
			var actual = new ClientData(element);
			var addr = element.SelectSingleNode("Address");
			Assert.Equal(addr?.Attributes?["City"].Value, actual.Address.City);
			Assert.Equal(addr?.Attributes?["Street"].Value, actual.Address.Street);
			Assert.Equal(addr?.Attributes?["BuildingNumber"].Value, actual.Address.BuildingNumber.ToString());
			Assert.Equal(element.Attributes["FirstName"].Value, actual.FirstName);
			Assert.Equal(element.Attributes["LastName"].Value, actual.LastName);
			Assert.Equal(element.Attributes["Email"].Value, actual.Email);
			Assert.Equal(element.Attributes["PhoneNumber"].Value, actual.PhoneNumber);
		}
		
		[Fact]
		public void TestConstructorThrowsNullReferenceExcpetion()
		{
			var doc = new XmlDocument();
			Assert.Throws<NullReferenceException>(() => new ClientData(null));
			Assert.Throws<NullReferenceException>(() => new ClientData(doc.CreateElement("main")));
			var element = CreateXmlElement(doc, "FName", "Lname", "Emain", "+380000000", new Address("city", "street", 10));
			var child = element.SelectSingleNode("Address");
			if (child != null)
			{
				element.RemoveChild(child);	
			}
			Assert.Throws<NullReferenceException>(() => new ClientData(element));
		}
		
		[Theory]
		[MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
		public void TestToXml(string fName, string lName, string email, string pNumber, Address address)
		{
			var addr = new ClientData(fName, lName, email, pNumber, address);
			var actual = addr.ToXml();
			var addrElem = actual.Element("Address");
			Assert.Equal(address.City, addrElem?.Attribute("City")?.Value);
			Assert.Equal(address.Street, addrElem?.Attribute("Street")?.Value);
			Assert.Equal(address.BuildingNumber.ToString(), addrElem?.Attribute("BuildingNumber")?.Value);
			Assert.Equal(fName, actual.Attribute("FirstName")?.Value);
			Assert.Equal(lName, actual.Attribute("LastName")?.Value);
			Assert.Equal(email, actual.Attribute("Email")?.Value);
			Assert.Equal(pNumber, actual.Attribute("PhoneNumber")?.Value);
		}
		
		private static XmlElement CreateXmlElement(XmlDocument doc, string fName, string lName, string email, string pNumber, Address address)
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
			attribute = doc.CreateAttribute("FirstName");
			attribute.Value = fName;
			xmlAttributeCollection.Add(attribute);
			
			attribute = doc.CreateAttribute("LastName");
			attribute.Value = lName;
			xmlAttributeCollection.Add(attribute);
			
			attribute = doc.CreateAttribute("Email");
			attribute.Value = email;
			xmlAttributeCollection.Add(attribute);
			
			attribute = doc.CreateAttribute("PhoneNumber");
			attribute.Value = pNumber;
			xmlAttributeCollection.Add(attribute);
			
			var mainElement = doc.CreateElement("main");
			foreach (var attr in xmlAttributeCollection)
			{
				mainElement.Attributes.Append(attr);
			}
			mainElement.AppendChild(addressElement);
			return mainElement;
		}

		private class TestData
		{
			public static IEnumerable<object[]> Data => new List<object[]>
			{
				new object[]
				{
					"FirstName1", "LastName1", "Email1", "+380000001", new Address("City1", "Street1", 11) 
				},
				new object[]
				{
					"FirstName2", "LastName2", "Email2", "+380000002", new Address("City2", "Street2", 12) 
				},
				new object[]
				{
					"FirstName3", "LastName3", "Email3", "+380000003", new Address("City3", "Street3", 13) 
				},
				new object[]
				{
					"FirstName4", "LastName4", "Email4", "+380000004", new Address("City4", "Street4", 14) 
				},
				new object[]
				{
					"FirstName5", "LastName5", "Email5", "+380000005", new Address("City5", "Street5", 15) 
				}
			};
		}
	}
}
