using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CargoDelivery.Classes.OrderData
{
	/// <summary>
	/// Class to represent an ShopData.
	/// </summary>
	[Serializable]
	public class ShopData
	{
		/// <summary>
		/// Name.
		/// </summary>
		[XmlAttribute]
		public string Name { get; set; }
		
		public Address Address { get; set; }

		public ShopData()
		{
		}

		/// <summary>
		/// Function to set data.
		/// </summary>
		/// <param name="name">A name.</param>
		/// <param name="shopAddress">An object of the class Address.</param>
		public ShopData(string name, Address shopAddress)
		{
			Name = name;
			Address = shopAddress;
		}

		/// <summary>
		/// An ShopData class constructor creates its object using data of type XmlAttributeCollection.
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
		/// Transfer the ShopData object to Xml Element.
		/// </summary>
		public XElement ToXml()
		{
			return new XElement("ShopData",
				new XAttribute("Name", Name),
				Address.ToXml()
			);
		}
	}
}
