using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using SchoolProject.Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.UseCases.Authentication.Commands.Models;

public class RefreshTokenCommand : ICommand<Response<JwtAuthenticationResult>>
{
   public string AcessToken { get; set; }
   public string RefreshToken { get; set; }

}
