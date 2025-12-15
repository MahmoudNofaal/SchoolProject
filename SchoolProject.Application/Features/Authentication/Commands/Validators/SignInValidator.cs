using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.Authentication.Commands.Models;
using SchoolProject.Application.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Features.Authentication.Commands.Validators;

public class SignInValidator : AbstractValidator<SignInCommand>
{

   private readonly IStringLocalizer<SharedResources> _stringLocalizer;

   public SignInValidator(IStringLocalizer<SharedResources> stringLocalizer)
   {
      this._stringLocalizer = stringLocalizer;

      this.ApplyValidationRules();
      this.ApplyCustomValidations();
   }

   private void ApplyValidationRules()
   {

      RuleFor(x => x.Email)
         .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
         .NotNull().WithMessage("{PropertyName} cannot be null.")
         .EmailAddress().WithMessage("Email must be a valid format");

      RuleFor(x => x.Password)
        .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
        .NotNull().WithMessage("{PropertyName} cannot be null.");

   }

   private void ApplyCustomValidations()
   {
   }


}
