using System;
using System.Linq;
using System.Collections.Generic;

using ConsoleTables;

namespace AdoDotNet.Task
{
	public class Task
	{
		/// <summary>
		/// Database used for the task.
		/// </summary>
		private readonly Db _db;
		
		/// <summary>
		/// Template of the database's title.
		/// </summary>
		private const string TitleTemplate = "|============= {0} =============|";

		/// <summary>
		/// Initializes a new instance of the <see cref="Task"/> class.
		/// </summary>
		public Task()
		{
			_db = new Db("Persist Security Info=False;Integrated Security=true;Initial Catalog=Northwind;server=(local)");
		}

		/// <summary>
		/// Connects to the database.
		/// </summary>
		/// <exception cref="Exception">Throws if connection failed.</exception>
		private void ConnectToDatabase()
		{
			if (!_db.Connect())
			{
				throw new Exception("can't connect to database");
			}
			
			Console.WriteLine(TitleTemplate + "\n\n", "Successfully connected to the database");
		}

		/// <summary>
		/// Disconnects from the database.
		/// </summary>
		/// <exception cref="Exception">Throws if disconnection failed.</exception>
		private void DisconnectFromDatabase()
		{
			if (!_db.Disconnect())
			{
				throw new Exception("can't disconnect from database");
			}
			
			Console.WriteLine("\n" + TitleTemplate, "Successfully disconnected from the database");
		}

		/// <summary>
		/// Function to print table.
		/// </summary>
		/// <param name="title">Table title.</param>
		/// <param name="data">Table to print.</param>
		/// <param name="columnsNames">Columns' names.</param>
		private static void PrintTaskResult(string title, IReadOnlyCollection<List<string>> data, List<string> columnsNames)
		{			
			var table = new ConsoleTable(columnsNames.ToArray());

			foreach (var row in data)
			{
				table.AddRow(row.ToArray<object>());
			}
			
			Console.WriteLine(TitleTemplate + "\n", title);
			table.Write(Format.MarkDown);
			if (data.Count == 0)
			{
				Console.WriteLine(TitleTemplate + "\n", "There is no data to display, result is empty");
			}
		}
		
		/// <summary>
		/// Function to execute all task's queries (19-28, see their descriptions in <see cref="Queries"/>).
		/// </summary>
		public void ExecuteTask()
		{
			try
			{
				ConnectToDatabase();
			}
			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
			}

			var taskNumber = 19;
			foreach (var query in Queries.Data)
			{
				try
				{
					var result = _db.ExecQuery(query, out var columnsNames);
					PrintTaskResult($"Task {taskNumber}", result, columnsNames);
				}
				catch (Exception exc)
				{
					Console.WriteLine($"Task {taskNumber}: {exc.Message}\n");
				}

				taskNumber++;
			}

			try
			{
				DisconnectFromDatabase();
			}
			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
			}
		}

		public void ExampleQuery(string query)
		{
			var result = _db.ExecQuery(query, out var columnsNames);
			PrintTaskResult("Example Query", result, columnsNames);
		}
		
		// Query 22: show the list of french customers’ names
		// who used to order non-french products (use left join).

		/*
			@"SELECT DISTINCT C.ContactName
			FROM Customers C
			LEFT JOIN Orders O ON C.CustomerID = O.CustomerID
			LEFT JOIN OrderDetails OD ON OD.OrderID = O.OrderID
			LEFT JOIN Products P ON P.ProductID = OD.ProductID
			LEFT  JOIN Suppliers S ON P.SupplierID = S.SupplierID" +
			"HAVING C.Country = \"France\" AND S.Country <> \"France\"",
		*/

		// Query 23: show the list of french customers’ names
		// who used to order non-french products.
		/*
		 @"SELECT DISTINCT C.ContactName
			FROM Customers C
			JOIN Orders O ON C.CustomerID = O.CustomerID
			JOIN OrderDetails OD ON OD.OrderID = O.OrderID
			JOIN Products P ON P.ProductID = OD.ProductID
			JOIN Suppliers S ON P.SupplierID = S.SupplierID" +
			"HAVING C.Country = \"France\" AND S.Country <> \"France\""
		,
		*/
		// Query 24: show the list of french customers’ names
		// who used to order french products.
		/*
		 	@"SELECT DISTINCT C.ContactName
			FROM Customers C
			JOIN Orders O ON C.CustomerID = O.CustomerID
			JOIN OrderDetails OD ON OD.OrderID = O.OrderID
			JOIN Products P ON P.ProductID = OD.ProductID
			JOIN Suppliers S ON P.SupplierID = S.SupplierID" +
			"HAVING C.Country = \"France\" AND S.Country = \"France\"" 
		 */
		
		// Query 26: show the total ordering sums calculated for each customer’s country
		// for domestic and non-domestic products separately
		// (e.g.: “France – French products ordered – Non-french products ordered”
		// and so on for each country).
		///Use Union?!
		/*
		  @"SELECT DISTINCT C.Country, SUM(O.UnitPrice) AS CustomersCountry, SUM(ONF.UnitPrice) AS NotCustomersCountry 
			FROM Customers C
			JOIN Orders O ON C.CustomerID = O.CustomerID
			JOIN OrderDetails OD ON OD.OrderID = O.OrderID
			JOIN Products P ON P.ProductID = OD.ProductID
			JOIN Suppliers S ON P.SupplierID = S.SupplierID" +
		   "HAVING C.Country = \"France\" AND S.Country = \"France\"+
		   "JOIN Orders ONF ON C.CustomerID = ONF.CustomerID
			JOIN OrderDetails ODNF ON ODNF.OrderID = ONF.OrderID
			JOIN Products PNF ON PNF.ProductID = ODNF.ProductID
			JOIN Suppliers SNF ON PNF.SupplierID = SNF.SupplierID" +
			"HAVING C.Country <> SNF.Country ",
		*/
		
		// Query 28: show the list of product names along with unit prices and the history of unit prices
		// taken from the orders (show ‘Product name – Unit price – Historical price’).
		// The duplicate records should be eliminated.
		// If no orders were made for a certain product, then the result for this product should look like
		// ‘Product name – Unit price – NULL’. Sort the list by the product name.
		
		/*
		@""
		*/
	}
	

}



