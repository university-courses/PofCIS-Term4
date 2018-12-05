using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoDelivery.Classes.OrderData
{
	/// <summary>
	/// Class to represent a shop data.
	/// </summary>
	[Table("Shops")]
	public class ShopData
	{
		/// <summary>
		/// Holds an id of shop data.
		/// </summary>
		[Key]
		public int Id { get; set; }
		
		/// <summary>
		/// A name of the shop.
		/// </summary>
		[Required, MaxLength(256)]
		public string Name { get; set; }
		
		/// <summary>
		/// An address of the shop.
		/// </summary>
		public virtual Address Address { get; set; } = new Address();
	}
}
