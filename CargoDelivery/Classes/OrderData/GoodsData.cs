using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CargoDelivery.Classes.OrderData
{
	/// <summary>
	/// Class to represent an GoodsData.
	/// </summary>
	[Serializable]
	public class GoodsData
	{
		/// <summary>
		/// Code a code number.
		/// </summary>
		[XmlAttribute]
		public uint Code { get; set; }
		
		/// <summary>
		/// An amount of weight.
		/// </summary>
		[XmlAttribute]
		public double Weight { get; set; }

		public GoodsData()
		{
		}

		/// <summary>
		/// Function to set data.
		/// </summary>
		/// <param name="code">A code number.</param>
		/// <param name="weight">An amount of weight.</param>
		public GoodsData(uint code, double weight)
		{
			Code = code;
			Weight = weight;
		}

		/// <summary>
		/// An GoodsData class constructor creates its object using data of type XmlAttributeCollection.
		/// </summary>
		public GoodsData(XmlAttributeCollection source)
		{
			if (source == null)
			{
				throw new NullReferenceException("can't parse GoodsData");
			}
			
			if (!uint.TryParse(source["Code"].Value, out var code))
			{
				throw new InvalidDataException("GoodsData.Code must be of type 'uint'");
			}

			Code = code;
			if (!double.TryParse(source["Weight"].Value, out var weight))
			{
				throw new InvalidDataException("GoodsData.Weight must be of type 'uint'");
			}

			Weight = weight;
		}

		/// <summary>
		/// Transfer the GoodsData object to Xml Element.
		/// </summary>
		public XElement ToXml()
		{
			return new XElement("GoodsData",
				new XAttribute("Code", Code),
				new XAttribute("Weight", Weight)
			);
		}
	}
}
