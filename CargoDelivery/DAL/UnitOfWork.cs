using System;

using CargoDelivery.Classes;
using CargoDelivery.DAL.Interfaces;

namespace CargoDelivery.DAL
{
	/// <summary>
	/// Is used to manage repositories. 
	/// </summary>
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		/// <summary>
		/// Contains order context instance.
		/// </summary>
		private readonly OrderContext _context;
		
		/// <inheritdoc />
		/// <summary>
		/// Orders field is used to manage table 'Orders' in the database.
		/// </summary>
		public GenericRepository<Order> Orders { get; }
		
		/// <summary>
		/// A variable which shows whether context is disposed or not.
		/// </summary>
		private bool _disposed;

		/// <summary>
		/// Default constructor that instantiates UnitOfWork object. 
		/// </summary>
		public UnitOfWork()
		{
			_context = new OrderContext();
			Orders = new GenericRepository<Order>(_context);
		}
		
		/// <summary>
		/// Saves context changes.
		/// </summary>
		public void Save()
		{
			Orders.Save();
		}

		/// <summary>
		/// Disposes context using disposing parameter.
		/// </summary>
		/// <param name="disposing">Sets disposing state.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}

			_disposed = true;
		}
		
		/// <summary>
		/// Implements Dispose method.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
