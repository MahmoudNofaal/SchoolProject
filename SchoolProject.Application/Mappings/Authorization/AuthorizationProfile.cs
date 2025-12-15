using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Mappings.Authorization;

public partial class AuthorizationProfile : Profile
{

	public AuthorizationProfile()
	{
		this.RolesListResultQueryMapping();
		this.SingleRoleQueryMapping();

	}

}
