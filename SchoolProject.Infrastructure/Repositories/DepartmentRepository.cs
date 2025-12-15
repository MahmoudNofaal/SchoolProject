using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Repositories.Abstractionss;

namespace SchoolProject.Infrastructure.Repositories;

public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
{
   private readonly DbSet<Department> _departmentDbSet;

   public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
   {
      this._departmentDbSet = dbContext.Set<Department>();
   }


}
