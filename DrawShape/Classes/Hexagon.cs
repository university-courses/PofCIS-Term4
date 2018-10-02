using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace DrawShape.Classes
{
	[Serializable]
	public class Hexagon
	{
		[XmlAttribute]
		public Point[] Points { get; set; }

		[XmlAnyAttribute]
		public int R { get; set; }
		
		[XmlAnyAttribute]
		public int G { get; set; }
		
		[XmlAnyAttribute]
		public int B { get; set; }
		
		[XmlAttribute]
		public string Name { get; set; }

		public Hexagon(string name, List<Point> points, Color color)
		{
			Name = name;
			Points = points.ToArray();
			R = color.R;
			G = color.G;
			B = color.B;
		}

		public Polygon ToPolygon()
		{
			var polygon = new Polygon();
			foreach (var point in Points)
			{
				polygon.Points.Add(new System.Windows.Point(point.X, point.Y));
			}
			var color = Color.FromRgb((byte)R, (byte)G, (byte)B);
			polygon.Stroke = new SolidColorBrush(color);
			polygon.Fill = new SolidColorBrush(color);
			return polygon;
		}
	}
}
