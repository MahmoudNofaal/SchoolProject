using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Seeder;

public static class ApplicationUserSeeder
{

   public static async Task SeedAsync(UserManager<ApplicationUser> _userManager)
   {
      var usersCount = await _userManager.Users.CountAsync();
      if (usersCount == 0)
      {
         var adminUser = new ApplicationUser()
         {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            FullName = "Admin School Project",
            Country = "Egypt",
            PhoneNumber = "01012653212",
            Address = "Admin address street 123",
            EmailConfirmed = true,
         };

         var createUserResult = await _userManager.CreateAsync(adminUser, "AdminPassword123!");
         var createRoleResult = await _userManager.AddToRoleAsync(adminUser, "Admin");

      }
   }

}

public static class ApplicationRoleSeeder
{

   public static async Task SeedAsync(RoleManager<ApplicationRole> _roleManager)
   {
      var rolesCount = await _roleManager.Roles.CountAsync();

      if (rolesCount == 0)
      {
         var adminRole = new ApplicationRole()
         {
            Name = "Admin"
         };

         var userRole = new ApplicationRole()
         {
            Name = "User"
         };

         var createRoleResult01 = await _roleManager.CreateAsync(adminRole);
         var createRoleResult02 = await _roleManager.CreateAsync(userRole);

      }
   }

}
