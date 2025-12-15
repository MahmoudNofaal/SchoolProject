using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Services.Abstractions;

public interface IStudentService
{

   Task<List<Student>> GetStudentsListAsync();
   Task<Student> GetByIdWithIncludeAsync(int id);
   Task<Student> GetByIdAsync(int id);
   Task<bool> AddAsync(Student student);
   Task<bool> EditAsync(Student student);
   Task<bool> DeleteAsync(Student student);
   Task<bool> IsNameExistsAsync(string name);
   Task<bool> IsNameExistsExcludeSelfAsync(string name, int id);

   IQueryable<Student> GetStudentsAsQueryable();
   IQueryable<Student> GetStudentsByDepartmentIdAsQueryable(int departmentId);

   IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderBy, string search);

}
