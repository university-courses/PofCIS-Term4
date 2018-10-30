using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CargoDelivery.Classes.OrderData
{
	[Serializable]
	public class ShopData
	{
		[XmlAttribute]
		public string Name { get; set; }
		
		public Address Address { get; set; }

		public ShopData()
		{
		}

		public ShopData(string name, Address shopAddress)
		{
			Name = name;
			Address = shopAddress;
		}

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

		public XElement ToXml()
		{
			return new XElement("ShopData",
				new XAttribute("Name", Name),
				Address.ToXml()
			);
		}
	}
}
