using Microsoft.AspNetCore.Mvc;
using SchoolProject.Application.Features.Departments.Queries.Models;
using SchoolProject.Presentation.Base;
using SchoolProject.Presentation.MetaData;

namespace SchoolProject.Presentation.Controllers;

//[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : AppControllerBase
{

   [HttpGet(DepartmentRoutes.GetById)]
   public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
   {
      var response = await Mediator.Send(query);

      //return Ok(response);
      return NewResult(response);
   }

}
