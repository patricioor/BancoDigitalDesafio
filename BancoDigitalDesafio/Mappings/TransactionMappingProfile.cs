using AutoMapper;
using BancoDigitalDesafio.Domain.Transaction;
using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.DTO;

namespace BancoDigitalDesafio.Mappings;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<TransactionOp, TransactionDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}