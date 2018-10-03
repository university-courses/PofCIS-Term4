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
		public class BorderColor
		{
			[XmlAttribute]
			public int R { get; set; }
		
			[XmlAttribute]
			public int G { get; set; }
		
			[XmlAttribute]
			public int B { get; set; }
			
			public BorderColor()
			{
			}
			
			public BorderColor(int r, int g, int b)
			{
				R = r;
				G = g;
				B = b;
			}
		}
		
		public class FillColor
		{
			[XmlAttribute]
			public int R { get; set; }
		
			[XmlAttribute]
			public int G { get; set; }
		
			[XmlAttribute]
			public int B { get; set; }

			public FillColor()
			{
			}
			
			public FillColor(int r, int g, int b)
			{
				R = r;
				G = g;
				B = b;
			}
		}
		
		[XmlAttribute]
		public string Name { get; set; }

		[XmlElement]
		public FillColor ColorFill { get; set; }
		
		[XmlElement]
		public BorderColor ColorBorder { get; set; }
		
		[XmlArray]
		public Point[] Points { get; set; }

		public Hexagon()
		{
		}
		
		public Hexagon(string name, List<Point> points, Brush fillBrush, Brush borderBrush)
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
			var colorFill = ((SolidColorBrush) fillBrush).Color;
			ColorFill = new FillColor(colorFill.R, colorFill.G, colorFill.B);
			var colorBorder = ((SolidColorBrush) borderBrush).Color;
			ColorBorder = new BorderColor(colorBorder.R, colorBorder.G, colorBorder.B);
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
			polygon.Stroke = new SolidColorBrush(Color.FromRgb((byte)ColorBorder.R, (byte)ColorBorder.G, (byte)ColorBorder.B));
			polygon.Fill = new SolidColorBrush(Color.FromRgb((byte)ColorFill.R, (byte)ColorFill.G, (byte)ColorFill.B));
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
			return new Hexagon(polygon.Name, points, polygon.Fill, polygon.Stroke);
		}
	}
}
