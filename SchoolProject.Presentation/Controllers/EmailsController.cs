using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.UseCases.Emails.Commands.Models;
using SchoolProject.Presentation.Base;
using SchoolProject.Presentation.MetaData;

namespace SchoolProject.Presentation.Controllers;

//[Route("api/[controller]")]
[ApiController]
public class EmailsController : AppControllerBase
{

   
   #region SendEmail
   [HttpPost(EmailRoutes.SendEmail)]
   public async Task<IActionResult> SendEmail([FromBody] SendEmailCommand command)
   {
      var response = await Mediator.Send(command);

      //return Ok(response);
      return NewResult(response);
   }
   #endregion

   
}
