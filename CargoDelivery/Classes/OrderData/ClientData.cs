using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoDelivery.Classes.OrderData
{
	/// <summary>
	/// Class to represent client data.
	/// </summary>
	[Table("Clients")]
	public class ClientData
	{
		/// <summary>
		/// Holds an id of client data.
		/// </summary>
		[Key]
		public int Id { get; set; }
		
		/// <summary>
		/// The first name of a client.
		/// </summary>
		[Required]
		public string FirstName { get; set; }
		
		/// <summary>
		/// The second name of a client.
		/// </summary>
		[Required]
		public string LastName { get; set; }
		
		/// <summary>
		/// An email of the client.
		/// </summary>
		[Required]
		public string Email { get; set; }
		
		/// <summary>
		/// Client's phone number.
		/// </summary>
		[Required]
		public string PhoneNumber { get; set; }
		
		/// <summary>
		/// Client's address.
		/// </summary>
		public virtual Address Address { get; set; } = new Address();
	}
}
