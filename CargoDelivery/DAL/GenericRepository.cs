using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;

using CargoDelivery.DAL.Interfaces;

namespace CargoDelivery.DAL
{
	/// <summary>
	/// Used for managing the database with databse context.
	/// </summary>
	/// <typeparam name="TEntity">Template entity.</typeparam>
	public sealed class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		/// <summary>
		/// Holds Ddatabase content.
		/// </summary>
		internal readonly DbContext Context;
		
		/// <summary>
		/// Holds specific database entity object.
		/// </summary>
		internal readonly DbSet<TEntity> DbSet;

		/// <summary>
		/// Creates GenericRepository object.
		/// </summary>
		/// <param name="context">Database context to manage db table.</param>
		public GenericRepository(DbContext context)
		{
			Context = context;
			DbSet = context.Set<TEntity>();
		}

		/// <summary>
		/// Search data in database using specific parameters.
		/// </summary>
		/// <param name="filter">Search filter</param>
		/// <param name="orderBy">Query to order search data</param>
		/// <param name="includeProperties">Additional search properties.</param>
		/// <returns>A list of data of specific database entity.</returns>
		public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
		{
			IQueryable<TEntity> query = DbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}

			query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
				.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
			return orderBy?.Invoke(query).ToList() ?? query.ToList();
		}

		/// <summary>
		/// Search data in database using id of the object.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Returns data of specific database entity by its id.</returns>
		public TEntity GetById(object id)
		{
			return DbSet.Find(id);
		}

		/// <summary>
		/// Inserts new data to a table.
		/// </summary>
		/// <param name="entity">Specific entity object.</param>
		public void Insert(TEntity entity)
		{
			DbSet.Add(entity);
		}

		/// <summary>
		/// Removes data from the database by id.
		/// </summary>
		/// <param name="id"></param>
		public void Delete(object id)
		{
			var entityToDelete = DbSet.Find(id);
			Delete(entityToDelete);
		}

		/// <summary>
		/// Removes data from the database by entity object.
		/// </summary>
		/// <param name="entityToDelete">Specific entity.</param>
		public void Delete(TEntity entityToDelete)
		{
			if (Context.Entry(entityToDelete).State == EntityState.Detached)
			{
				DbSet.Attach(entityToDelete);
			}

			DbSet.Remove(entityToDelete);
		}

		/// <summary>
		/// Updates entity in database by given entity object.
		/// </summary>
		/// <param name="entityToUpdate">Specific entity.</param>
		public void Update(TEntity entityToUpdate)
		{
			DbSet.Attach(entityToUpdate);
			Context.Entry(entityToUpdate).State = EntityState.Modified;
		}

		/// <summary>
		/// Saves context changes.
		/// </summary>
		public void Save()
		{
			Context.SaveChanges();
		}
	}
}
