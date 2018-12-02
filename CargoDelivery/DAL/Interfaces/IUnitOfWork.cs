using CargoDelivery.Classes;

namespace CargoDelivery.DAL.Interfaces
{
	public interface IUnitOfWork
	{
		GenericRepository<Order> Orders { get; }
		void Save();
	}
}
