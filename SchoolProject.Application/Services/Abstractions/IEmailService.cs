using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Services.Abstractions;

public interface IEmailService
{

   Task<string> SendEmailAsync(string userEmail, string message, string? reason);

}
