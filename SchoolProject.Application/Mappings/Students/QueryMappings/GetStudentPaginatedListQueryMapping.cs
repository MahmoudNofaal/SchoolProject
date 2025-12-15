using SchoolProject.Application.Features.Students.Queries.Results;
using SchoolProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA_School_Project.Application.Mappings.Students;

public partial class StudentProfile
{

   public void GetStudentPaginatedListMapping()
   {

      CreateMap<Student, GetStudentPaginatedListResponse>()
         .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.Name_Ar, src.Name_En)))
         .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Localize(src.Department.Name_Ar, src.Department.Name_En)))
         ; 

   }

}
