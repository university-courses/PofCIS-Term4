using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AdoDotNet
{
	public class Db
	{
		private readonly string _connectionString;
		private SqlConnection _connection;

		public Db(string connectionString)
		{
			_connectionString = connectionString;
		}
		
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

		private static List<string> GetColumnsList(IDataRecord reader)
		{
			var result = new List<string>();
			for (var i = 0; i < reader.FieldCount; i++)
			{
				result.Add(reader.GetName(i));
			}

			return result;
		}

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
