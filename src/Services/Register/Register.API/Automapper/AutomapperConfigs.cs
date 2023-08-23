using AutoMapper;
using Register.API.DTOs;
using Register.Application.Domain.Entities;

namespace Register.API.Automapper
{
    public class AutomapperConfigs : Profile
    {
        public AutomapperConfigs()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
