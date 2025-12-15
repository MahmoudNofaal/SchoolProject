using SchoolProject.Application.Features.ApplicationUsers.Commands.Models;
using SchoolProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA_School_Project.Application.Mappings.ApplicationUsers;

public partial class ApplicationUserProfile
{

   public void UpdateUserCommandMapping()
   {

      CreateMap<UpdateApplicationUserCommand, ApplicationUser>()
         .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
         .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
         .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
         .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
         .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
         .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
         .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
         ;

   }

}
