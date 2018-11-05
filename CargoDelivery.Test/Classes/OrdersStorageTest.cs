using Xunit;

using System.Linq;
using System.Collections.Generic;

using CargoDelivery.Classes;
using CargoDelivery.Classes.OrderData;

namespace CargoDelivery.Test.Classes
{
	public class OrdersStorageTest
	{
		[Fact]
		public void TestCreateIfNotExists()
		{
			const string storagePath = "storage.xml";
			var storage = new OrdersStorage(storagePath);
			Assert.False(storage.StorageExists());
			storage.CreateIfNotExists();
			Assert.True(storage.StorageExists());
			storage.DeleteIfExists();
			Assert.False(storage.StorageExists());
		}
		
		[Theory]
		[MemberData(nameof(TestData.AddData), MemberType = typeof(TestData))]
		public void TestAdd(Order order)
		{
			const string storagePath = "storage.xml";
			var storage = new OrdersStorage(storagePath);
			storage.DeleteIfExists();
			storage.CreateIfNotExists();
			storage.Add(order);
			var actual = storage.Retrieve(order.Id);
			storage.DeleteIfExists();
			Assert.Equal(order.Id, actual.Id);
			Assert.Equal(order.ShopData.Name, actual.ShopData.Name);
			Assert.Equal(order.ShopData.Address.City, actual.ShopData.Address.City);
			Assert.Equal(order.ShopData.Address.Street, actual.ShopData.Address.Street);
			Assert.Equal(order.ShopData.Address.BuildingNumber, actual.ShopData.Address.BuildingNumber);
			Assert.Equal(order.GoodsData.Code, actual.GoodsData.Code);
			Assert.Equal(order.GoodsData.Weight, actual.GoodsData.Weight);
			Assert.Equal(order.ClientData.FirstName, actual.ClientData.FirstName);
			Assert.Equal(order.ClientData.LastName, actual.ClientData.LastName);
			Assert.Equal(order.ClientData.Email, actual.ClientData.Email);
			Assert.Equal(order.ClientData.PhoneNumber, actual.ClientData.PhoneNumber);
			Assert.Equal(order.ClientData.Address.City, actual.ClientData.Address.City);
			Assert.Equal(order.ClientData.Address.Street, actual.ClientData.Address.Street);
			Assert.Equal(order.ClientData.Address.BuildingNumber, actual.ClientData.Address.BuildingNumber);
		}
		
		[Theory]
		[MemberData(nameof(TestData.RetrieveAllIdsData), MemberType = typeof(TestData))]
		public void TestRetrieveAllIds(List<Order> orders)
		{
			const string storagePath = "storage.xml";
			var storage = new OrdersStorage(storagePath);
			storage.DeleteIfExists();
			storage.CreateIfNotExists();
			foreach (var order in orders)
			{
				storage.Add(order);
			}

			var ls = storage.RetrieveAllIds();
			storage.DeleteIfExists();
			var ids = ls.Keys.ToArray();
			var owners = ls.Values.ToArray();
			for (var i = 0; i < orders.Count; i++)
			{
				Assert.Equal(orders[i].Id, ids[i]);
				Assert.Equal(orders[i].ClientData.FirstName + " " + orders[i].ClientData.LastName, owners[i]);
			}
		}
		
