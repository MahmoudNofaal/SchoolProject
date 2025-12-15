using SchoolProject.Application.UseCases.Departments.Queries.Results;
using SchoolProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA_School_Project.Application.Mappings.Departments;

public partial class DepartmentProfile
{
   public void GetSingleDepartmentQueryMapping()
   {
      CreateMap<Department, SingleDepartmentResult>()
         .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.Name_Ar, src.Name_En)))
         .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager.Localize(src.Manager.Name_Ar, src.Manager.Name_En)))
         //.ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
         .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.DepartmentSubjects))
         .ForMember(dest => dest.Instructors, opt => opt.MapFrom(src => src.Instructors));

      //CreateMap<Student, SingleStudentResult>()
      //   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      //   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.Name_Ar, src.Name_En)));

      CreateMap<Instructor, SingleInstructorResult>()
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
         .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.Name_Ar, src.Name_En)));

      CreateMap<DepartmentSubject, SingleSubjectResult>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubjectId))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.Localize(src.Subject.Name_Ar, src.Subject.Name_En)));

   }
}
