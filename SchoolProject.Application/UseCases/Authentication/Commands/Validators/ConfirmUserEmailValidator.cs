using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Resources;
using SchoolProject.Application.UseCases.Authentication.Commands.Models;

namespace SchoolProject.Application.UseCases.Authentication.Commands.Validators;

public class ConfirmUserEmailValidator : AbstractValidator<ConfirmUserEmailCommand>
{

   private readonly IStringLocalizer<SharedResources> _stringLocalizer;

   public ConfirmUserEmailValidator(IStringLocalizer<SharedResources> stringLocalizer)
   {
      this._stringLocalizer = stringLocalizer;

      this.ApplyValidationRules();
      this.ApplyCustomValidations();
   }

   private void ApplyValidationRules()
   {

      RuleFor(x => x.UserId)
         .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
         .NotNull().WithMessage("{PropertyName} cannot be null.");

      RuleFor(x => x.Code)
        .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
        .NotNull().WithMessage("{PropertyName} cannot be null.");

   }

   private void ApplyCustomValidations()
   {
   }


}
