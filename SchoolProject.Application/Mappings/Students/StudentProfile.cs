using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA_School_Project.Application.Mappings.Students;

public partial class StudentProfile : Profile
{

	public StudentProfile()
	{

		this.GetStudentListMapping();
		this.GetSingleStudentMapping();
		this.AddStudentCommandMapping();
		this.EditStudentCommandMapping();
		this.GetStudentPaginatedListMapping();

   }

}

