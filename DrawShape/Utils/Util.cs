using System.IO;
using System.Collections.Generic;

using DrawShape.Classes;

namespace DrawShape.Utils
{
	public static class Util
	{
		public static Hexagon GetHexagonByClick(Point clickPosition, List<Hexagon> hexagons)
		{
			for (var i = hexagons.Count - 1; i >= 0; i--)
			{
				if (PointIsInRect(clickPosition, hexagons[i]))
				{
					return hexagons[i];
				}
			}

			return null;
		}

		public static bool PointIsInRect(Point point, Hexagon rect)
		{
			// TODO: check if mouse is clicked on hexagon
			
			return false;
		}
		
		/// <summary>
		/// Returns an index of a hexagon with specified name.
		/// </summary>
		/// <param name="name">Hexagon's name.</param>
		/// <param name="hexagons">A list of available hexagons.</param>
		/// <returns>Index of hexagon.</returns>
		/// <exception cref="InvalidDataException">Throws if hexagon does not exist.</exception>
		public static int GetHexagonIdByName(string name, List<Hexagon> hexagons)
		{
			for (var i = 0; i < hexagons.Count; i++)
			{
				if (hexagons[i].Name == name)
				{
					return i;
				}
			}

			throw new InvalidDataException("hexagon does not exist");
		}
	}
}
