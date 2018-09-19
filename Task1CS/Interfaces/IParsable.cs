namespace Task1CS.Interfaces
{
	/// <summary>
	/// Interface to allow to parse strings of data
	/// </summary>
	public interface IParsable
	{
		/// <summary>
		/// Function parses strings of data. 
		/// </summary>
		/// <param name="data">String that has to be parsed.</param>
		/// <returns>True if string was parsed, false if string was not parsed.</returns>
		bool Parse(string data);
	}
}
