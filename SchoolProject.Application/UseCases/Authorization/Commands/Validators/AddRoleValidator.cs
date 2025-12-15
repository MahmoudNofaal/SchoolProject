using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Application.UseCases.Authorization.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.UseCases.Authorization.Commands.Validators;

public class AddRoleValidator : AbstractValidator<AddRoleCommand>
{
   private readonly IStringLocalizer<SharedResources> _stringLocalizer;
   private readonly IAuthorizationService _authorizationService;

   public AddRoleValidator(IStringLocalizer<SharedResources> stringLocalizer,
                           IAuthorizationService authorizationService)
   {
      this._stringLocalizer = stringLocalizer;
      this._authorizationService = authorizationService;
      this.ApplyValidationRules();
      this.ApplyCustomValidations();
   }

   public void ApplyValidationRules()
   {
      RuleFor(x => x.RoleName)
         .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
         .NotNull().WithMessage("Role name cannot be null.")
         .MaximumLength(100).WithMessage("Role name must not exceed 100 characters.");

   }

   public void ApplyCustomValidations()
   {
      RuleFor(x => x.RoleName)
         .MustAsync(async (key, CancellationToken) => !await _authorizationService.IsRoleExistsAsync(key))
         .WithMessage("Role already exists!");

   }

}
