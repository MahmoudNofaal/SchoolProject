using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Repositories.Abstractionss;

namespace SchoolProject.Infrastructure.Repositories;

public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
{
   private readonly DbSet<Instructor> _instructorDbSet;

   public InstructorRepository(ApplicationDbContext dbContext) : base(dbContext)
   {
      this._instructorDbSet = dbContext.Set<Instructor>();
   }


}
