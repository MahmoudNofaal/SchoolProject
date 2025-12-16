using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.UseCases.Authorization.Commands.Models;
using SchoolProject.Application.UseCases.Authorization.Queries.Models;
using SchoolProject.Presentation.Base;
using SchoolProject.Presentation.MetaData;

namespace SchoolProject.Presentation.Controllers;

//[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Admin,User")]
[Authorize(Roles = "Admin")]
public class AuthorizationController : AppControllerBase
{

   #region AddRole
   [HttpPost(AuthorizationRoutes.CreateRole)]
   public async Task<IActionResult> AddRole([FromBody] AddRoleCommand command)
   {
      var result = await Mediator.Send(command);

      return NewResult(result);
   }
   #endregion

   #region EditRole
   [HttpPut(AuthorizationRoutes.EditRole)]
   public async Task<IActionResult> EditRole([FromBody] EditRoleCommand command)
   {
      var result = await Mediator.Send(command);

      return NewResult(result);
   }
   #endregion

   #region DeleteRole
   [HttpDelete(AuthorizationRoutes.DeleteRole)]
   public async Task<IActionResult> DeleteRole([FromRoute] int id)
   {
      var result = await Mediator.Send(new DeleteRoleCommand(id));

      return NewResult(result);
   }
   #endregion

   #region RoleList
   [HttpGet(AuthorizationRoutes.RoleList)]
   [AllowAnonymous]
   public async Task<IActionResult> RoleList()
   {
      var result = await Mediator.Send(new GetRolesListQuery());

      return NewResult(result);
   }
   #endregion

   #region SingleRole
   [HttpGet(AuthorizationRoutes.SingleRole)]
   [AllowAnonymous]
   public async Task<IActionResult> SingleRole([FromRoute] int id)
   {
      var result = await Mediator.Send(new GetRoleByIdQuery(id));

      return NewResult(result);
   }
   #endregion

   #region GetUserRoles
   [HttpGet(AuthorizationRoutes.GetUserRoles)]
   [AllowAnonymous]
   public async Task<IActionResult> GetUserRoles([FromRoute] int id)
   {
      var result = await Mediator.Send(new GetUserRolesQuery(id));

      return NewResult(result);
   }
   #endregion

   #region UpdateUserRoles
   [HttpPut(AuthorizationRoutes.UpdateUserRoles)]
   [AllowAnonymous]
   public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
   {
      var result = await Mediator.Send(command);

      return NewResult(result);
   }
   #endregion

   #region GetUserClaims
   [HttpGet(AuthorizationRoutes.GetUserClaims)]
   [AllowAnonymous]
   public async Task<IActionResult> GetUserClaims([FromRoute] int id)
   {
      var result = await Mediator.Send(new GetUserClaimsQuery(id));

      return NewResult(result);
   }
   #endregion

   #region UpdateUserClaims
   [HttpPut(AuthorizationRoutes.UpdateUserClaims)]
   [AllowAnonymous]
   public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
   {
      var result = await Mediator.Send(command);

      return NewResult(result);
   }
   #endregion


}
