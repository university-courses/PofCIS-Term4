using System;
using System.Data;
using System.Data.SqlClient;

namespace AdoDotNet.Task
{
	public class Task
	{
		private readonly string _connectionString;
		private SqlConnection _connection;

		public Task()
		{
			_connectionString = "Persist Security Info=False;Integrated Security=true;Initial Catalog=Northwind;server=(local)";
		}
		
		public bool ConnectToDatabase()
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

		public bool DisconnectFromDatabase()
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
	}
}
