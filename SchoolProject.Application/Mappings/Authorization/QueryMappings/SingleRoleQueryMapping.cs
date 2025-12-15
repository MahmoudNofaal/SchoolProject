using AutoMapper;
using SchoolProject.Application.UseCases.Authorization.Queries.Results;
using SchoolProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Mappings.Authorization;

public partial class AuthorizationProfile : Profile
{
   public void SingleRoleQueryMapping()
   {
      CreateMap<ApplicationRole, SingleRoleResult>()
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
         .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
         .ReverseMap();

   }
}
