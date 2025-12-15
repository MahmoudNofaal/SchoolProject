
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Services.Abstractions;

public interface IDepartmentService
{

   Task<Department> GetByIdWithIncludeAsync(int id);
   Task<Department> GetByIdAsync(int id);
   Task<bool> IsDepartmentIdExistsAsync(int id);

}
