using System.Data.Entity;

using CargoDelivery.Classes;
using CargoDelivery.Classes.OrderData;

namespace CargoDelivery.DAL
{
	/// <inheritdoc />
	/// <summary>
	/// Represents an order.
	/// </summary>
	public class OrderContext : DbContext
	{	/// <summary>
		/// Holds orders.
		/// </summary>
		public DbSet<Order> Orders { get; set; }
		
		/// <summary>
		/// Contains client personal information.
		/// </summary>
		public DbSet<ClientData> ClientDatas { get; set; }
		
		/// <summary>
		/// Holds an information about ordered goods.
		/// </summary>
		public DbSet<GoodsData> GoodsDatas { get; set; }
		
		/// <summary>
		/// Represents shop data.
		/// </summary>
		public DbSet<ShopData> ShopDatas { get; set; }
		
		/// <summary>
		/// Contains addresses.
		/// </summary>
		public DbSet<Address> Addresses { get; set; }

		/// <inheritdoc />
		/// <summary>
		/// Parameterless OrderContext constructor.
		/// </summary>
		public OrderContext() : base("CargoDeliveryDb")
		{
		}
	}
}
