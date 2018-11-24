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

		/// <summary>
		/// Query 20: show the list of customers’ names who used to order the ‘Tofu’ product.
		/// </summary>
		public const string Task20 =
@"";

		/// <summary>
		/// Query 21: show the list of customers’ names who used to order the ‘Tofu’ product,
		/// along with the total amount of the product they have ordered
		/// and with the total sum for ordered product calculated.
		/// </summary>
		public const string Task21 =
@"";

		/// <summary>
		/// Query 22: show the list of french customers’ names who used to order non-french products (use left join).
		/// </summary>
		public const string Task22 = 
@"";

		/// <summary>
		/// Query 23: show the list of french customers’ names who used to order non-french products.
		/// </summary>
		public const string Task23 = 
@"";

		/// <summary>
		/// Query 24: show the list of french customers’ names who used to order french products.
		/// </summary>
		public const string Task24 = 
@"";

		/// <summary>
		/// Query 25: show the total ordering sum calculated for each country of customer.
		/// </summary>
		public const string Task25 = 
@"";

		/// <summary>
		/// Query 26: show the total ordering sums calculated for each customer’s country
		/// for domestic and non-domestic products separately
		/// (e.g.: “France – French products ordered – Non-french products ordered” and so on for each country).
		/// </summary>
		public const string Task26 = 
@"";

		/// <summary>
		/// Query 27: show the list of product categories along with total ordering sums calculated
		/// for the orders made for the products of each category, during the year 1997.
		/// </summary>
		public const string Task27 =
@"";

		/// <summary>
		/// Query 28: show the list of product names along with unit prices and the history of unit prices
		/// taken from the orders (show ‘Product name – Unit price – Historical price’).
		/// The duplicate records should be eliminated.
		/// If no orders were made for a certain product, then the result for this product should look like
		/// ‘Product name – Unit price – NULL’. Sort the list by the product name.
		/// </summary>
		public const string Task28 = 
@"";
	}
}
