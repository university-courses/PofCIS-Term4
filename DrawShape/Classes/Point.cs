using System;
using System.Xml.Serialization;

namespace DrawShape.Classes
{
	[Serializable]
	public struct Point
	{
		[XmlAttribute]
		public double X { get; set; }
		
		[XmlAttribute]
		public double Y { get; set; }
	}
}
