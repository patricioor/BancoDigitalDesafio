using BancoDigitalDesafio.Domain.user;

namespace BancoDigitalDesafio.Repositories;

public interface IUserRepository
{
    public User findUserByDocument(string document);
    public User findUserById(int id);
}