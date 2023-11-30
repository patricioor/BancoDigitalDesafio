using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.DTO;

namespace BancoDigitalDesafio.Repositories;

public interface IUserRepository
{
    public User GetUserById(int id);
    public User CreateUser(UserDto user);
    public User FindUserByDocument(string document);
    public User FindUserById(int id);
}