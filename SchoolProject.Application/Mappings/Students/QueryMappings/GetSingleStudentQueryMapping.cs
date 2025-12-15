using SchoolProject.Application.UseCases.Students.Queries.Results;
using SchoolProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA_School_Project.Application.Mappings.Students;

public partial class StudentProfile
{

   public void GetSingleStudentMapping()
   {

      CreateMap<Student, SingleStudentResult>()
         .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Localize(src.Department.Name_Ar, src.Department.Name_En)))
         .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.Name_Ar, src.Name_En)));

   }

}