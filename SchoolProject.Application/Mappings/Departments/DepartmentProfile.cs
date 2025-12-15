using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA_School_Project.Application.Mappings.Departments;

public partial class DepartmentProfile : Profile
{

	public DepartmentProfile()
	{

		this.GetSingleDepartmentQueryMapping();

	}

}
