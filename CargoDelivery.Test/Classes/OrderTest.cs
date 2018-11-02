using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Xunit;

using CargoDelivery.Classes;
using CargoDelivery.Classes.OrderData;

namespace CargoDelivery.Test.Classes
{
	public class OrderTest
	{
		[Theory]
		[MemberData(nameof(TestData.ConstructorData), MemberType = typeof(TestData))]
		public void TestConstructorWithMulrParams(uint id, ClientData clientData, ShopData shopData, GoodsData goodsData)
		{
			var actual = new Order(id, clientData, shopData, goodsData);
			Assert.Equal(id, actual.Id);
			Assert.Equal(shopData.Name, actual.ShopData.Name);
			Assert.Equal(shopData.Address.City, actual.ShopData.Address.City);
			Assert.Equal(shopData.Address.Street, actual.ShopData.Address.Street);
			Assert.Equal(shopData.Address.BuildingNumber, actual.ShopData.Address.BuildingNumber);
			Assert.Equal(goodsData.Code, actual.GoodsData.Code);
			Assert.Equal(goodsData.Weight, actual.GoodsData.Weight);
			Assert.Equal(clientData.FirstName, actual.ClientData.FirstName);
			Assert.Equal(clientData.LastName, actual.ClientData.LastName);
			Assert.Equal(clientData.Email, actual.ClientData.Email);
			Assert.Equal(clientData.PhoneNumber, actual.ClientData.PhoneNumber);
			Assert.Equal(clientData.Address.City, actual.ClientData.Address.City);
			Assert.Equal(clientData.Address.Street, actual.ClientData.Address.Street);
			Assert.Equal(clientData.Address.BuildingNumber, actual.ClientData.Address.BuildingNumber);
		}
		
		[Theory]
		[MemberData(nameof(TestData.ConstructorData), MemberType = typeof(TestData))]
		public void TestConstructorWithXmlNodeParam(uint id, ClientData clientData, ShopData shopData, GoodsData goodsData)
		{
			var actual = new Order(CreateXmlElement(new XmlDocument(), id, clientData, shopData, goodsData));
			Assert.Equal(id, actual.Id);
			Assert.Equal(shopData.Name, actual.ShopData.Name);
			Assert.Equal(shopData.Address.City, actual.ShopData.Address.City);
			Assert.Equal(shopData.Address.Street, actual.ShopData.Address.Street);
			Assert.Equal(shopData.Address.BuildingNumber, actual.ShopData.Address.BuildingNumber);
			Assert.Equal(goodsData.Code, actual.GoodsData.Code);
			Assert.Equal(goodsData.Weight, actual.GoodsData.Weight);
			Assert.Equal(clientData.FirstName, actual.ClientData.FirstName);
			Assert.Equal(clientData.LastName, actual.ClientData.LastName);
			Assert.Equal(clientData.Email, actual.ClientData.Email);
			Assert.Equal(clientData.PhoneNumber, actual.ClientData.PhoneNumber);
			Assert.Equal(clientData.Address.City, actual.ClientData.Address.City);
			Assert.Equal(clientData.Address.Street, actual.ClientData.Address.Street);
			Assert.Equal(clientData.Address.BuildingNumber, actual.ClientData.Address.BuildingNumber);
		}
		
