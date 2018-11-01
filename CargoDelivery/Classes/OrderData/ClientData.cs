using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CargoDelivery.Classes.OrderData
{
	/// <summary>
	/// Class to represent Orders.
	/// </summary>
	[Serializable]
	public class ClientData
	{
		/// <summary>
		/// FirstName a name of the first name.
		/// </summary>
		[XmlAttribute]
		public string FirstName { get; set; }
		
		/// <summary>
		/// LasttName a name of the last name.
		/// </summary>
		[XmlAttribute]
		public string LastName { get; set; }
		
		/// <summary>
		/// Email a name of the email.
		/// </summary>
		[XmlAttribute]
		public string Email { get; set; }
		
		/// <summary>
		/// PhoneNumber a phone number.
		/// </summary>
		[XmlAttribute]
		public string PhoneNumber { get; set; }
		
		public Address Address { get; set; }

		public ClientData()
		{
		}

		/// <summary>
		/// Function to set data.
		/// </summary>
		/// <param name="firstName">A name of the first name.</param>
		/// <param name="lastName">A name of the last name.</param>
		/// <param name="email">A name of the email.</param>
		/// <param name="phoneNumber">A phone number.</param>
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

		/// <summary>
		/// An OrderData class constructor creates its object using data of type XmlAttributeCollection.
		/// </summary>
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

		/// <summary>
		/// Transfer the OrderData object to Xml Element.
		/// </summary>
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
