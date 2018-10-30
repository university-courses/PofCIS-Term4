using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CargoDelivery.Classes
{
	public class OrdersStorage
	{
		private readonly string _path;
		
		public OrdersStorage(string path)
		{
			_path = path;
		}
		
		public void CreateIfNotExists()
		{
			if (StorageExists())
			{
				return;
			}
			Stream stream = new FileStream(_path, FileMode.Create);
			new XmlSerializer(typeof(List<Order>)).Serialize(stream, new List<Order>());
			stream.Close();
		}

		public Order Retrieve(uint id)
		{
			var doc = new XmlDocument();
			var node = FindNode(id, ref doc);
			if (node == null)
			{
				throw new Exception("data not found");
			}
			return new Order(node);
		}

		public Dictionary<string, string> RetrieveAllIds()
		{
			var dict = new Dictionary<string, string>();
			var doc = new XmlDocument();
			doc.Load(_path);
			var iterator = doc.SelectNodes("/ArrayOfOrder/Order")?.GetEnumerator();
			while (iterator != null && iterator.MoveNext())
			{
				if (iterator.Current is XmlNode current)
				{
					if (current.Attributes == null)
					{
						continue;
					}

					var owner = current.SelectSingleNode("ClientData");
					if (owner?.Attributes == null)
					{
						continue;
					}

					if (current.Attributes == null)
					{
						continue;
					}

					dict[current.Attributes["Id"].Value] =
						owner.Attributes["FirstName"].Value + " " + owner.Attributes["LastName"].Value;
				}
				else
				{
					throw new NullReferenceException("storage data is corrupted");
				}
			}

			return dict;
		}

		public void Add(Order order)
		{
			var xmlDoc = new XmlDocument();
			if (FindNode(order.Id, ref xmlDoc) != null)
			{
				throw new InvalidDataException("order already exists");
			}

			var doc = XDocument.Load(_path);
			var orders = doc.Element("ArrayOfOrder");
			orders?.Add(order.ToXml());
			doc.Save(_path);
		}
		
		public void Update(uint oldOrderId, Order newOrder)
		{
			var doc = new XmlDocument();
			var node = FindNode(oldOrderId, ref doc);
			if (node.Attributes == null)
			{
				return;
			}
			Order.Edit(ref node, newOrder);
			doc.Save(_path);
		}

		public void Remove(uint id)
		{
			var doc = new XmlDocument();
			doc.Load(_path);
			var iterator = doc.SelectNodes("/ArrayOfOrder/Order")?.GetEnumerator();
			while (iterator != null && iterator.MoveNext())
			{
				if (iterator.Current is XmlNode current)
				{
					if (current.Attributes != null || current.Attributes["Id"].Value.Equals(id.ToString()))
					{
						current.ParentNode?.RemoveChild(current);
						doc.Save(_path);
						return;
					}
				}
				else
				{
					throw new NullReferenceException("storage data is corrupted");
				}
			}
		}

		public XmlNode FindNode(uint id, ref XmlDocument document)
		{
			document.Load(_path);
			var iterator = document.SelectNodes("/ArrayOfOrder/Order")?.GetEnumerator();
			while (iterator != null && iterator.MoveNext())
			{
				if (iterator.Current is XmlNode current)
				{
					if (current.Attributes != null && current.Attributes["Id"].Value.Equals(id.ToString()))
					{
						return current;
					}
				}
				else
				{
					throw new NullReferenceException("storage data is corrupted");
				}
			}

			return null;
		}
		
		public bool StorageExists()
		{
			return File.Exists(_path);
		}
	}
}
