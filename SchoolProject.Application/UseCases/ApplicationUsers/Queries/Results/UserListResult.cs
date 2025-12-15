using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.UseCases.ApplicationUsers.Queries.Results;

public class UserListResult
{
   public int Id { get; set; }
   public string FullName { get; set; }
   public string Email { get; set; }

   public string? Address { get; set; }
   public string? Country { get; set; }

}
