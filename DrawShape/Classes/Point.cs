using System;
using System.IO;
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

		public Point(double x, double y)
		{
			X = x;
			Y = y;
		}
	}
}
