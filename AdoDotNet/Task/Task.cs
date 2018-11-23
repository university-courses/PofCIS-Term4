using System;
using System.Linq;
using System.Collections.Generic;

using ConsoleTables;

namespace AdoDotNet.Task
{
	public class Task
	{
		private readonly Db _db;
		private const string TitleTemplate = "|============= {0} =============|";

		public Task()
		{
			_db = new Db("Persist Security Info=False;Integrated Security=true;Initial Catalog=Northwind;server=(local)");
		}

		private void ConnectToDatabase()
		{
			if (!_db.Connect())
			{
				throw new Exception("can't connect to database");
			}
			
			Console.WriteLine(TitleTemplate + "\n\n", "Successfully connected to the database");
		}

		private void DisconnectFromDatabase()
		{
			if (!_db.Disconnect())
			{
				throw new Exception("can't disconnect from database");
			}
			
			Console.WriteLine("\n" + TitleTemplate, "Successfully disconnected from the database");
		}

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
		
		public void ExecuteTask()
		{
			try
			{
				ConnectToDatabase();
				
				Task19();
				
				// TODO: call all db requests here
				
				DisconnectFromDatabase();
			}
			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
			}
		}
		
		public void Task19()
		{
			var result = _db.ExecQuery(Queries.Task19, out var columnsNames);
			PrintTaskResult("Task 19", result, columnsNames);
		}
	}
}
