using SchoolProject.Application.UseCases.Students.Commands.Models;
using SchoolProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA_School_Project.Application.Mappings.Students;

public partial class StudentProfile
{

   public void EditStudentCommandMapping()
   {
      CreateMap<EditStudentCommand, Student>()
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
         .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));

   }

}
