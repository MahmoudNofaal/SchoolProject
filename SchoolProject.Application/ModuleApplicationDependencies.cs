using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Application.Services;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Helpers;
using SchoolProject.Infrastructure.Context;
using System.Reflection;
using System.Text;

namespace SchoolProject.Application;

public static class ModuleApplicationDependencies
{
   public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
   {
      // Application Services
      services.AddTransient<IStudentService, StudentService>();
      services.AddTransient<IDepartmentService, DepartmentService>();
      services.AddTransient<IAuthenticationService, AuthenticationService>();
      services.AddTransient<IAuthorizationService, AuthorizationService>();
      services.AddTransient<IEmailService, EmailService>();
      services.AddTransient<IApplicationUserService, ApplicationUserService>();

      // MediatR
      services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

      // AutoMapper
      services.AddAutoMapper(Assembly.GetExecutingAssembly());

      // FluentValidation
      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviors.ValidationBehavior<,>));

      #region Email Configuration
      var emailSettings = new EmailSettings();

      configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);

      services.AddSingleton(emailSettings);
      #endregion

      #region Identity
      services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
      {
         // Password settings.
         options.Password.RequireDigit = true;
         options.Password.RequireLowercase = true;
         options.Password.RequireNonAlphanumeric = true;
         options.Password.RequireUppercase = true;
         options.Password.RequiredLength = 6;
         options.Password.RequiredUniqueChars = 1;

         // Lockout settings.
         options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
         options.Lockout.MaxFailedAccessAttempts = 5;
         options.Lockout.AllowedForNewUsers = true;

         // User settings.
         options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
         options.User.RequireUniqueEmail = true;
         options.SignIn.RequireConfirmedEmail = true;
      })
      .AddEntityFrameworkStores<ApplicationDbContext>()
      .AddDefaultTokenProviders();
      #endregion

      #region Authentication - JWT(JSON Web Token) Bearer Token
      // JWT SETTINGS
      var jwtSettings = new JwtSettings();
      configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
      services.AddSingleton(jwtSettings);

      // JWT Authentication
      services.AddAuthentication(options =>
      {
         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
      {
         options.RequireHttpsMetadata = false;
         options.SaveToken = true;
         options.TokenValidationParameters = new TokenValidationParameters
         {
            ValidateIssuer = jwtSettings.ValidateIssuer,
            ValidIssuers = new[] { jwtSettings.Issuer },
            ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigninKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            ValidateAudience = jwtSettings.ValidateAudience,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = jwtSettings.ValidateLifetime
         };
      });
      #endregion

      #region Authorization - Policy
      services.AddAuthorization(options =>
      {
         // set policy name
         options.AddPolicy("CreateStudent", policy =>
         {
            // set claim type and value
            policy.RequireClaim("Create", "True");
         });

         options.AddPolicy("EditStudent", policy =>
         {
            // set claim type and value
            policy.RequireClaim("Edit", "True");
         });

         options.AddPolicy("DeleteStudent", policy =>
         {
            // set claim type and value
            policy.RequireClaim("Delete", "True");
         });

      });
      #endregion

      return services;
   }
}
