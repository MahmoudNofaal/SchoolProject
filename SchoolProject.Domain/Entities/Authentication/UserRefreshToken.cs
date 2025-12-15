using SchoolProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Domain.Entities.Authentication;

public class UserRefreshToken
{
   [Key]
   public int Id { get; set; }

   public int UserId { get; set; }
   public string? AccessToken { get; set; }
   public string? RefreshToken { get; set; }
   public string? JwtId { get; set; }
   public bool IsUsed { get; set; }
   public bool IsRevoked { get; set; }
   public DateTime AddedDate { get; set; }
   public DateTime ExpiryDate { get; set; }

   [ForeignKey(nameof(UserId))]
   [InverseProperty(nameof(ApplicationUser.UserRefreshTokens))]
   public virtual ApplicationUser? User { get; set; }
}
