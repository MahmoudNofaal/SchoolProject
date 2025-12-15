using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.Authentication;
using SchoolProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SchoolProject.Infrastructure.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{

	public ApplicationDbContext()
	{
	}

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
   {

	}

	public DbSet<Department> Departments { get; set; }
	public DbSet<DepartmentSubject> DepartmentSubjects { get; set; }
	public DbSet<Student> Students { get; set; }
	public DbSet<StudentSubject> StudentSubjects { get; set; }
	public DbSet<Subject> Subjects { get; set; }
	public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());



   }

}
