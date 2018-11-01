using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CargoDelivery.Classes.OrderData
{

	/// <summary>
	/// Class to represent an Address.
	/// </summary>
	[Serializable]
	public class Address
	{
		/// <summary>
		/// City a name of the city.
		/// </summary>
		[XmlAttribute]
		public string City { get; set; }
		
		/// <summary>
		/// Street a name of the street.
		/// </summary>
		[XmlAttribute]
		public string Street { get; set; }
		
		/// <summary>
		/// BuildingNumber a number of the building.
		/// </summary>
		[XmlAttribute]
		public uint BuildingNumber { get; set; }

		public Address()
		{
		}

		/// <summary>
		/// Function to set data.
		/// </summary>
		/// <param name="city">A name of the city.</param>
		/// <param name="street">A name of the street.</param>
		/// <param name="buildingNumber">A number of the building.</param>
		public Address(string city, string street, uint buildingNumber)
		{
			City = city;
			Street = street;
			BuildingNumber = buildingNumber;
		}

		/// <summary>
		/// An Address class constructor creates its object using data of type XmlAttributeCollection.
		/// </summary>
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

		/// <summary>
		/// Transfer the Address object to Xml Element.
		/// </summary>
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
