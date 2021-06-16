using AutoMapper;
using FurnitureFactory.DTO;
using FurnitureFactory.DTO.Module;
using FurnitureFactory.Models;

namespace FurnitureFactory
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Module, ModuleDto>();
            CreateMap< ModuleDto, Module>();
        }
    }
}