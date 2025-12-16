using Microsoft.EntityFrameworkCore;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Enums;
using SchoolProject.Infrastructure.Repositories.Abstractionss;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Services;

public class StudentService : IStudentService
{
   private readonly IStudentRepository _studentRepository;

   public StudentService(IStudentRepository studentRepository)
   {
      this._studentRepository = studentRepository;
   }

   public async Task<bool> AddAsync(Student student)
   {
      // add student
      await _studentRepository.AddAsync(student);

      return true;
   }

   public async Task<bool> DeleteAsync(Student student)
   {
      var transactions = _studentRepository.BeginTransaction();

      try
      {
         // delete student
         await _studentRepository.DeleteAsync(student);
         await transactions.CommitAsync();
         return true;
      }
      catch
      {
         await transactions.RollbackAsync();
         return false;
      }
   }

   public async Task<bool> EditAsync(Student student)
   {
      // add student
      await _studentRepository.UpdateAsync(student);

      return true;
   }

   public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderBy, string search)
   {
     var querable = _studentRepository.GetTableAsNoTracking()
                                      .Include(x => x.Department)
                                      .AsQueryable();

      querable = querable.Where(x => string.IsNullOrEmpty(search) ||
                                     x.Name_En.Contains(search) ||
                                     x.Address.Contains(search) ||
                                     x.Phone.Contains(search) ||
                                     (x.Department != null && x.Department.Name_En.Contains(search)));

      switch (orderBy)
      {
         case StudentOrderingEnum.StudId:
            querable = querable.OrderBy(x => x.Id);
            break;
         case StudentOrderingEnum.Name:
            querable = querable.OrderBy(x => x.Name_En);
            break;
         case StudentOrderingEnum.Address:
            querable = querable.OrderBy(x => x.Address);
            break;
         case StudentOrderingEnum.DepartmentName:
            querable = querable.OrderBy(x => x.Department.Name_En);
            break;
      }

      return querable;
   }

   public async Task<Student> GetByIdAsync(int id)
   {
      return await _studentRepository.GetByIdAsync(id);
   }

   public async Task<Student> GetByIdWithIncludeAsync(int id)
   {
      var student = _studentRepository.GetTableAsNoTracking()
                                      .Include(x => x.Department)
                                      .FirstOrDefault(x => x.Id == id);

      return student;
   }

   public IQueryable<Student> GetStudentsAsQueryable()
   {
      return _studentRepository.GetTableAsNoTracking()
                               .Include(x => x.Department)
                               .AsQueryable();
   }

   public IQueryable<Student> GetStudentsByDepartmentIdAsQueryable(int departmentId)
   {
      return _studentRepository.GetTableAsNoTracking()
                               .Where(x => x.DepartmentId == departmentId)
                               .AsQueryable();
   }

   public async Task<List<Student>> GetStudentsListAsync()
   {
      return await _studentRepository.GetStudentsListAsync();
   }

   public async Task<bool> IsNameExistsAsync(string name)
   {
      return await _studentRepository.GetTableAsNoTracking()
                                     .AnyAsync(x => x.Name_En == name);
   }

   public async Task<bool> IsNameExistsExcludeSelfAsync(string name, int id)
   {
      return await _studentRepository.GetTableAsNoTracking()
                                     .AnyAsync(x => x.Name_En == name && x.Id != id);
   }
}

