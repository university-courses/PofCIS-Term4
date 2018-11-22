using System;

namespace AdoDotNet.Task
{
	public class Task
	{
		private readonly Db _db;
		private const string Title = "\n|============= {0} =============|\n";

		public Task()
		{
			_db = new Db("Persist Security Info=False;Integrated Security=true;Initial Catalog=Northwind;server=(local)");
		}

		public void ConnectToDatabase()
		{
			if (!_db.Connect())
			{
				throw new Exception("can't connect to database");
			}
			
			Console.WriteLine(Title, "Successfully connected to the database");
		}

		public void DisconnectFromDatabase()
		{
			if (!_db.Disconnect())
			{
				throw new Exception("can't disconnect from database");
			}
			
			Console.WriteLine(Title, "Successfully disconnected from the database");
		}

		public void ExecuteTask()
		{
			try
			{
				ConnectToDatabase();
				
				// TODO: call all db requests here
				
				DisconnectFromDatabase();
			}
			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
			}
		}
		
		public void TestQuery()
		{
			var result = _db.ExecQuery("SELECT FirstName, LastName, City FROM [Employees]", out var columns);
			foreach (var column in columns)
			{
				Console.Write($"{column}\t\t|");
			}
			Console.WriteLine("\n----------------------------------------");
			
			foreach (var row in result)
			{
				foreach (var col in row)
				{
					Console.Write($"{col}\t\t|");
				}
				Console.WriteLine();
			}
		}
	}
}
