using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Features.ApplicationUsers.Commands.Models;
using SchoolProject.Application.Features.ApplicationUsers.Queries.Models;
using SchoolProject.Presentation.Base;
using SchoolProject.Presentation.MetaData;

namespace SchoolProject.Presentation.Controllers;

//[Route("api/[controller]")]
[ApiController]
public class ApplicationUsersController : AppControllerBase
{

   [HttpPost(ApplicationUserRoutes.Create)]
   public async Task<IActionResult> Create([FromBody] AddApplicationUserCommand command)
   {
      var response = await Mediator.Send(command);

      //return Ok(response);
      return NewResult(response);
   }

   [HttpGet(ApplicationUserRoutes.Paginated)]
   public async Task<IActionResult> Paginated([FromQuery] GetUserListQuery query)
   {
      var response = await Mediator.Send(query);

      //return Ok(response);
      return Ok(response);
   }

   [HttpGet(ApplicationUserRoutes.GetById)]
   public async Task<IActionResult> GetUserById([FromRoute] int id)
   {
      var response = await Mediator.Send(new GetUserByIdQuery(id));

      //return Ok(response);
      return NewResult(response);
   }

   [HttpPut(ApplicationUserRoutes.Edit)]
   public async Task<IActionResult> UpdateUser([FromBody] UpdateApplicationUserCommand command)
   {
      var response = await Mediator.Send(command);

      //return Ok(response);
      return NewResult(response);
   }

   [HttpDelete(ApplicationUserRoutes.Delete)]
   public async Task<IActionResult> DeleteUser([FromRoute] int id)
   {
      var response = await Mediator.Send(new DeleteApplicationUserCommand(id));

      //return Ok(response);
      return NewResult(response);
   }

   [HttpPatch(ApplicationUserRoutes.ChangePassword)]
   public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand command)
   {
      var response = await Mediator.Send(command);

      //return Ok(response);
      return NewResult(response);
   }


}
