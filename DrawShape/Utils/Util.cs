using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawShape.Utils
{
	public static class Util
	{
		public static int GetHexagonIdByClickPos(Classes.Point clickPosition, UIElementCollection elements)
		{
			var hexagons = elements.OfType<Polygon>();
			var enumerable = hexagons as Polygon[] ?? hexagons.ToArray();
			for (var i = enumerable.Length - 1; i >= 0; i--)
			{
				if (PointIsInHexagon(clickPosition, enumerable[i]))
				{
					return i;
				}
			}

			throw new InvalidDataException("hexagon does not exist");
		}

		public static bool PointIsInHexagon(Classes.Point point, Polygon hexagon)
		{
			// TODO: check if mouse is clicked on hexagon
			
			return false;
		}
		
		/// <summary>
		/// Returns an index of a hexagon with specified name.
		/// </summary>
		/// <param name="name">Hexagon's name.</param>
		/// <param name="elements">A list of available hexagons.</param>
		/// <returns>Index of hexagon.</returns>
		/// <exception cref="InvalidDataException">Throws if hexagon does not exist.</exception>
		public static int GetHexagonIdByName(string name, UIElementCollection elements)
		{
			var hexagons = elements.OfType<Polygon>();
			var enumerable = hexagons as Polygon[] ?? hexagons.ToArray();
			for (var i = 0; i < enumerable.Length; i++)
			{
				if (enumerable[i].Name == name)
				{
					return i;
				}
			}

			throw new InvalidDataException("hexagon does not exist");
		}
		
		public static bool GetColorFromColorDilog(out Brush brush)
		{
			var colorDialog = new ColorDialog {AllowFullOpen = true};
			var dialogResult = colorDialog.ShowDialog();
			brush = new SolidColorBrush();
			if (dialogResult != DialogResult.OK)
			{
				return false;
			}
			brush = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
			return true;
		}
	}
}
