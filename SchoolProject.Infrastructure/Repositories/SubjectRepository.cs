using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Results;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Repositories.Abstractionss;

namespace SchoolProject.Infrastructure.Repositories;

public class SubjectRepository : GenericRepositoryAsync<Subject>, ISubjectRepository
{
   private readonly DbSet<Subject> _subjectDbSet;

   public SubjectRepository(ApplicationDbContext dbContext) : base(dbContext)
   {
      this._subjectDbSet = dbContext.Set<Subject>();
   }


}
