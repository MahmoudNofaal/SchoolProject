using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Resources;
using SchoolProject.Application.UseCases.Emails.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.UseCases.Emails.Commands.Validators;

public class SendEmailValidator : AbstractValidator<SendEmailCommand>
{
   private readonly IStringLocalizer<SharedResources> _stringLocalizer;

   public SendEmailValidator(IStringLocalizer<SharedResources> stringLocalizer)
	{
      this._stringLocalizer = stringLocalizer;

		this.ApplyValidationRules();
		this.ApplyCustomValidations();
   }

	public void ApplyValidationRules()
	{
      RuleFor(x => x.UserEmail)
         .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
         .NotNull().WithMessage("{PropertyName} cannot be null.")
         .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

      RuleFor(x => x.Message)
         .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
         .NotNull().WithMessage("{PropertyName} cannot be null.")
         .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

   }

   public void ApplyCustomValidations()
	{

	}

}
