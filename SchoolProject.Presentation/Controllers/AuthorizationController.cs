using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Features.Authorization.Commands.Models;
using SchoolProject.Presentation.Base;
using SchoolProject.Presentation.MetaData;

namespace SchoolProject.Presentation.Controllers;

//[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : AppControllerBase
{

   [HttpPost(AuthorizationRoutes.CreateRole)]
   public async Task<IActionResult> AddRole([FromBody] AddRoleCommand command)
   {
      var result = await Mediator.Send(command);

      return NewResult(result);
   }


}
