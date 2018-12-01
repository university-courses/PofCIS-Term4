namespace CargoDelivery.Classes.OrderData
{
	/// <summary>
	/// Class to represent an address.
	/// </summary>
	public class Address
	{
		/// <summary>
		/// A name of the city.
		/// </summary>
		public string City { get; set; }
		
		/// <summary>
		/// A name of the street.
		/// </summary>
		public string Street { get; set; }
		
		/// <summary>
		/// A number of building.
		/// </summary>
		public uint BuildingNumber { get; set; }
	}
}
