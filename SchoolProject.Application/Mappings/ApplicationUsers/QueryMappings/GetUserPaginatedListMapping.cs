using SchoolProject.Application.UseCases.ApplicationUsers.Queries.Results;
using SchoolProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA_School_Project.Application.Mappings.ApplicationUsers;

public partial class ApplicationUserProfile
{

   public void GetUserPaginatedListMapping()
   {
      CreateMap<ApplicationUser, UserListResult>()
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
         .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
         .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
         .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
         .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));

   }

}