		[Theory]
		[MemberData(nameof(TestData.UpdateData), MemberType = typeof(TestData))]
		public void TestUpdate(Order order, Order newOrder)
		{
			const string storagePath = "storage.xml";
			var storage = new OrdersStorage(storagePath);
			storage.DeleteIfExists();
			storage.CreateIfNotExists();
			storage.Add(order);
			var actual = storage.Retrieve(order.Id);
			Assert.Equal(order.Id, actual.Id);
			Assert.Equal(order.ShopData.Name, actual.ShopData.Name);
			Assert.Equal(order.ShopData.Address.City, actual.ShopData.Address.City);
			Assert.Equal(order.ShopData.Address.Street, actual.ShopData.Address.Street);
			Assert.Equal(order.ShopData.Address.BuildingNumber, actual.ShopData.Address.BuildingNumber);
			Assert.Equal(order.GoodsData.Code, actual.GoodsData.Code);
			Assert.Equal(order.GoodsData.Weight, actual.GoodsData.Weight);
			Assert.Equal(order.ClientData.FirstName, actual.ClientData.FirstName);
			Assert.Equal(order.ClientData.LastName, actual.ClientData.LastName);
			Assert.Equal(order.ClientData.Email, actual.ClientData.Email);
			Assert.Equal(order.ClientData.PhoneNumber, actual.ClientData.PhoneNumber);
			Assert.Equal(order.ClientData.Address.City, actual.ClientData.Address.City);
			Assert.Equal(order.ClientData.Address.Street, actual.ClientData.Address.Street);
			Assert.Equal(order.ClientData.Address.BuildingNumber, actual.ClientData.Address.BuildingNumber);
			storage.Update(order.Id, newOrder);
			actual = storage.Retrieve(newOrder.Id);
			Assert.Equal(newOrder.Id, actual.Id);
			Assert.Equal(newOrder.ShopData.Name, actual.ShopData.Name);
			Assert.Equal(newOrder.ShopData.Address.City, actual.ShopData.Address.City);
			Assert.Equal(newOrder.ShopData.Address.Street, actual.ShopData.Address.Street);
			Assert.Equal(newOrder.ShopData.Address.BuildingNumber, actual.ShopData.Address.BuildingNumber);
			Assert.Equal(newOrder.GoodsData.Code, actual.GoodsData.Code);
			Assert.Equal(newOrder.GoodsData.Weight, actual.GoodsData.Weight);
			Assert.Equal(newOrder.ClientData.FirstName, actual.ClientData.FirstName);
			Assert.Equal(newOrder.ClientData.LastName, actual.ClientData.LastName);
			Assert.Equal(newOrder.ClientData.Email, actual.ClientData.Email);
			Assert.Equal(newOrder.ClientData.PhoneNumber, actual.ClientData.PhoneNumber);
			Assert.Equal(newOrder.ClientData.Address.City, actual.ClientData.Address.City);
			Assert.Equal(newOrder.ClientData.Address.Street, actual.ClientData.Address.Street);
			Assert.Equal(newOrder.ClientData.Address.BuildingNumber, actual.ClientData.Address.BuildingNumber);
			storage.DeleteIfExists();
		}
		
		[Theory]
		[MemberData(nameof(TestData.RemoveData), MemberType = typeof(TestData))]
		public void TestRemove(Order order)
		{
			const string storagePath = "storage.xml";
			var storage = new OrdersStorage(storagePath);
			storage.DeleteIfExists();
			storage.CreateIfNotExists();
			storage.Add(order);
			var actual = storage.Retrieve(order.Id);
			Assert.Equal(order.Id, actual.Id);
			Assert.Equal(order.ShopData.Name, actual.ShopData.Name);
			Assert.Equal(order.ShopData.Address.City, actual.ShopData.Address.City);
			Assert.Equal(order.ShopData.Address.Street, actual.ShopData.Address.Street);
			Assert.Equal(order.ShopData.Address.BuildingNumber, actual.ShopData.Address.BuildingNumber);
			Assert.Equal(order.GoodsData.Code, actual.GoodsData.Code);
			Assert.Equal(order.GoodsData.Weight, actual.GoodsData.Weight);
			Assert.Equal(order.ClientData.FirstName, actual.ClientData.FirstName);
			Assert.Equal(order.ClientData.LastName, actual.ClientData.LastName);
			Assert.Equal(order.ClientData.Email, actual.ClientData.Email);
			Assert.Equal(order.ClientData.PhoneNumber, actual.ClientData.PhoneNumber);
			Assert.Equal(order.ClientData.Address.City, actual.ClientData.Address.City);
			Assert.Equal(order.ClientData.Address.Street, actual.ClientData.Address.Street);
			Assert.Equal(order.ClientData.Address.BuildingNumber, actual.ClientData.Address.BuildingNumber);
			storage.Remove(order.Id);
			Assert.False(storage.OrderExists(order.Id));
			storage.DeleteIfExists();
		}

