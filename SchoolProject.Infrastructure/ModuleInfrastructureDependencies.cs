using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Infrastructure.Repositories.Abstractionss;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Infrastructure;

public static class ModuleInfrastructureDependencies
{
   public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
   {
      services.AddTransient<IStudentRepository, StudentRepository>();
      services.AddTransient<IDepartmentRepository, DepartmentRepository>();
      services.AddTransient<ISubjectRepository, SubjectRepository>();
      services.AddTransient<IInstructorRepository, InstructorRepository>();
      services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

      services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

      services.AddDbContext<ApplicationDbContext>(options =>
      {
         options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
      });

      return services;
   }
}
