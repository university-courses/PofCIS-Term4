using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace DrawShape.Classes
{
    /// <summary>
    /// Class to represent a hexagon in two-dimensional space.
    /// </summary>
	[Serializable]
	public class Hexagon
	{
        /// <summary>
        /// Class to represent a border of hexagon.
        /// </summary>
		public class BorderColor
		{
            /// <summary>
            /// Amount of red colour in RGB specification.
            /// </summary>
			[XmlAttribute]
			public int R { get; set; }

            /// <summary>
            /// Amount of green colour in RGB specification.
            /// </summary>
            [XmlAttribute]
			public int G { get; set; }

            /// <summary>
            /// Amount of blue colour in RGB specification.
            /// </summary>
            [XmlAttribute]
			public int B { get; set; }

            /// <summary>
            /// Function to set colour of the border.
            /// </summary>
            public BorderColor()
			{
			}

            /// <summary>
            /// Function to set colour of the border using rgb specification.
            /// </summary>
            /// <param name="r">Amount of red.</param>
            /// <param name="g">Amount of green.</param>
            /// <param name="b">Amount of blue.</param>
            public BorderColor(int r, int g, int b)
			{
				R = r;
				G = g;
				B = b;
			}
		}
		
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Hexagon"/> class.  
        /// </summary>
		public Hexagon()
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Hexagon"/> class. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="points"></param>
        /// <param name="fillBrush"></param>
        /// <param name="borderBrush"></param>
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
