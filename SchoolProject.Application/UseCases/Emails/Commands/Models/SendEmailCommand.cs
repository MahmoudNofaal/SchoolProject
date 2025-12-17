using SchoolProject.Application.Bases;
using SchoolProject.Application.Bases.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.UseCases.Emails.Commands.Models;

public class SendEmailCommand : ICommand<Response<string>>
{
   public string UserEmail { get; set; }
   public string Message { get; set; }

}
