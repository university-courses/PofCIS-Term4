using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CargoDelivery.Classes.OrderData
{
	[Serializable]
	public class ClientData
	{
		[XmlAttribute]
		public string FirstName { get; set; }
		
		[XmlAttribute]
		public string LastName { get; set; }
		
		[XmlAttribute]
		public string Email { get; set; }
		
		[XmlAttribute]
		public string PhoneNumber { get; set; }
		
		public Address Address { get; set; }

		public ClientData()
		{
		}

		public ClientData(
			string firstName,
			string lastName,
			string email,
			string phoneNumber,
			Address clientAddress
		)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
			Address = clientAddress;
		}

		public ClientData(XmlNode source)
		{
			if (source == null)
			{
				throw new NullReferenceException("can't parse ClientData");
			}
			
			if (source.Attributes == null)
			{
				throw new NullReferenceException("can't parse ClientData attributes");
			}

			FirstName = source.Attributes["FirstName"].Value;
			LastName = source.Attributes["LastName"].Value;
			Email = source.Attributes["Email"].Value;
			PhoneNumber = source.Attributes["PhoneNumber"].Value;
			var addressNode = source.SelectSingleNode("Address");
			if (addressNode == null)
			{
				throw new NullReferenceException("can't parse ClientData.Address");
			}

			Address = new Address(addressNode.Attributes);
		}

		public XElement ToXml()
		{
			return new XElement("ClientData",
				new XAttribute("FirstName", FirstName),
				new XAttribute("LastName", LastName),
				new XAttribute("Email", Email),
				new XAttribute("PhoneNumber", PhoneNumber),
				Address.ToXml()
			);
		}
	}
}
