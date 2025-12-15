using Microsoft.EntityFrameworkCore;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.Repositories.Abstractionss;

namespace SchoolProject.Application.Services;

public class DepartmentService : IDepartmentService
{
   private readonly IDepartmentRepository _departmentRepository;

   public DepartmentService(IDepartmentRepository departmentRepository)
   {
      this._departmentRepository = departmentRepository;
   }

   

   public async Task<Department> GetByIdAsync(int id)
   {
      return await _departmentRepository.GetByIdAsync(id);
   }

   public async Task<Department> GetByIdWithIncludeAsync(int id)
   {
      var student = await _departmentRepository.GetTableAsNoTracking()
                                               //.Include(x => x.Students)
                                               .Include(x => x.DepartmentSubjects).ThenInclude(y => y.Subject)
                                               .Include(x => x.Instructors)
                                               .Include(x => x.Manager)
                                               .FirstOrDefaultAsync(x => x.Id == id);

      return student;
   }

   public async Task<bool> IsDepartmentIdExistsAsync(int id)
   {
      return await _departmentRepository.GetTableAsNoTracking().AnyAsync(x => x.Id == id);
   }
}

