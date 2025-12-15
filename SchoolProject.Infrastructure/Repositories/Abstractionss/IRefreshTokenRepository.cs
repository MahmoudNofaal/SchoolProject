using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.Authentication;

namespace SchoolProject.Infrastructure.Repositories.Abstractionss;

public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
{


}