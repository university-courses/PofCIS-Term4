using CargoDelivery.Classes.OrderData;

namespace CargoDelivery.Classes
{
	/// <summary>
	/// Represents an order.
	/// </summary>
	public class Order
	{
		/// <summary>
		/// Holds an id of the order.
		/// </summary>
		public long Id { get; set; }
		
		/// <summary>
		/// Contains client personal information.
		/// </summary>
		public ClientData ClientData { get; set; }
		
		/// <summary>
		/// Represents shop data.
		/// </summary>
		public ShopData ShopData { get; set; }
		
		/// <summary>
		/// Holds an information about ordered goods.
		/// </summary>
		public GoodsData GoodsData { get; set; }
	}
}
