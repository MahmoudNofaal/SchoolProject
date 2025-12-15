using SchoolProject.Application.UseCases.Students.Commands.Models;
using SchoolProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA_School_Project.Application.Mappings.Students;

public partial class StudentProfile
{

   public void AddStudentCommandMapping()
   {
      CreateMap<AddStudentCommand, Student>()
         .ForMember(dest => dest.Name_Ar, opt => opt.MapFrom(src => src.Name_Ar))
         .ForMember(dest => dest.Name_En, opt => opt.MapFrom(src => src.Name_En));

   }

}
