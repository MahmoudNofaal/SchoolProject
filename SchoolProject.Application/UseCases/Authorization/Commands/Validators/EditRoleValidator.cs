using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Application.UseCases.Authorization.Commands.Models;

namespace SchoolProject.Application.UseCases.Authorization.Commands.Validators;

public class EditRoleValidator : AbstractValidator<EditRoleCommand>
{
   private readonly IStringLocalizer<SharedResources> _stringLocalizer;

   public EditRoleValidator(IStringLocalizer<SharedResources> stringLocalizer)
   {
      this._stringLocalizer = stringLocalizer;

      this.ApplyValidationRules();
      this.ApplyCustomValidations();
   }

   public void ApplyValidationRules()
   {
      RuleFor(x => x.Id)
         .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
         .NotNull().WithMessage("Role id cannot be null.");

      RuleFor(x => x.RoleName)
         .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
         .NotNull().WithMessage("Role name cannot be null.")
         .MaximumLength(100).WithMessage("Role name must not exceed 100 characters.");

   }

   public void ApplyCustomValidations()
   {
   }

}
