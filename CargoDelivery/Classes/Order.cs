using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CargoDelivery.Classes
{
	[Serializable]
	public class Order
	{
		[XmlAttribute]
		public uint Id { get; set; }

		public Order(XmlNode source)
		{
			if (source.Attributes == null)
			{
				throw new InvalidDataException("invalid xml source content");
			}

			Id = uint.Parse(source.Attributes["Id"].Value);

			// TODO: read all Order fields.
		}
		
		public Order(uint id)
		{
			Id = id;
		}
		
		public Order()
		{
		}
	}
}
