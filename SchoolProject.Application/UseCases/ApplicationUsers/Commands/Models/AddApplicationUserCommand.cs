using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.UseCases.ApplicationUsers.Commands.Models;

public class AddApplicationUserCommand : ICommand<Response<string>>
{
   public string FullName { get; set; }
   public string Email { get; set; }
   public string? Address { get; set; }
   public string? Country { get; set; }
   public string? PhoneNumber { get; set; }
   public string Password { get; set; }
   public string ConfirmPassword { get; set; }

}
