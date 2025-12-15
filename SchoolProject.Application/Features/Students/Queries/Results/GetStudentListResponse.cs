using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolProject.Application.Features.Students.Queries.Results;

public class GetStudentListResponse
{
   public int Id { get; set; }

   public string? Name { get; set; }
   public string? Address { get; set; }
   public string? DepartmentName { get; set; }

}
