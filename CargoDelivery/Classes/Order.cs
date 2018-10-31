using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using System.Xml.Serialization;

using CargoDelivery.Classes.OrderData;

namespace CargoDelivery.Classes
{
	[Serializable]
	public class Order
	{
		[XmlAttribute]
		public uint Id { get; set; }
		
		public ClientData ClientData { get; set; }
		
		public ShopData ShopData { get; set; }
		
		public GoodsData GoodsData { get; set; }

		public Order(XmlNode source)
		{
			if (source.Attributes == null)
			{
				throw new InvalidDataException("invalid xml source content");
			}
			
			if (!uint.TryParse(source.Attributes["Id"].Value, out var id))
			{
				throw new InvalidDataException("Order.Id must be of type 'uint'");
			}
			
			Id = id;
			ClientData = new ClientData(source.SelectSingleNode("ClientData"));
			ShopData = new ShopData(source.SelectSingleNode("ShopData"));
			var goodsData = source.SelectSingleNode("GoodsData");
			if (goodsData == null)
			{
				return;
			}

			GoodsData = new GoodsData(goodsData.Attributes);
		}
		
		public Order(uint id, ClientData clientData, ShopData shopData, GoodsData goodsData)
		{
			Id = id;
			ClientData = clientData;
			ShopData = shopData;
			GoodsData = goodsData;
		}
		
		public Order()
		{
		}

		public XElement ToXml()
		{
			return new XElement("Order",
				new XAttribute("Id", Id), ClientData.ToXml(), GoodsData.ToXml(), ShopData.ToXml()
			);
		}

		public static void Edit(ref XmlNode node, Order @new)
		{
			if (node == null)
			{
				throw new NullReferenceException("can't edit node");
			}
			
			if (node.Attributes != null)
			{
				node.Attributes["Id"].Value = @new.Id.ToString();	
			}

			var clientData = node.SelectSingleNode("ClientData");
			if (clientData == null)
			{
				throw new NullReferenceException("can't edit ClientData");
			}

			if (clientData.Attributes == null)
			{
				throw new NullReferenceException("can't edit ClientData.Attributes");
			}

			clientData.Attributes["FirstName"].Value = @new.ClientData.FirstName;
			clientData.Attributes["LastName"].Value = @new.ClientData.LastName;
			clientData.Attributes["Email"].Value = @new.ClientData.Email;
			clientData.Attributes["PhoneNumber"].Value = @new.ClientData.PhoneNumber;
			var clientAddress = clientData.SelectSingleNode("Address");
			if (clientAddress == null)
			{
				throw new NullReferenceException("can't edit ClientData.Address");
			}

			if (clientAddress.Attributes == null)
			{
				throw new NullReferenceException("can't edit ClientData.Address.Attributes");
			}

			clientAddress.Attributes["City"].Value = @new.ClientData.Address.City;
			clientAddress.Attributes["Street"].Value = @new.ClientData.Address.Street;
			clientAddress.Attributes["BuildingNumber"].Value = @new.ClientData.Address.BuildingNumber.ToString();
			var shopData = node.SelectSingleNode("ShopData");
			if (shopData == null)
			{
				throw new NullReferenceException("can't edit Order.ShopData");
			}

			if (shopData.Attributes == null)
			{
				throw new NullReferenceException("can't edit Order.ShopData.Attributes");
			}

			shopData.Attributes["Name"].Value = @new.ShopData.Name;
			var shopAddress = shopData.SelectSingleNode("Address");
			if (shopAddress == null)
			{
				throw new NullReferenceException("can't edit ShopData.Address");
			}

			if (shopAddress.Attributes == null)
			{
				throw new NullReferenceException("can't edit ShopData.Address.Attributes");
			}

			shopAddress.Attributes["City"].Value = @new.ShopData.Address.City;
			shopAddress.Attributes["Street"].Value = @new.ShopData.Address.Street;
			shopAddress.Attributes["BuildingNumber"].Value = @new.ShopData.Address.BuildingNumber.ToString();

			var goodsData = node.SelectSingleNode("GoodsData");
			if (goodsData == null)
			{
				throw new NullReferenceException("can't edit Order.GoodsData");
			}

			if (goodsData.Attributes == null)
			{
				throw new NullReferenceException("can't edit Order.GoodsData.Attributes");
			}

			goodsData.Attributes["Code"].Value = @new.GoodsData.Code.ToString();
			goodsData.Attributes["Weight"].Value = @new.GoodsData.Weight.ToString(CultureInfo.InvariantCulture);
		}
	}
}
