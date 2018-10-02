using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using DrawShape.Classes;

namespace DrawShape.Utils
{
	public static class Serialization
	{
		public static void SerializeHexagons(string fileName, List<Hexagon> hexagons)
		{
			Stream stream = new FileStream(fileName, FileMode.Create);
			new XmlSerializer(typeof(List<Hexagon>)).Serialize(stream, hexagons);
			stream.Close();
		}

		public static List<Hexagon> DeserializeHexagons(string fileName)
		{
			Stream stream = new FileStream(fileName, FileMode.Open);
			var result = new XmlSerializer(typeof(List<Hexagon>)).Deserialize(stream) as List<Hexagon>;
			stream.Close();
			return result;
		}
	}
}
