using SchoolProject.Application.Features.ApplicationUsers.Queries.Results;
using SchoolProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA_School_Project.Application.Mappings.ApplicationUsers;

public partial class ApplicationUserProfile
{
   public void GetSingleUserQueryMapping()
   {
      CreateMap<ApplicationUser, GetSingleUserResponse>()
         .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
         .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
         .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
         .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));

   }

}
