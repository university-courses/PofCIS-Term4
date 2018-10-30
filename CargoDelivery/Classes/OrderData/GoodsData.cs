using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CargoDelivery.Classes.OrderData
{
	[Serializable]
	public class GoodsData
	{
		[XmlAttribute]
		public uint Code { get; set; }
		
		[XmlAttribute]
		public uint Amount { get; set; }

		public GoodsData()
		{
		}

		public GoodsData(uint code, uint amount)
		{
			Code = code;
			Amount = amount;
		}

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
			if (!uint.TryParse(source["Amount"].Value, out var amount))
			{
				throw new InvalidDataException("GoodsData.Amount must be of type 'uint'");
			}

			Amount = amount;
		}

		public XElement ToXml()
		{
			return new XElement("GoodsData",
				new XAttribute("Code", Code),
				new XAttribute("Amount", Amount)
			);
		}
	}
}
