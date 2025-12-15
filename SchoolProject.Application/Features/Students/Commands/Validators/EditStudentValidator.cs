using FluentValidation;
using SchoolProject.Application.Features.Students.Commands.Models;
using SchoolProject.Application.Services.Abstractions;

namespace SchoolProject.Application.Features.Students.Commands.Validators;

public class EditStudentValidator : AbstractValidator<EditStudentCommand>
{
   private readonly IStudentService _studentService;

   public EditStudentValidator(IStudentService studentService)
   {
      this._studentService = studentService;

      this.ApplyValidationRules();
      this.ApplyCustomValidations();
   }

   public void ApplyValidationRules()
   {
      RuleFor(x => x.Name)
         .NotEmpty().WithMessage("Student name is required.")
         .NotNull().WithMessage("Student name cannot be null.")
         .MaximumLength(100).WithMessage("Student name must not exceed 100 characters.");

      RuleFor(x => x.Address)
         .NotEmpty().WithMessage("Address is required.")
         .NotNull().WithMessage("{PropertyName} cannot be null.")
         .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

   }

   public void ApplyCustomValidations()
   {
      RuleFor(x => x.Name)
         .MustAsync(async (model, key, CancellationToken) =>
            !await _studentService.IsNameExistsExcludeSelfAsync(key, model.Id)
         ).WithMessage("Name is already exists.");

      // Add any custom validation logic here if needed in the future
   }

}
