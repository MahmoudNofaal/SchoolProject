using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace SchoolProject.Infrastructure.Repositories.Abstractionss;

public interface IGenericRepositoryAsync<T> where T : class
{
   Task<T> AddAsync(T entity);
   Task AddRangeAsync(ICollection<T> entities);

   Task<T> GetByIdAsync(int id);
   IQueryable<T> GetTableAsNoTracking();
   IQueryable<T> GetTableAsTracking();

   Task UpdateAsync(T entity);
   Task UpdateRangeAsync(ICollection<T> entities);

   Task DeleteAsync(T entity);
   Task DeleteRangeAsync(ICollection<T> entities);

   IDbContextTransaction BeginTransaction();
   void Commit();
   void RollBack();

   Task SaveChangesAsync();

}

