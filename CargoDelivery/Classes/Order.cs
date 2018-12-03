using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CargoDelivery.Classes.OrderData;

namespace CargoDelivery.Classes
{
	/// <summary>
	/// Represents an order.
	/// </summary>
	[Table("Orders")]
	public class Order
	{
		/// <summary>
		/// Holds an id of the order.
		/// </summary>
		[Key]
		public int Id { get; set; }
		
		/// <summary>
		/// Contains client personal information.
		/// </summary>
		public virtual ClientData ClientData { get; set; } = new ClientData();

		/// <summary>
		/// Represents shop data.
		/// </summary>
		public virtual ShopData ShopData { get; set; } = new ShopData();

		/// <summary>
		/// Holds an information about ordered goods.
		/// </summary>
		public virtual GoodsData GoodsData { get; set; } = new GoodsData();
	}
}
