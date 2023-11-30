
using AutoMapper;
using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.DTO;

namespace BancoDigitalDesafio.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}