using System;
using System.Linq;
using System.Linq.Expressions;

namespace MetinGo.Server.Infrastructure.Database
{
	public interface IRepository<T> where T : class
	{
		void Add(T entity);
		void Delete(T entity);
		void Edit(T entity);
		IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
		IQueryable<T> GetAll();
		void Save();
	}
}