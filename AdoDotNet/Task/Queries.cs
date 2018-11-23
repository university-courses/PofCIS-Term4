namespace AdoDotNet.Task
{
	/// <summary>
	/// Contains task queries.
	/// </summary>
	public static class Queries
	{
		/// <summary>
		/// Query 19: show the list of french customers’ names who have made more than one order
		/// </summary>
		public const string Task19 =
@"SELECT DISTINCT C.ContactName, result.orders_count
FROM Customers C
JOIN (
	SELECT O.CustomerID, Count(*) AS orders_count
	FROM Orders O
	GROUP BY O.CustomerID
) AS result ON result.CustomerID = C.CustomerID AND C.Country = 'France' AND result.orders_count > 1;";
	}
}