		[Theory]
		[MemberData(nameof(TestData.ConstructorData), MemberType = typeof(TestData))]
		public void TestToXml(uint id, ClientData clientData, ShopData shopData, GoodsData goodsData)
		{
			var actual = new Order(id, clientData, shopData, goodsData).ToXml();
			Assert.Equal(id.ToString(), actual.Attribute("Id")?.Value);
			Assert.Equal(shopData.Name, actual.Element("ShopData")?.Attribute("Name")?.Value);
			Assert.Equal(shopData.Address.City, actual.Element("ShopData")?.Element("Address")?.Attribute("City")?.Value);
			Assert.Equal(shopData.Address.Street, actual.Element("ShopData")?.Element("Address")?.Attribute("Street")?.Value);
			Assert.Equal(shopData.Address.BuildingNumber.ToString(), actual.Element("ShopData")?.Element("Address")?.Attribute("BuildingNumber")?.Value);
			Assert.Equal(goodsData.Code.ToString(), actual.Element("GoodsData")?.Attribute("Code")?.Value);
			Assert.Equal(goodsData.Weight.ToString(CultureInfo.InvariantCulture), actual.Element("GoodsData")?.Attribute("Weight")?.Value);
			Assert.Equal(clientData.FirstName, actual.Element("ClientData")?.Attribute("FirstName")?.Value);
			Assert.Equal(clientData.LastName, actual.Element("ClientData")?.Attribute("LastName")?.Value);
			Assert.Equal(clientData.Email, actual.Element("ClientData")?.Attribute("Email")?.Value);
			Assert.Equal(clientData.PhoneNumber, actual.Element("ClientData")?.Attribute("PhoneNumber")?.Value);
			Assert.Equal(clientData.Address.City, actual.Element("ClientData")?.Element("Address")?.Attribute("City")?.Value);
			Assert.Equal(clientData.Address.Street, actual.Element("ClientData")?.Element("Address")?.Attribute("Street")?.Value);
			Assert.Equal(clientData.Address.BuildingNumber.ToString(), actual.Element("ClientData")?.Element("Address")?.Attribute("BuildingNumber")?.Value);
		}
		
		[Theory]
		[MemberData(nameof(TestData.EditXmlNodeData), MemberType = typeof(TestData))]
		public void TestEditXmlNode(uint id, ClientData clientData, ShopData shopData, GoodsData goodsData, Order order)
		{
			var xmlNode = CreateXmlElement(new XmlDocument(), id, clientData, shopData, goodsData);
			Assert.Equal(id.ToString(), xmlNode.Attributes?["Id"].Value);
			Assert.Equal(shopData.Name, xmlNode.SelectSingleNode("ShopData")?.Attributes?["Name"].Value);
			Assert.Equal(shopData.Address.City, xmlNode.SelectSingleNode("ShopData")?.SelectSingleNode("Address")?.Attributes?["City"].Value);
			Assert.Equal(shopData.Address.Street, xmlNode.SelectSingleNode("ShopData")?.SelectSingleNode("Address")?.Attributes?["Street"].Value);
			Assert.Equal(shopData.Address.BuildingNumber.ToString(), xmlNode.SelectSingleNode("ShopData")?.SelectSingleNode("Address")?.Attributes?["BuildingNumber"].Value);
			Assert.Equal(goodsData.Code.ToString(), xmlNode.SelectSingleNode("GoodsData")?.Attributes?["Code"].Value);
			Assert.Equal(goodsData.Weight.ToString(CultureInfo.InvariantCulture), xmlNode.SelectSingleNode("GoodsData")?.Attributes?["Weight"].Value);
			Assert.Equal(clientData.FirstName, xmlNode.SelectSingleNode("ClientData")?.Attributes?["FirstName"].Value);
			Assert.Equal(clientData.LastName, xmlNode.SelectSingleNode("ClientData")?.Attributes?["LastName"].Value);
			Assert.Equal(clientData.Email, xmlNode.SelectSingleNode("ClientData")?.Attributes?["Email"].Value);
			Assert.Equal(clientData.PhoneNumber, xmlNode.SelectSingleNode("ClientData")?.Attributes?["PhoneNumber"].Value);
			Assert.Equal(clientData.Address.City, xmlNode.SelectSingleNode("ClientData")?.SelectSingleNode("Address")?.Attributes?["City"].Value);
			Assert.Equal(clientData.Address.Street, xmlNode.SelectSingleNode("ClientData")?.SelectSingleNode("Address")?.Attributes?["Street"].Value);
			Assert.Equal(clientData.Address.BuildingNumber.ToString(), xmlNode.SelectSingleNode("ClientData")?.SelectSingleNode("Address")?.Attributes?["BuildingNumber"].Value);
			Order.EditXmlNode(ref xmlNode, order);
			Assert.Equal(order.Id.ToString(), xmlNode.Attributes?["Id"].Value);
			Assert.Equal(order.ShopData.Name, xmlNode.SelectSingleNode("ShopData")?.Attributes?["Name"].Value);
			Assert.Equal(order.ShopData.Address.City, xmlNode.SelectSingleNode("ShopData")?.SelectSingleNode("Address")?.Attributes?["City"].Value);
			Assert.Equal(order.ShopData.Address.Street, xmlNode.SelectSingleNode("ShopData")?.SelectSingleNode("Address")?.Attributes?["Street"].Value);
			Assert.Equal(order.ShopData.Address.BuildingNumber.ToString(), xmlNode.SelectSingleNode("ShopData")?.SelectSingleNode("Address")?.Attributes?["BuildingNumber"].Value);
			Assert.Equal(order.GoodsData.Code.ToString(), xmlNode.SelectSingleNode("GoodsData")?.Attributes?["Code"].Value);
			Assert.Equal(order.GoodsData.Weight.ToString(CultureInfo.InvariantCulture), xmlNode.SelectSingleNode("GoodsData")?.Attributes?["Weight"].Value);
			Assert.Equal(order.ClientData.FirstName, xmlNode.SelectSingleNode("ClientData")?.Attributes?["FirstName"].Value);
			Assert.Equal(order.ClientData.LastName, xmlNode.SelectSingleNode("ClientData")?.Attributes?["LastName"].Value);
			Assert.Equal(order.ClientData.Email, xmlNode.SelectSingleNode("ClientData")?.Attributes?["Email"].Value);
			Assert.Equal(order.ClientData.PhoneNumber, xmlNode.SelectSingleNode("ClientData")?.Attributes?["PhoneNumber"].Value);
			Assert.Equal(order.ClientData.Address.City, xmlNode.SelectSingleNode("ClientData")?.SelectSingleNode("Address")?.Attributes?["City"].Value);
			Assert.Equal(order.ClientData.Address.Street, xmlNode.SelectSingleNode("ClientData")?.SelectSingleNode("Address")?.Attributes?["Street"].Value);
			Assert.Equal(order.ClientData.Address.BuildingNumber.ToString(), xmlNode.SelectSingleNode("ClientData")?.SelectSingleNode("Address")?.Attributes?["BuildingNumber"].Value);
		}
		
