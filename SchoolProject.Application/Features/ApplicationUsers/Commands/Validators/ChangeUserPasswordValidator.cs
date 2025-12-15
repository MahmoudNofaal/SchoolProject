using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.ApplicationUsers.Commands.Models;
using SchoolProject.Application.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Features.ApplicationUsers.Commands.Validators;

public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
{

   private readonly IStringLocalizer<SharedResources> _stringLocalizer;

   public ChangeUserPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
   {
      this._stringLocalizer = stringLocalizer;

      this.ApplyValidationRules();
      this.ApplyCustomValidations();
   }

   private void ApplyValidationRules()
   {

      RuleFor(x => x.CurrentPassword)
        .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
        .NotNull().WithMessage("{PropertyName} cannot be null.");

      RuleFor(x => x.NewPassword)
        .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
        .NotNull().WithMessage("{PropertyName} cannot be null.");

      RuleFor(x => x.ConfirmPassword)
        .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
        .NotNull().WithMessage("{PropertyName} cannot be null.")
        .Equal(x => x.NewPassword).WithMessage("Passwords doesnot matches!");

   }

   private void ApplyCustomValidations()
   {
   }

}