		private class TestData
		{
			public static IEnumerable<object[]> AddData => new List<object[]>
			{
				new object[]
				{
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					),
				},
				new object[]
				{
					new Order(1, new ClientData(
							"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
						), new ShopData(
							"ShopName2", new Address("City2", "Street2", 2)
						), new GoodsData(
							2, 2
						)
					),
				},
				new object[]
				{
					new Order(1, new ClientData(
							"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
						), new ShopData(
							"ShopName3", new Address("City3", "Street3", 3)
						), new GoodsData(
							3, 3
						)
					),
				}
			};
			
			public static IEnumerable<object[]> RetrieveAllIdsData => new List<object[]>
			{
				new object[]
				{
					new List<Order>
					{
						new Order(1, new ClientData(
								"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
							), new ShopData(
								"ShopName1", new Address("City1", "Street1", 1)
							), new GoodsData(
								1, 1
							)
						),
						new Order(2, new ClientData(
								"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
							), new ShopData(
								"ShopName2", new Address("City2", "Street2", 2)
							), new GoodsData(
								2, 2
							)
						),
						new Order(3, new ClientData(
								"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
							), new ShopData(
								"ShopName3", new Address("City3", "Street3", 3)
							), new GoodsData(
								3, 3
							)
						)
					}, 	
				}
			};
			
			public static IEnumerable<object[]> UpdateData => new List<object[]>
			{
				new object[]
				{
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					),
					new Order(1, new ClientData(
							"FirstName1Changed", "LastName1Changed", "email1Changed@gmail.com", "+3800000001", new Address("City1Changed", "Street1Changed", 1)
						), new ShopData(
							"ShopName1Changed", new Address("City1Changed", "Street1Changed", 2)
						), new GoodsData(
							11, 10
						)
					)
				},
				new object[]
				{
					new Order(1, new ClientData(
							"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
						), new ShopData(
							"ShopName2", new Address("City2", "Street2", 2)
						), new GoodsData(
							2, 2
						)
					),
					new Order(12, new ClientData(
							"FirstName2Changed", "LastName2Changed", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
						), new ShopData(
							"ShopName2Changed", new Address("City2", "Street2Changed", 2)
						), new GoodsData(
							2, 2
						)
					)
				},
				new object[]
				{
					new Order(1, new ClientData(
							"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
						), new ShopData(
							"ShopName3", new Address("City3", "Street3", 3)
						), new GoodsData(
							3, 3
						)
					),
					new Order(197, new ClientData(
							"FirstChangedName3", "LastNaChangedme3", "emailChanged3@gmail.com", "+3800000003", new Address("CiChangedty3", "Street3", 3)
						), new ShopData(
							"ShopChangedName3", new Address("City3", "StreChangedet3", 3)
						), new GoodsData(
							3, 3
						)
					)
				}
			};
			
			public static IEnumerable<object[]> RemoveData => new List<object[]>
			{
				new object[]
				{
					new Order(1, new ClientData(
							"FirstName1", "LastName1", "email1@gmail.com", "+3800000001", new Address("City1", "Street1", 1)
						), new ShopData(
							"ShopName1", new Address("City1", "Street1", 1)
						), new GoodsData(
							1, 1
						)
					),
				},
				new object[]
				{
					new Order(1, new ClientData(
							"FirstName2", "LastName2", "email2@gmail.com", "+3800000002", new Address("City2", "Street2", 2)
						), new ShopData(
							"ShopName2", new Address("City2", "Street2", 2)
						), new GoodsData(
							2, 2
						)
					),
				},
				new object[]
				{
					new Order(1, new ClientData(
							"FirstName3", "LastName3", "email3@gmail.com", "+3800000003", new Address("City3", "Street3", 3)
						), new ShopData(
							"ShopName3", new Address("City3", "Street3", 3)
						), new GoodsData(
							3, 3
						)
					),
				}
			};
		}
	}
}