		private static XmlNode CreateXmlElement(XmlDocument doc, uint id, ClientData clientData, ShopData shopData, GoodsData goodsData)
		{
			var xmlAttributeCollection = new List<XmlAttribute>();
			var attribute = doc.CreateAttribute("City");
			attribute.Value = clientData.Address.City;
			xmlAttributeCollection.Add(attribute);
			attribute = doc.CreateAttribute("Street");
			attribute.Value = clientData.Address.Street;
			xmlAttributeCollection.Add(attribute);
			attribute = doc.CreateAttribute("BuildingNumber");
			attribute.Value = clientData.Address.BuildingNumber.ToString();
			xmlAttributeCollection.Add(attribute);
			var addressElement = doc.CreateElement("Address");
			foreach (var attr in xmlAttributeCollection)
			{
				addressElement.Attributes.Append(attr);
			}
			xmlAttributeCollection.Clear();
			attribute = doc.CreateAttribute("FirstName");
			attribute.Value = clientData.FirstName;
			xmlAttributeCollection.Add(attribute);
			attribute = doc.CreateAttribute("LastName");
			attribute.Value = clientData.LastName;
			xmlAttributeCollection.Add(attribute);
			attribute = doc.CreateAttribute("Email");
			attribute.Value = clientData.Email;
			xmlAttributeCollection.Add(attribute);
			attribute = doc.CreateAttribute("PhoneNumber");
			attribute.Value = clientData.PhoneNumber;
			xmlAttributeCollection.Add(attribute);
			var clientElement = doc.CreateElement("ClientData");
			foreach (var attr in xmlAttributeCollection)
			{
				clientElement.Attributes.Append(attr);
			}
			clientElement.AppendChild(addressElement);
			xmlAttributeCollection.Clear();
			attribute = doc.CreateAttribute("City");
			attribute.Value = shopData.Address.City;
			xmlAttributeCollection.Add(attribute);
			attribute = doc.CreateAttribute("Street");
			attribute.Value = shopData.Address.Street;
			xmlAttributeCollection.Add(attribute);
			attribute = doc.CreateAttribute("BuildingNumber");
			attribute.Value = shopData.Address.BuildingNumber.ToString();
			xmlAttributeCollection.Add(attribute);
			addressElement = doc.CreateElement("Address");
			foreach (var attr in xmlAttributeCollection)
			{
				addressElement.Attributes.Append(attr);
			}
			xmlAttributeCollection.Clear();
			attribute = doc.CreateAttribute("Name");
			attribute.Value = shopData.Name;
			var shopElement = doc.CreateElement("ShopData");
			shopElement.Attributes.Append(attribute);
			shopElement.AppendChild(addressElement);
			xmlAttributeCollection.Clear();
			attribute = doc.CreateAttribute("Code");
			attribute.Value = goodsData.Code.ToString();
			xmlAttributeCollection.Add(attribute);
			attribute = doc.CreateAttribute("Weight");
			attribute.Value = goodsData.Weight.ToString(CultureInfo.InvariantCulture);
			xmlAttributeCollection.Add(attribute);
			var goodsElement = doc.CreateElement("GoodsData");
			foreach (var attr in xmlAttributeCollection)
			{
				goodsElement.Attributes.Append(attr);
			}
			attribute = doc.CreateAttribute("Id");
			attribute.Value = id.ToString();
			var mainElement = doc.CreateElement("Order");
			mainElement.Attributes.Append(attribute);
			mainElement.AppendChild(clientElement);
			mainElement.AppendChild(shopElement);
			mainElement.AppendChild(goodsElement);
			return mainElement;
		}
		
