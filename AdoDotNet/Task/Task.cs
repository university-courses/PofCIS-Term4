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
				
				Task19();
				Task20();
				Task21();
				Task22();
				Task23();
				Task24();
				Task25();
				Task26();
				Task27();
				Task28();
				
				DisconnectFromDatabase();
			}
			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
			}
		}
		
		/// <summary>
		/// Function to execute task's query #19
		/// </summary>
		public void Task19()
		{
			var result = _db.ExecQuery(Queries.Task19, out var columnsNames);
			PrintTaskResult("Task 19", result, columnsNames);
		}
		
		/// <summary>
		/// Function to execute task's query #20
		/// </summary>
		public void Task20()
		{
			var result = _db.ExecQuery(Queries.Task20, out var columnsNames);
			PrintTaskResult("Task 20", result, columnsNames);
		}
		
		/// <summary>
		/// Function to execute task's query #21
		/// </summary>
		public void Task21()
		{
			var result = _db.ExecQuery(Queries.Task21, out var columnsNames);
			PrintTaskResult("Task 21", result, columnsNames);
		}
		
		/// <summary>
		/// Function to execute task's query #22
		/// </summary>
		public void Task22()
		{
			var result = _db.ExecQuery(Queries.Task22, out var columnsNames);
			PrintTaskResult("Task 22", result, columnsNames);
		}
		
		/// <summary>
		/// Function to execute task's query #23
		/// </summary>
		public void Task23()
		{
			var result = _db.ExecQuery(Queries.Task23, out var columnsNames);
			PrintTaskResult("Task 23", result, columnsNames);
		}
		
		/// <summary>
		/// Function to execute task's query #24
		/// </summary>
		public void Task24()
		{
			var result = _db.ExecQuery(Queries.Task24, out var columnsNames);
			PrintTaskResult("Task 24", result, columnsNames);
		}
		
		/// <summary>
		/// Function to execute task's query #25
		/// </summary>
		public void Task25()
		{
			var result = _db.ExecQuery(Queries.Task25, out var columnsNames);
			PrintTaskResult("Task 25", result, columnsNames);
		}
		
		/// <summary>
		/// Function to execute task's query #26
		/// </summary>
		public void Task26()
		{
			var result = _db.ExecQuery(Queries.Task26, out var columnsNames);
			PrintTaskResult("Task 26", result, columnsNames);
		}
		
		/// <summary>
		/// Function to execute task's query #27
		/// </summary>
		public void Task27()
		{
			var result = _db.ExecQuery(Queries.Task27, out var columnsNames);
			PrintTaskResult("Task 27", result, columnsNames);
		}
		
		/// <summary>
		/// Function to execute task's query #28
		/// </summary>
		public void Task28()
		{
			var result = _db.ExecQuery(Queries.Task28, out var columnsNames);
			PrintTaskResult("Task 28", result, columnsNames);
		}
	}
}
