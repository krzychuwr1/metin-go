using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MetinGo.Server.Infrastructure.Database
{
    public class Repository<T> : IRepository<T> where T:class
    {
	    private readonly MetinGoDbContext _metinGoDbContext;

	    public Repository(MetinGoDbContext metinGoDbContext)
	    {
		    _metinGoDbContext = metinGoDbContext;
	    }

	    public virtual IQueryable<T> GetAll()
	    {

		    IQueryable<T> query = _metinGoDbContext.Set<T>();
		    return query;
	    }

	    public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
	    {
		    IQueryable<T> query = _metinGoDbContext.Set<T>().Where(predicate);
		    return query;
	    }

	    public virtual void Add(T entity)
	    {
		    _metinGoDbContext.Set<T>().Add(entity);
	    }

	    public virtual void Delete(T entity)
	    {
		    _metinGoDbContext.Set<T>().Remove(entity);
	    }

	    public virtual void Edit(T entity)
	    {
		    _metinGoDbContext.Entry(entity).State = EntityState.Modified;
	    }

	    public virtual void Save()
	    {
		    _metinGoDbContext.SaveChanges();
	    }
	}
}
