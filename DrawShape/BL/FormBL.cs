using Microsoft.Win32;

using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;

using DrawShape.Utils;
using DrawShape.Classes;

namespace DrawShape.BL
{
	public static class FormBl
	{
		public static void SetColor(ref Brush current, ref Rectangle picker)
		{
			if (!Util.GetColorFromColorDilog(out var color))
			{
				return;
			}

			current = color;
			picker.Fill = color;
		}

		public static IEnumerable<Hexagon> ReadHexagons()
		{
			var ofd = new OpenFileDialog { Filter = @"Xml files (*.xml)|*.xml", DefaultExt = "xml", AddExtension = true};
			return ofd.ShowDialog() == true ? Serialization.DeserializeHexagons(ofd.FileName) : null;
		}

		public static void SaveHexagons(ref Canvas canvas)
		{
			var sfd = new SaveFileDialog {Filter = @"Xml files (*.xml)|*.xml", DefaultExt = "xml", FileName = "Rectangles", AddExtension = true};
			if (sfd.ShowDialog() != true)
			{
				return;
			}

			var hexagons = new List<Hexagon>();
			foreach (var item in canvas.Children)
			{
				if (item is Polygon polygon)
				{
					hexagons.Add(Hexagon.FromPolygon(polygon));
				}
			}

			Serialization.SerializeHexagons(sfd.FileName, hexagons);
		}
	}
}
