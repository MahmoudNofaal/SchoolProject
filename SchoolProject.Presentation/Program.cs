using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolProject.Application;
using SchoolProject.Domain;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Helpers;
using SchoolProject.Infrastructure;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Seeder;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Presentation;

public class Program
{
   public static async Task Main(string[] args)
   {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.

      builder.Services.AddControllers();
      // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
      builder.Services.AddOpenApi();

      #region Swagger Configuration
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen(options =>
      {
         options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter JWT token like this: Bearer {your token}"
         });

         options.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
             {
                 new OpenApiSecurityScheme
                 {
                     Reference = new OpenApiReference
                     {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                     }
                 },
                 Array.Empty<string>()
             }
         });
      });

      #endregion

      #region Localization Configuration

      builder.Services.AddLocalization(options =>
      {
         options.ResourcesPath = "";
      });

      builder.Services.Configure<RequestLocalizationOptions>(options =>
      {
         var supportedCultures = new List<CultureInfo>
         {
            new CultureInfo("en-US"),
            new CultureInfo("ar-EG"),
         };

         options.DefaultRequestCulture = new RequestCulture("en-US");
         options.SupportedCultures = supportedCultures;
         options.SupportedUICultures = supportedCultures;
      });

      #endregion

      #region CORS Configuration
      var school_mobile = "School_Mobile";
      builder.Services.AddCors(options =>
      {
         options.AddPolicy(name: school_mobile,
                           policy =>
                           {
                              policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                           });
      });
      #endregion

      #region Dependencies (Dependency Injection)
      builder.Services.AddInfrastructureDependencies(builder.Configuration)
                      .AddApplicationDependencies(builder.Configuration)
                      .AddDomainDependencies();
      #endregion

      var app = builder.Build();

      #region Seed Data

      using (var scope = app.Services.CreateScope())
      {
         var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
         var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

         await ApplicationRoleSeeder.SeedAsync(roleManager);
         await ApplicationUserSeeder.SeedAsync(userManager);

      }

      #endregion


      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
         //app.MapOpenApi();
         app.UseSwagger();
         app.UseSwaggerUI();
      }

      #region Localization Middleware

      var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

      app.UseRequestLocalization(localizationOptions!.Value);

      #endregion


      app.UseCors(school_mobile);

      app.UseHttpsRedirection();

      app.UseAuthentication();
      app.UseAuthorization();

      app.MapControllers();

      app.Run();
   }
}
