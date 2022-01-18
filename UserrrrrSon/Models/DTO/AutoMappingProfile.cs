using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserrrrrSon.Models.Authentication;
using AutoMapper;
using UserrrrrSon.Models.models_;
using UserrrrrSon.Models.DTO2;

namespace UserrrrrSon.Models.DTO
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<AppUser, AppUserDTO>().ReverseMap();
            CreateMap<AppUser, UserDetailDTO>().ReverseMap();
            CreateMap<AppRole, RoleDTO>().ReverseMap();
            CreateMap<Branch, BranchDTO>().ReverseMap();
            CreateMap<UserProfile, UserProfileDTO>().ReverseMap();
            CreateMap<UserProfile, UserEditProfileDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Service, ServiceDTO>().ReverseMap();
        }

    }
}
