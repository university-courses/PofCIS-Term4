using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace CargoDelivery.Classes
{
	/// <summary>
	/// Represents an order storage object.
	/// </summary>
	public class OrdersStorage
	{
		/// <summary>
		/// Holds storage path.
		/// </summary>
		private readonly string _path;
		
		/// <summary>
		/// Constructs an object which can connect with storage file.
		/// </summary>
		/// <param name="path"></param>
		public OrdersStorage(string path)
		{
			_path = path;
		}
		
		/// <summary>
		/// Creates new storage file if it does not already exist.
		/// </summary>
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

		/// <summary>
		/// Retrieves oredr object by its id.
		/// </summary>
		/// <param name="id">Order id.</param>
		/// <returns>Order object.</returns>
		/// <exception cref="Exception">Throws if object with given id does not exist.</exception>
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

		/// <summary>
		/// Retrieve orders basic data from the storage.
		/// </summary>
		/// <returns>
		/// Dictionary where key is an id and value is the concatenation of the first and last name of order's author.
		/// </returns>
		/// <exception cref="NullReferenceException">Throws if storage file is broken.</exception>
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

		/// <summary>
		/// Appends new order to the storage.
		/// </summary>
		/// <param name="order">An order to be appended.</param>
		/// <exception cref="InvalidDataException">Throws if the order with given id already exists.</exception>
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
		
		/// <summary>
		/// Updates existent order by its id.
		/// </summary>
		/// <param name="oldOrderId">An id of editable order.</param>
		/// <param name="newOrder">New order data.</param>
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

		/// <summary>
		/// Deletes order from the storage by its id.
		/// </summary>
		/// <param name="id">Order id.</param>
		/// <exception cref="NullReferenceException">Throws if storage file is broken.</exception>
		public void Remove(long id)
		{
			var doc = new XmlDocument();
			doc.Load(_path);
			var iterator = doc.SelectNodes("/ArrayOfOrder/Order")?.GetEnumerator();
			while (iterator != null && iterator.MoveNext())
			{
				if (iterator.Current is XmlNode current)
				{
					if (current.Attributes != null && current.Attributes["Id"].Value.Equals(id.ToString()))
					{
						var parentNode = current.ParentNode;
						if (parentNode == null)
						{
							throw new NullReferenceException("storage data is corrupted");
						}

						parentNode.RemoveChild(current);
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

		/// <summary>
		/// Searches for xml order node by its id.
		/// </summary>
		/// <param name="id">An id of the order.</param>
		/// <param name="document">Storage document.</param>
		/// <returns>Order in XmlNode representation.</returns>
		/// <exception cref="NullReferenceException">Throws if storage file is broken.</exception>
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
		
		/// <summary>
		/// Checks if storage exists.
		/// </summary>
		/// <returns>True if storage exists, otherwise returns false.</returns>
		public bool StorageExists()
		{
			return File.Exists(_path);
		}

		/// <summary>
		/// Deletes storage file if it exists.
		/// </summary>
		public void DeleteIfExists()
		{
			if (StorageExists())
			{
				File.Delete(_path);
			}
		}
		
		/// <summary>
		/// Checks if an order exists by its id. 
		/// </summary>
		/// <param name="id">Order id.</param>
		/// <returns>True if order with given id exists, otherwise returns false.</returns>
		public bool OrderExists(long id)
		{
			var doc = new XmlDocument();
			doc.Load(_path);
			return FindNode(id, ref doc) != null;
		}
	}
}
