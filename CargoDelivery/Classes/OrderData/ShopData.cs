using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CargoDelivery.Classes.OrderData
{
	/// <summary>
	/// Class to represent a shop data.
	/// </summary>
	[Serializable]
	public class ShopData
	{
		/// <summary>
		/// A name of the shop.
		/// </summary>
		[XmlAttribute]
		public string Name { get; set; }
		
		/// <summary>
		/// An address of the shop.
		/// </summary>
		public Address Address { get; set; }

		/// <summary>
		/// Default constructor is used for xml serialization/deserialization. 
		/// </summary>
		public ShopData()
		{
			Address = new Address();
		}

		/// <summary>
		/// Constructor to set data from multiple parameters.
		/// </summary>
		/// <param name="name">A name of the shop</param>
		/// <param name="shopAddress">An address of the shop.</param>
		public ShopData(string name, Address shopAddress)
		{
			Name = name.Trim();
			Address = shopAddress;
		}

		/// <summary>
		/// Constructor creates its object using XmlNode data.
		/// </summary>
		public ShopData(XmlNode source)
		{
			if (source == null)
			{
				throw new NullReferenceException("can't parse ShopData");
			}
			
			var attributes = source.Attributes;
			if (attributes == null)
			{
				throw new NullReferenceException("can't parse ShopData.Name");
			}

			Name = attributes["Name"].Value;
			var addressElement = source.SelectSingleNode("Address");
			if (addressElement == null)
			{
				throw new NullReferenceException("can't parse ShopData.Address");
			}

			Address = new Address(addressElement.Attributes);
		}

		/// <summary>
		/// Transfers ShopData object to XElement.
		/// </summary>
		public XElement ToXml()
		{
			return new XElement(
				"ShopData",
				new XAttribute("Name", Name),
				Address.ToXml());
		}
	}
}
