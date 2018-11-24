using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AdoDotNet
{
	/// <summary>
	/// Class to represent database.
	/// </summary>
	public class Db
	{
		/// <summary>
		/// Connection string for database.
		/// </summary>
		private readonly string _connectionString;
		
		/// <summary>
		/// Variable to represent connection to database.
		/// </summary>
		private SqlConnection _connection;

		/// <summary>
		/// Creates new instance of <see cref="Db"/> class.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		public Db(string connectionString)
		{
			_connectionString = connectionString;
		}
		
		/// <summary>
		/// Connects to the database.
		/// </summary>
		/// <returns>
		/// True if connection was successfull.
		/// False if connection failed. 
		/// </returns>
		public bool Connect()
		{
			try
			{
				_connection = new SqlConnection(_connectionString);
				_connection.Open();
				if (_connection.State == ConnectionState.Open)
				{
					return true;
				}
			}
			catch (SqlException exc)
			{
				Console.WriteLine(exc.Message);
			}
			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
			}

			return false;
		}

		/// <summary>
		/// Disconnects from the database.
		/// </summary>
		/// <returns>
		/// True if disconnection was successfull.
		/// False if disconnection failed.
		/// </returns>
		public bool Disconnect()
		{
			try
			{
				if (_connection.State == ConnectionState.Open)
				{
					_connection.Close();
				}

				return _connection.State == ConnectionState.Closed;
			}
			catch (SqlException exc)
			{
				Console.WriteLine(exc.Message);
			}
			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
			}

			return false;
		}

		/// <summary>
		/// Function to get list of column names.
		/// </summary>
		/// <param name="reader">Reader to access data in columns.</param>
		/// <returns>List of strings.</returns>
		private static List<string> GetColumnsList(IDataRecord reader)
		{
			var result = new List<string>();
			for (var i = 0; i < reader.FieldCount; i++)
			{
				result.Add(reader.GetName(i));
			}

			return result;
		}

		/// <summary>
		/// Function to get data from the row.
		/// </summary>
		/// <param name="reader">Reader to access data in columns.</param>
		/// <returns>List of strings.</returns>
		private static List<string> RetrieveData(IDataRecord reader)
		{
			var row = new List<string>();
			var dataToDisplay = new object[reader.FieldCount];
			reader.GetValues(dataToDisplay);
			for (var i = 0; i < reader.FieldCount; i++)
			{
				row.Add(dataToDisplay[i].ToString());
			}

			return row;
		}

		/// <summary>
		/// Function for executing a query.
		/// </summary>
		/// <param name="query">Query that has to be executed.</param>
		/// <param name="columnsList">Over what columns to execute query.</param>
		/// <returns>Modified data.</returns>
		/// <exception cref="Exception">Throws if not connected to database.</exception>
		public List<List<string>> ExecQuery(string query, out List<string> columnsList)
		{
			if (_connection.State != ConnectionState.Open)
			{
				throw new Exception("connect to database first");
			}
			
			var command = new SqlCommand(query, _connection);
			var data = new List<List<string>>();
			using (var reader = command.ExecuteReader())
			{
				columnsList = GetColumnsList(reader);
				if (!reader.Read())
				{
					return data;
				}
				
				do
				{
					data.Add(RetrieveData(reader));
				}
				while (reader.Read());
			}

			return data;
		}
	}
}
