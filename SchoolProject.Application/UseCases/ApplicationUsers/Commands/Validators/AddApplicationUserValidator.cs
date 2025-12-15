using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Resources;
using SchoolProject.Application.UseCases.ApplicationUsers.Commands.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.UseCases.ApplicationUsers.Commands.Validators;

public class AddApplicationUserValidator : AbstractValidator<AddApplicationUserCommand>
{
   private readonly IStringLocalizer<SharedResources> _stringLocalizer;

   public AddApplicationUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
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

      RuleFor(x => x.Password)
        .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
        .NotNull().WithMessage("{PropertyName} cannot be null.");

      RuleFor(x => x.ConfirmPassword)
        .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
        .NotNull().WithMessage("{PropertyName} cannot be null.")
        .Equal(x => x.Password).WithMessage("Passwords doesnot matches!");

   }

   private void ApplyCustomValidations()
   {
   }


}
