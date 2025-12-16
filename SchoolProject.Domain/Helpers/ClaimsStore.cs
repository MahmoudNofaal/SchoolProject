using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Domain.Helpers;

public static class ClaimsStore
{

   public static List<Claim> Cliams = new()
   {
      new Claim("Create", "false"),
      new Claim("Edit", "false"),
      new Claim("Delete", "false"),
   };

}
