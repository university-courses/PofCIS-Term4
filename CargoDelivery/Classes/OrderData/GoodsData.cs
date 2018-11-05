using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CargoDelivery.Classes.OrderData
{
	/// <summary>
	/// Class to represent a goods data.
	/// </summary>
	[Serializable]
	public class GoodsData
	{
		/// <summary>
		/// Code a code of goods.
		/// </summary>
		[XmlAttribute]
		public uint Code { get; set; }
		
		/// <summary>
		/// Weight of goods.
		/// </summary>
		[XmlAttribute]
		public double Weight { get; set; }

		/// <summary>
		/// Default constructor is used for xml serialization/deserialization. 
		/// </summary>
		public GoodsData()
		{
		}

		/// <summary>
		/// Constructor to set data from multiple parameters.
		/// </summary>
		/// <param name="code">A code of goods.</param>
		/// <param name="weight">Weight of goods.</param>
		public GoodsData(uint code, double weight)
		{
			Code = code;
			Weight = weight;
		}

		/// <summary>
		/// Constructor creates its object using XmlAttributeCollection data.
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
		/// Transfers the GoodsData object to XElement.
		/// </summary>
		public XElement ToXml()
		{
			return new XElement(
				"GoodsData",
				new XAttribute("Code", Code),
				new XAttribute("Weight", Weight));
		}
	}
}
