using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Features.Students.Commands.Models;

public class AddStudentCommand : ICommand<Response<string>>
{
   public string Name_Ar { get; set; }
   public string Name_En { get; set; }

   public string Address { get; set; }

   public string? Phone { get; set; }

   public int DepartmentId { get; set; }

}
