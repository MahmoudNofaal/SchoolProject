using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.UseCases.Students.Commands.Models;
using SchoolProject.Application.UseCases.Students.Queries.Models;
using SchoolProject.Presentation.Base;
using SchoolProject.Presentation.MetaData;

namespace SchoolProject.Presentation.Controllers;

//[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Admin")]
[Authorize]
public class StudentsController : AppControllerBase
{

   [HttpGet(StudentRoutes.GetList)]
   public async Task<IActionResult> GetStudentsList()
   {
      var response = await Mediator.Send(new GetStudentListQuery());

      //return Ok(response);
      return NewResult(response);
   }

   [HttpGet(StudentRoutes.Paginated)]
   [AllowAnonymous]
   public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginatedListQuery query)
   {
      var response = await Mediator.Send(query);

      //return Ok(response);
      return Ok(response);
   }

   [HttpGet(StudentRoutes.GetById)]
   public async Task<IActionResult> GetStudentById([FromRoute] int id)
   {
      var response = await Mediator.Send(new GetStudentByIdQuery(id));

      //return Ok(response);
      return NewResult(response);
   }


   [HttpPost(StudentRoutes.Create)]
   public async Task<IActionResult> CreateStudent([FromBody] AddStudentCommand command)
   {
      var response = await Mediator.Send(command);

      //return Ok(response);
      return NewResult(response);
   }

   [HttpPut(StudentRoutes.Edit)]
   public async Task<IActionResult> EditStudent([FromBody] EditStudentCommand command)
   {
      var response = await Mediator.Send(command);

      //return Ok(response);
      return NewResult(response);
   }

   [HttpDelete(StudentRoutes.Delete)]
   public async Task<IActionResult> DeleteStudent([FromRoute] int id)
   {
      var response = await Mediator.Send(new DeleteStudentCommand(id));

      //return Ok(response);
      return NewResult(response);
   }

}
