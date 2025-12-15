using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Domain.Results;

public class JwtAuthenticationResult
{
   public string AccessToken { get; set; }
   public RefreshToken RefreshToken { get; set; }

}

