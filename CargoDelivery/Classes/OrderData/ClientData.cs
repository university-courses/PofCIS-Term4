namespace CargoDelivery.Classes.OrderData
{
	/// <summary>
	/// Class to represent client data.
	/// </summary>
	public class ClientData
	{
		/// <summary>
		/// The first name of a client.
		/// </summary>
		public string FirstName { get; set; }
		
		/// <summary>
		/// The second name of a client.
		/// </summary>
		public string LastName { get; set; }
		
		/// <summary>
		/// An email of the client.
		/// </summary>
		public string Email { get; set; }
		
		/// <summary>
		/// Client's phone number.
		/// </summary>
		public string PhoneNumber { get; set; }
		
		/// <summary>
		/// Client's address.
		/// </summary>
		public Address Address { get; set; }
	}
}
