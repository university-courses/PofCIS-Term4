using System.Collections.Generic;

namespace AdoDotNet.Task
{
	/// <summary>
	/// Contains task queries.
	/// </summary>
	public static class Queries
	{
		public static readonly List<string> Data = new List<string>
		{
//Query 18: show the list of french customers’ names who have made more than one order
//(use grouping).
@"SELECT DISTINCT C.ContactName
FROM Customers C
JOIN (
	SELECT O.CustomerID, Count(*) AS orders_count
	FROM Orders O
	GROUP BY O.CustomerID
) AS result ON result.CustomerID = C.CustomerID AND C.Country = 'France' AND result.orders_count > 1;",

// Query 19: show the list of french customers’ names who have made more than one order
@"SELECT DISTINCT C.ContactName
FROM Customers C
JOIN (
	SELECT O.CustomerID, Count(*) AS orders_count
	FROM Orders O
	GROUP BY O.CustomerID
) AS result ON result.CustomerID = C.CustomerID AND C.Country = 'France' AND result.orders_count > 1;",

// Query 20: show the list of customers’ names who used to order the ‘Tofu’ product.		
@"SELECT C.ContactName
FROM Customers C
JOIN Orders O ON O.CustomerID = C.CustomerID " +
"JOIN \"Order Details Extended\" Ode ON Ode.OrderID = O.OrderID " +
@"WHERE Ode.ProductName = 'Tofu';",

// Query 21: show the list of customers’ names who used to order the ‘Tofu’ product,
// along with the total amount of the product they have ordered
// and with the total sum for ordered product calculated.			
@"SELECT C.ContactName, Ode.Quantity, Ode.ExtendedPrice AS TotalPrice
FROM Customers C
JOIN Orders O ON O.CustomerID = C.CustomerID " +
"JOIN \"Order Details Extended\" Ode ON Ode.OrderID = O.OrderID " +
@"WHERE Ode.ProductName = 'Tofu';",

// Query 22: show the list of french customers’ names who used to order non-french products (use left join).
@"SELECT DISTINCT C.ContactName
FROM Customers C
LEFT JOIN Orders O ON C.CustomerID = O.CustomerID
LEFT JOIN OrderDetails OD ON OD.OrderID = O.OrderID
LEFT JOIN Products P ON P.ProductID = OD.ProductID
LEFT  JOIN Suppliers S ON P.SupplierID = S.SupplierID" +
"HAVING C.Country = \"France\" AND S.Country <> \"France\"",

// Query 23: show the list of french customers’ names who used to order non-french products.
@"SELECT DISTINCT C.ContactName
FROM Customers C
JOIN Orders O ON C.CustomerID = O.CustomerID
JOIN OrderDetails OD ON OD.OrderID = O.OrderID
JOIN Products P ON P.ProductID = OD.ProductID
JOIN Suppliers S ON P.SupplierID = S.SupplierID" +
"HAVING C.Country = \"France\" AND S.Country <> \"France\"",

// Query 24: show the list of french customers’ names who used to order french products.
@"SELECT DISTINCT C.ContactName
FROM Customers C
JOIN Orders O ON C.CustomerID = O.CustomerID
JOIN OrderDetails OD ON OD.OrderID = O.OrderID
JOIN Products P ON P.ProductID = OD.ProductID
JOIN Suppliers S ON P.SupplierID = S.SupplierID" +
"HAVING C.Country = \"France\" AND S.Country = \"France\"",

// Query 25: show the total ordering sum calculated for each country of customer.
@"SELECT O.ShipCountry, Sum(Ode.ExtendedPrice) AS TotalPriceSum
FROM Orders O " +
"JOIN \"Order Details Extended\" Ode ON Ode.OrderID = O.OrderID " +
"GROUP BY O.ShipCountry;",

// Query 26: show the total ordering sums calculated for each customer’s country
// for domestic and non-domestic products separately
// (e.g.: “France – French products ordered – Non-french products ordered” and so on for each country).
@"SELECT DISTINCT C.Country, SUM(O.UnitPrice) AS CustomersCountry, SUM(ONF.UnitPrice) AS NotCustomersCountry 
FROM Customers C
JOIN Orders O ON C.CustomerID = O.CustomerID
JOIN OrderDetails OD ON OD.OrderID = O.OrderID
JOIN Products P ON P.ProductID = OD.ProductID
JOIN Suppliers S ON P.SupplierID = S.SupplierID" +
"HAVING C.Country = \"France\" AND S.Country = \"France\""+
@"JOIN Orders ONF ON C.CustomerID = ONF.CustomerID 
JOIN OrderDetails ODNF ON ODNF.OrderID = ONF.OrderID
JOIN Products PNF ON PNF.ProductID = ODNF.ProductID
JOIN Suppliers SNF ON PNF.SupplierID = SNF.SupplierID" +
"HAVING C.Country <> SNF.Country ",

// Query 27: show the list of product categories along with total ordering sums calculated
// for the orders made for the products of each category, during the year 1997.
@"SELECT Psf.CategoryName, Sum(Psf.ProductSales) AS TotalOrderingSum " +
"FROM \"Product Sales For 1997\" Psf " +
"GROUP BY Psf.CategoryName;",
		};
	}
}
