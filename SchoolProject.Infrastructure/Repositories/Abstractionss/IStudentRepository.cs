using SchoolProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Infrastructure.Repositories.Abstractionss;

public interface IStudentRepository : IGenericRepositoryAsync<Student>
{

   Task<List<Student>> GetStudentsListAsync();
 
}
