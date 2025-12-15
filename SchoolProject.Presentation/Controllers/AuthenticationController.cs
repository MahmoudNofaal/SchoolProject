using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.UseCases.Authentication.Commands.Models;
using SchoolProject.Application.UseCases.Authentication.Queries.Models;
using SchoolProject.Presentation.Base;
using SchoolProject.Presentation.MetaData;

namespace SchoolProject.Presentation.Controllers;

//[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : AppControllerBase
{

   [HttpPost(AuthenticationRoutes.SignIn)]
   public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
   {
      var result = await Mediator.Send(command);

      return NewResult(result);
   }

   [HttpPost(AuthenticationRoutes.RefreshToken)]
   public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
   {
      var result = await Mediator.Send(command);

      return NewResult(result);
   }

   [HttpGet(AuthenticationRoutes.ValidateToken)]
   public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery command)
   {
      var result = await Mediator.Send(command);

      return NewResult(result);
   }

}
