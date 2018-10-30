using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CargoDelivery.Classes.OrderData
{
	[Serializable]
	public class Address
	{
		[XmlAttribute]
		public string City { get; set; }
		
		[XmlAttribute]
		public string Street { get; set; }
		
		[XmlAttribute]
		public uint BuildingNumber { get; set; }

		public Address()
		{
		}

		public Address(string city, string street, uint buildingNumber)
		{
			City = city;
			Street = street;
			BuildingNumber = buildingNumber;
		}

		public Address(XmlAttributeCollection source)
		{
			if (source == null)
			{
				throw new NullReferenceException("can't parse Address");
			}

			City = source["City"].Value;
			Street = source["Street"].Value;
			if (!uint.TryParse(source["BuildingNumber"].Value, out var buidingNumber))
			{
				throw new InvalidDataException("Address.BuildingNumber must be of type 'uint'");
			}

			BuildingNumber = buidingNumber;
		}

		public XElement ToXml()
		{
			return new XElement("Address",
				new XAttribute("City", City),
				new XAttribute("Street", Street),
				new XAttribute("BuildingNumber", BuildingNumber)
			);
		}
	}
}
