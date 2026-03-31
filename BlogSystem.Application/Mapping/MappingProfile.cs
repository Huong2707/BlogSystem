using AutoMapper;
using BlogSystem.Application.DTOs;
using BlogSystem.Application.DTOs.Categories;
using BlogSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Mapping
{
    public class MappingProfile : Profile

    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.AvatarUrl))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(
                    src => src.UserRole != null
                    ? src.UserRole.Select(ur => ur.Role.RoleName).ToList() 
                    : new List<string>()
                    ));

            //map category -> categoryDto
            CreateMap<Category, CategoryDto>()
                .ForMember(c => c.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(c => c.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(c => c.CategorySlug, opt => opt.MapFrom(src => src.CateSlug))
                .ForMember(c => c.Description, opt => opt.MapFrom(src => src.CategoryDescription))
             .ForMember(c => c.IsActive, opt => opt.MapFrom(src => src.IsActive))
             .ForMember(c => c.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
        }


    }
}
