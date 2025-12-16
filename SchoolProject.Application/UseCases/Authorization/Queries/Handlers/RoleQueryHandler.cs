using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Application.UseCases.Authorization.Queries.Models;
using SchoolProject.Application.UseCases.Authorization.Queries.Results;
using SchoolProject.Domain.Entities.Identity;

namespace SchoolProject.Application.UseCases.Authorization.Queries.Handlers;

public class RoleQueryHandler : ResponseHandler,
                                IQueryHandler<GetRolesListQuery, Response<IEnumerable<RolesListResult>>>,
                                IQueryHandler<GetRoleByIdQuery, Response<SingleRoleResult>>,
                                IQueryHandler<GetUserRolesQuery, Response<UserRolesResult>>
{
   private readonly IAuthorizationService _authorizationService;
   private readonly IMapper _mapper;
   private readonly UserManager<ApplicationUser> _userManager;

   public RoleQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                    IAuthorizationService authorizationService,
                                    IMapper mapper,
                                    UserManager<ApplicationUser> userManager) : base(stringLocalizer)
   {
      this._authorizationService = authorizationService;
      this._mapper = mapper;
      this._userManager = userManager;
   }

   #region Get Roles List - Handle
   public async Task<Response<IEnumerable<RolesListResult>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
   {
      var roles = await _authorizationService.GetRolesListAsync();

      var rolesList = _mapper.Map<IEnumerable<RolesListResult>>(roles);

      return Success(rolesList);
   }
   #endregion

   #region Get Role By Id
   public async Task<Response<SingleRoleResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
   {
      var result = await _authorizationService.GetRoleByIdAsync(request.Id);

      if (result is null)
      {
         return BadRequest<SingleRoleResult>();
      }

      var roleResult = _mapper.Map<SingleRoleResult>(result);

      return Success(roleResult);
   }
   #endregion

   #region Get User Role - Handle
   public async Task<Response<UserRolesResult>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
   {
      var user = await _userManager.FindByIdAsync(request.UserId.ToString());

      if (user is null)
      {
         return NotFound<UserRolesResult>();
      }

      var respone =await _authorizationService.GetRolesAndUserRolesAsync(user);

      return Success(respone);
   } 
   #endregion

}
