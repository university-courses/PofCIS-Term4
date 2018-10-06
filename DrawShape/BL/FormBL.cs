using Microsoft.Win32;

using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;

using DrawShape.Utils;
using DrawShape.Classes;

namespace DrawShape.BL
{
	/// <summary>
	/// Class for business logick of the application.
	/// </summary>
	public static class FormBl
	{
		/// <summary>
		/// Function to pick a new colour.
		/// </summary>
		/// <param name="current">Parameter where the picked colour will be stored.</param>
		/// <param name="picker">Rectangle where picked colour will be displayed.</param>
		public static void SetColor(ref Brush current, ref Rectangle picker)
		{
			if (!Util.GetColorFromColorDilog(out var color))
			{
				return;
			}

			current = color;
			picker.Fill = color;
		}

		/// <summary>
		/// Function to read hexagons from xml file.
		/// </summary>
		/// <returns>Hexagons read.</returns>
		public static IEnumerable<Hexagon> ReadHexagons()
		{
			var ofd = new OpenFileDialog { Filter = @"Xml files (*.xml)|*.xml", DefaultExt = "xml", AddExtension = true};
			return ofd.ShowDialog() == true ? Serialization.DeserializeHexagons(ofd.FileName) : null;
		}

		/// <summary>
		/// Function to save hexagons to xml file.
		/// </summary>
		/// <param name="canvas">Canvas from which hexagons are to be saved.</param>
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
