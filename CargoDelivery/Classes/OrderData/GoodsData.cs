using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoDelivery.Classes.OrderData
{
	/// <summary>
	/// Class to represent a goods data.
	/// </summary>
	[Table("Goods")]
	public class GoodsData
	{
		/// <summary>
		/// Holds an id of goods data.
		/// </summary>
		[Key]
		public int Id { get; set; }
		
		/// <summary>
		/// Code a code of goods.
		/// </summary>
		[Required]
		public int Code { get; set; }
		
		/// <summary>
		/// Weight of goods.
		/// </summary>
		[Required]
		public double Weight { get; set; }
	}
}