		private class TestData
		{
			public static IEnumerable<object[]> ConstructorData => new List<object[]>
			{
				new object[]
				{
					1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						),
				},
				new object[]
				{
					2, new ClientData(
							"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
						), new ShopData(
							"ShopName2", new Address("City2", "Street2", 2)
						), new GoodsData(
							2, 2
						),
				},
				new object[]
				{
					3, new ClientData(
							"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
						), new ShopData(
							"ShopName3", new Address("City3", "Street3", 3)
						), new GoodsData(
							3, 3
						),
				},
				new object[]
				{
					1, new ClientData(
						"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
					), new ShopData(
						"ShopName1", new Address("City1", "Street1", 1)
					), new GoodsData(
						1, 1
					),
				},
				new object[]
				{
					2, new ClientData(
						"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
					), new ShopData(
						"ShopName2", new Address("City2", "Street2", 2)
					), new GoodsData(
						2, 2
					),
				},
				new object[]
				{
					3, new ClientData(
						"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
					), new ShopData(
						"ShopName3", new Address("City3", "Street3", 3)
					), new GoodsData(
						3, 3
					),
				},
				new object[]
				{
					1, new ClientData(
						"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
					), new ShopData(
						"ShopName1", new Address("City1", "Street1", 1)
					), new GoodsData(
						1, 1
					),
				},
				new object[]
				{
					2, new ClientData(
						"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
					), new ShopData(
						"ShopName2", new Address("City2", "Street2", 2)
					), new GoodsData(
						2, 2
					),
				},
				new object[]
				{
					3, new ClientData(
						"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
					), new ShopData(
						"ShopName3", new Address("City3", "Street3", 3)
					), new GoodsData(
						3, 3
					),
				},new object[]
				{
					1, new ClientData(
						"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
					), new ShopData(
						"ShopName1", new Address("City1", "Street1", 1)
					), new GoodsData(
						1, 1
					),
				},
				new object[]
				{
					2, new ClientData(
						"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
					), new ShopData(
						"ShopName2", new Address("City2", "Street2", 2)
					), new GoodsData(
						2, 2
					),
				},
				new object[]
				{
					3, new ClientData(
						"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
					), new ShopData(
						"ShopName3", new Address("City3", "Street3", 3)
					), new GoodsData(
						3, 3
					),
				},
				new object[]
				{
					1, new ClientData(
						"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
					), new ShopData(
						"ShopName1", new Address("City1", "Street1", 1)
					), new GoodsData(
						1, 1
					),
				},
				new object[]
				{
					2, new ClientData(
						"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
					), new ShopData(
						"ShopName2", new Address("City2", "Street2", 2)
					), new GoodsData(
						2, 2
					),
				},
				new object[]
				{
					3, new ClientData(
						"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
					), new ShopData(
						"ShopName3", new Address("City3", "Street3", 3)
					), new GoodsData(
						3, 3
					),
				},
				new object[]
				{
					1, new ClientData(
						"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
					), new ShopData(
						"ShopName1", new Address("City1", "Street1", 1)
					), new GoodsData(
						1, 1
					),
				},
				new object[]
				{
					2, new ClientData(
						"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
					), new ShopData(
						"ShopName2", new Address("City2", "Street2", 2)
					), new GoodsData(
						2, 2
					),
				},
				new object[]
				{
					3, new ClientData(
						"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
					), new ShopData(
						"ShopName3", new Address("City3", "Street3", 3)
					), new GoodsData(
						3, 3
					),
				}
			};
			
