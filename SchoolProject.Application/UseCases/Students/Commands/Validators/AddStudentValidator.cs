using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Application.UseCases.Students.Commands.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.UseCases.Students.Commands.Validators;

public class AddStudentValidator : AbstractValidator<AddStudentCommand>
{
   private readonly IStudentService _studentService;
   private readonly IStringLocalizer<SharedResources> _stringLocalizer;
   private readonly IDepartmentService _departmentService;

   public AddStudentValidator(IStudentService studentService,
                              IStringLocalizer<SharedResources> stringLocalizer,
                              IDepartmentService departmentService)
	{
      this._studentService = studentService;
      this._stringLocalizer = stringLocalizer;
      this._departmentService = departmentService;

      this.ApplyValidationRules();
		this.ApplyCustomValidations();
   }

	public void ApplyValidationRules()
	{
		RuleFor(x => x.Name_Ar)
			.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage("Student name cannot be null.")
         .MaximumLength(100).WithMessage("Student name must not exceed 100 characters.");

      RuleFor(x => x.Name_En)
         .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
         .NotNull().WithMessage("Student name cannot be null.")
         .MaximumLength(100).WithMessage("Student name must not exceed 100 characters.");

      RuleFor(x => x.Address)
         .NotEmpty().WithMessage("Address is required.")
         .NotNull().WithMessage("{PropertyName} cannot be null.")
         .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

      RuleFor(x => x.DepartmentId)
         .NotEmpty().WithMessage("Address is required.")
         .NotNull().WithMessage("{PropertyName} cannot be null.");

   }

	public void ApplyCustomValidations()
	{
		RuleFor(x => x.Name_Ar)
			.MustAsync(async (key, CancellationToken) => !await _studentService.IsNameExistsAsync(key))
         .WithMessage("Name must be unique.");

      // Add any custom validation logic here if needed in the future
      RuleFor(x => x.DepartmentId)
         .MustAsync(async (key, CancellationToken) => await _departmentService.IsDepartmentIdExistsAsync(key))
         .WithMessage("Department id does not exist.");

   }

}
