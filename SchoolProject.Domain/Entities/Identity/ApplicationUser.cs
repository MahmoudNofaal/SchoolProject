using Microsoft.AspNetCore.Identity;
using SchoolProject.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolProject.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser<int>
{

   public ApplicationUser()
   {
      this.UserRefreshTokens = new HashSet<UserRefreshToken>();
   }

   public string FullName { get; set; }
   public string? Address { get; set; }
   public string? Country { get; set; }

   [InverseProperty(nameof(UserRefreshToken.User))]
   public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }

}
