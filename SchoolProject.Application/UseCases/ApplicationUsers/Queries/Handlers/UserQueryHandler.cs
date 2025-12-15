using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Resources;
using SchoolProject.Application.UseCases.ApplicationUsers.Queries.Models;
using SchoolProject.Application.UseCases.ApplicationUsers.Queries.Results;
using SchoolProject.Application.Wrappers;
using SchoolProject.Domain.Entities.Identity;

namespace SchoolProject.Application.UseCases.ApplicationUsers.Queries.Handlers;

public class UserQueryHandler : ResponseHandler,
                                IQueryHandler<GetUserListQuery, PaginatedResult<UserListResult>>,
                                IQueryHandler<GetUserByIdQuery, Response<SingleUserResult>>
{
   private readonly IStringLocalizer<SharedResources> _stringLocalizer;
   private readonly IMapper _mapper;
   private readonly UserManager<ApplicationUser> _userManager;

   public UserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                           IMapper mapper,
                           UserManager<ApplicationUser> userManager) : base(stringLocalizer)
   {
      this._stringLocalizer = stringLocalizer;
      this._mapper = mapper;
      this._userManager = userManager;
   }

   #region Get Paginated Users - Handler
   public async Task<PaginatedResult<UserListResult>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
   {
      var users = _userManager.Users.AsQueryable();

      var paginatedResult = await _mapper.ProjectTo<UserListResult>(users)
                                         .ToPaginatedListAsync(request.PageNumber, request.PageSize);

      paginatedResult.Meta = new
      {
         Count = paginatedResult.Data.Count(),
      };

      return paginatedResult;
   }
   #endregion

   #region Get Single User - Handler
   public async Task<Response<SingleUserResult>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
   {
      var user = await _userManager.FindByIdAsync(request.Id.ToString());

      if (user == null)
      {
         return NotFound<SingleUserResult>(_stringLocalizer[SharedResourcesKeys.NotFound]);
      }

      var result = _mapper.Map<SingleUserResult>(user);

      return Success(result);
   } 
   #endregion

}
