using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using CargoDelivery.BL;

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

		public Order Retrieve(long id)
		{
			var doc = new XmlDocument();
			var node = FindNode(id, ref doc);
			if (node == null)
			{
				throw new Exception("data not found");
			}
			return new Order(node);
		}

		public Dictionary<long, string> RetrieveAllIds()
		{
			var dict = new Dictionary<long, string>();
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

					dict[long.Parse(current.Attributes["Id"].Value)] =
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
		
		public void Update(long oldOrderId, Order newOrder)
		{
			var doc = new XmlDocument();
			var node = FindNode(oldOrderId, ref doc);
			if (node.Attributes == null)
			{
				return;
			}

			Order.EditXmlNode(ref node, newOrder);
			doc.Save(_path);
		}

		public void Remove(long id)
		{
			var doc = new XmlDocument();
			doc.Load(_path);
			var iterator = doc.SelectNodes("/ArrayOfOrder/Order")?.GetEnumerator();
			while (iterator != null && iterator.MoveNext())
			{
				if (iterator.Current is XmlNode current)
				{
					if (current.Attributes != null && (current.Attributes != null || current.Attributes["Id"].Value.Equals(id.ToString())))
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

		public XmlNode FindNode(long id, ref XmlDocument document)
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

		public void DeleteIfExists()
		{
			if (StorageExists())
			{
				File.Delete(_path);
			}
		}
		
		public bool OrderExists(long id)
		{
			var doc = new XmlDocument();
			doc.Load(_path);
			return FindNode(id, ref doc) != null;
		}
	}
}
