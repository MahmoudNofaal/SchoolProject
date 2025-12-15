using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Repositories.Abstractionss;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Infrastructure.Repositories;

public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
{
   private readonly DbSet<Student> _studentsDbSet;

   public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
   {
      this._studentsDbSet = dbContext.Set<Student>();
   }

   public async Task<List<Student>> GetStudentsListAsync()
   {
      return await _studentsDbSet.Include(x => x.Department).ToListAsync();
   }

}
