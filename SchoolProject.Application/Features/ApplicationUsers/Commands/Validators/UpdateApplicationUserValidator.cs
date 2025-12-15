using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.ApplicationUsers.Commands.Models;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.ApplicationUsers.Commands.Validators;

public class UpdateApplicationUserValidator : AbstractValidator<UpdateApplicationUserCommand>
{
   private readonly IStringLocalizer<SharedResources> _stringLocalizer;

   public UpdateApplicationUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
   {
      this._stringLocalizer = stringLocalizer;

      this.ApplyValidationRules();
      this.ApplyCustomValidations();
   }

   private void ApplyValidationRules()
   {
      RuleFor(x => x.FullName)
         .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
         .NotNull().WithMessage("{PropertyName} cannot be null.")
         .MaximumLength(100).WithMessage("User full name must not exceed 100 characters.");

      RuleFor(x => x.Address)
         .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
         .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

      RuleFor(x => x.Email)
         .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
         .NotNull().WithMessage("{PropertyName} cannot be null.")
         .EmailAddress().WithMessage("Email must be a valid format");


   }

   private void ApplyCustomValidations()
   {
   }


}
