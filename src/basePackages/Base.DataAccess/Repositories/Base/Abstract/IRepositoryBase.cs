using Base.DataAccess.Entity;
using System.Linq.Expressions;

namespace Base.DataAccess.Repositories.Base.Abstract
{
	public interface IRepositoryBase<T> where T : IEntity
	{

		#region Create

		Task<T> AddAsync(T entity);

		#endregion

		#region Read

		T Get(Guid id);
		List<T> GetList(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Expression<Func<T, bool>> predicate = null);

		#endregion

		#region Update

		Task<T> UpdateAsync(T entity);

		#endregion

		#region Delete

		Task DeleteAsync(T entity);

		#endregion

	}
}
