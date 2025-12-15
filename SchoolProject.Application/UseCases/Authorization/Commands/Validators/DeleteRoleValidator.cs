using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Resources;
using SchoolProject.Application.UseCases.Authorization.Commands.Models;

namespace SchoolProject.Application.UseCases.Authorization.Commands.Validators;

public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
{
   private readonly IStringLocalizer<SharedResources> _stringLocalizer;

   public DeleteRoleValidator(IStringLocalizer<SharedResources> stringLocalizer)
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

   }

   public void ApplyCustomValidations()
   {
   }

}
