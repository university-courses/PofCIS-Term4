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
			_db = new Db("Integrated Security=true; Initial Catalog=Northwind; server=YURIYLISOVSKIY");
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
		/// Function to execute all task's queries (18-27, see their descriptions in <see cref="Queries"/>).
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

			var taskNumber = 18;
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
	}
}
