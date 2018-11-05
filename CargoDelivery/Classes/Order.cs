using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using System.Xml.Serialization;

using CargoDelivery.Classes.OrderData;

namespace CargoDelivery.Classes
{
	/// <summary>
	/// Represents an order.
	/// </summary>
	[Serializable]
	public class Order
	{
		/// <summary>
		/// Holds an id of the order.
		/// </summary>
		[XmlAttribute]
		public long Id { get; set; }
		
		/// <summary>
		/// Contains client personal information.
		/// </summary>
		public ClientData ClientData { get; set; }
		
		/// <summary>
		/// Represents shop data.
		/// </summary>
		public ShopData ShopData { get; set; }
		
		/// <summary>
		/// Holds an information about ordered goods.
		/// </summary>
		public GoodsData GoodsData { get; set; }

		/// <summary>
		/// Constructs order object from xml node source.
		/// </summary>
		/// <param name="source">Xml node object which contains order data.</param>
		/// <exception cref="InvalidDataException">
		/// Throws if xml node attricutes is null or if id value is not long type.
		/// </exception>
		public Order(XmlNode source)
		{
			if (source.Attributes == null)
			{
				throw new InvalidDataException("invalid xml source content");
			}
			
			if (!long.TryParse(source.Attributes["Id"].Value, out var id))
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
		
		/// <summary>
		/// Constructor with parameters.
		/// </summary>
		/// <param name="id">An id of the order.</param>
		/// <param name="clientData">Client data object.</param>
		/// <param name="shopData">Shop data object.</param>
		/// <param name="goodsData">Goods data object.</param>
		public Order(long id, ClientData clientData, ShopData shopData, GoodsData goodsData)
		{
			Id = id;
			ClientData = clientData;
			ShopData = shopData;
			GoodsData = goodsData;
		}
		
		/// <summary>
		/// Parameterless constructor.
		/// </summary>
		public Order()
		{
			ClientData = new ClientData();
			ShopData = new ShopData();
			GoodsData = new GoodsData();
		}

		/// <summary>
		/// Order to Xml object converter.
		/// </summary>
		/// <returns>Order representation as xml element.</returns>
		public XElement ToXml()
		{
			return new XElement(
				"Order",
				new XAttribute("Id", Id),
				ClientData.ToXml(),
				GoodsData.ToXml(),
				ShopData.ToXml());
		}

		/// <summary>
		/// Updates xml node by reference.
		/// </summary>
		/// <param name="node">Current editing xml node.</param>
		/// <param name="new">New order to be set.</param>
		/// <exception cref="NullReferenceException">Throws if node is null or contains invalid data.</exception>
		public static void EditXmlNode(ref XmlNode node, Order @new)
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
