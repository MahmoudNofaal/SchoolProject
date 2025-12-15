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

   [HttpPost(AuthorizationRoutes.CreateRole)]
   public async Task<IActionResult> AddRole([FromBody] AddRoleCommand command)
   {
      var result = await Mediator.Send(command);

      return NewResult(result);
   }

   [HttpPut(AuthorizationRoutes.EditRole)]
   public async Task<IActionResult> EditRole([FromBody] EditRoleCommand command)
   {
      var result = await Mediator.Send(command);

      return NewResult(result);
   }

   [HttpDelete(AuthorizationRoutes.DeleteRole)]
   public async Task<IActionResult> DeleteRole([FromRoute] int id)
   {
      var result = await Mediator.Send(new DeleteRoleCommand(id));

      return NewResult(result);
   }

   [HttpGet(AuthorizationRoutes.RoleList)]
   [AllowAnonymous]
   public async Task<IActionResult> RoleList()
   {
      var result = await Mediator.Send(new GetRolesListQuery());

      return NewResult(result);
   }

   [HttpGet(AuthorizationRoutes.SingleRole)]
   [AllowAnonymous]
   public async Task<IActionResult> SingleRole([FromRoute] int id)
   {
      var result = await Mediator.Send(new GetRoleByIdQuery(id));

      return NewResult(result);
   }


}
