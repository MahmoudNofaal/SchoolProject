using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Features.ApplicationUsers.Queries.Results;

public class GetSingleUserResponse
{
   public string FullName { get; set; }
   public string Email { get; set; }
   public string? Address { get; set; }
   public string? Country { get; set; }

}
