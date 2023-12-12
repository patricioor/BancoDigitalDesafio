using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.DTO;

namespace BancoDigitalDesafio.Repositories;

public interface IUserRepository
{
    public UserDto GetUserById(int id);
    public User CreateUser(UserDto user);
}