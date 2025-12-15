using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Features.ApplicationUsers.Commands.Models;

public class ChangeUserPasswordCommand : ICommand<Response<string>>
{
   public int Id { get; set; }
   public string CurrentPassword { get; set; }
   public string NewPassword { get; set; }
   public string ConfirmPassword { get; set; }

}
