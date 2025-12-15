using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Domain.Results;

public class RefreshToken
{
   public int Id { get; set; }

   public int UserId { get; set; }
   public string UserEmail { get; set; }

   public string TokenString { get; set; }
   public DateTime ExpiresAt { get; set; }

}