			public static IEnumerable<object[]> EditXmlNodeData => new List<object[]>
			{
				new object[]
				{
					1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					2, new ClientData(
							"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
						), new ShopData(
							"ShopName2", new Address("City2", "Street2", 2)
						), new GoodsData(
							2, 2
						),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					3, new ClientData(
							"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
						), new ShopData(
							"ShopName3", new Address("City3", "Street3", 3)
						), new GoodsData(
							3, 3
						),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					1, new ClientData(
						"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
					), new ShopData(
						"ShopName1", new Address("City1", "Street1", 1)
					), new GoodsData(
						1, 1
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					2, new ClientData(
						"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
					), new ShopData(
						"ShopName2", new Address("City2", "Street2", 2)
					), new GoodsData(
						2, 2
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					3, new ClientData(
						"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
					), new ShopData(
						"ShopName3", new Address("City3", "Street3", 3)
					), new GoodsData(
						3, 3
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					1, new ClientData(
						"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
					), new ShopData(
						"ShopName1", new Address("City1", "Street1", 1)
					), new GoodsData(
						1, 1
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					2, new ClientData(
						"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
					), new ShopData(
						"ShopName2", new Address("City2", "Street2", 2)
					), new GoodsData(
						2, 2
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					3, new ClientData(
						"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
					), new ShopData(
						"ShopName3", new Address("City3", "Street3", 3)
					), new GoodsData(
						3, 3
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					1, new ClientData(
						"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
					), new ShopData(
						"ShopName1", new Address("City1", "Street1", 1)
					), new GoodsData(
						1, 1
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					2, new ClientData(
						"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
					), new ShopData(
						"ShopName2", new Address("City2", "Street2", 2)
					), new GoodsData(
						2, 2
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					3, new ClientData(
						"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
					), new ShopData(
						"ShopName3", new Address("City3", "Street3", 3)
					), new GoodsData(
						3, 3
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					1, new ClientData(
						"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
					), new ShopData(
						"ShopName1", new Address("City1", "Street1", 1)
					), new GoodsData(
						1, 1
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					2, new ClientData(
						"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
					), new ShopData(
						"ShopName2", new Address("City2", "Street2", 2)
					), new GoodsData(
						2, 2
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					3, new ClientData(
						"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
					), new ShopData(
						"ShopName3", new Address("City3", "Street3", 3)
					), new GoodsData(
						3, 3
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					1, new ClientData(
						"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
					), new ShopData(
						"ShopName1", new Address("City1", "Street1", 1)
					), new GoodsData(
						1, 1
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					2, new ClientData(
						"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
					), new ShopData(
						"ShopName2", new Address("City2", "Street2", 2)
					), new GoodsData(
						2, 2
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				},
				new object[]
				{
					3, new ClientData(
						"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
					), new ShopData(
						"ShopName3", new Address("City3", "Street3", 3)
					), new GoodsData(
						3, 3
					),
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					)
				}
			};
		}
	}
}
