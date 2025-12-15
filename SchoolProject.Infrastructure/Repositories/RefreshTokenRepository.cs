using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities.Authentication;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Repositories.Abstractionss;

namespace SchoolProject.Infrastructure.Repositories;

public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
{
   private readonly DbSet<UserRefreshToken> _subjectDbSet;

   public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
   {
      this._subjectDbSet = dbContext.Set<UserRefreshToken>();
   }


}
