using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Domain.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Features.Authentication.Commands.Models;

public class SignInCommand : ICommand<Response<JwtAuthenticationResult>>
{

   public string Email { get; set; }
   public string Password { get; set; }

}
