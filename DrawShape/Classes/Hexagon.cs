using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace DrawShape.Classes
{
	[Serializable]
	public class Hexagon
	{
		[XmlArray]
		public Point[] Points { get; set; }

		[XmlAttribute]
		public int R { get; set; }
		
		[XmlAttribute]
		public int G { get; set; }
		
		[XmlAttribute]
		public int B { get; set; }
		
		[XmlAttribute]
		public string Name { get; set; }

		public Hexagon()
		{
		}
		
		public Hexagon(string name, List<Point> points, Brush brush)
		{
			if (points == null)
			{
				throw new NullReferenceException("points are null");
			}
			if (points.Count != 6)
			{
				throw new InvalidDataException("hexagon requires six points");
			}
			Name = name;
			Points = points.ToArray();
			var color = ((SolidColorBrush) brush).Color;
			R = color.R;
			G = color.G;
			B = color.B;
		}

		public Polygon ToPolygon()
		{
			if (Points == null)
			{
				throw new NullReferenceException("points are null");
			}
			if (Points.Length != 6)
			{
				throw new InvalidDataException("hexagon requires six points");
			}
			var polygon = new Polygon();
			foreach (var point in Points)
			{
				polygon.Points.Add(new System.Windows.Point(point.X, point.Y));
			}
			var color = Color.FromRgb((byte)R, (byte)G, (byte)B);
			polygon.Stroke = new SolidColorBrush(color);
			polygon.Fill = new SolidColorBrush(color);
			polygon.Name = Name;
			return polygon;
		}

		public static Hexagon FromPolygon(Polygon polygon)
		{
			var points = new List<Point>();
			foreach (var point in polygon.Points)
			{
				points.Add(new Point(point.X, point.Y));
			}
			return new Hexagon(polygon.Name, points, polygon.Fill);
		}
	}
}
