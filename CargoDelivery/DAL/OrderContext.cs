using System.Data.Entity;

using CargoDelivery.Classes;
using CargoDelivery.Classes.OrderData;

namespace CargoDelivery.DAL
{
	public class OrderContext : DbContext
	{
		public DbSet<Order> Orders { get; set; }
		public DbSet<ClientData> ClientDatas { get; set; }
		public DbSet<GoodsData> GoodsDatas { get; set; }
		public DbSet<ShopData> ShopDatas { get; set; }
		public DbSet<Address> Addresses { get; set; }

		public OrderContext() : base("CargoDeliveryDb")
		{
			
		}
	}
}
