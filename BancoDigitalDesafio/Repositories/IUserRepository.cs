using BancoDigitalDesafio.Domain.user;

namespace BancoDigitalDesafio.Repositories;

public interface IUserRepository
{
    public void ValidateTransaction(User sender, int amount);
    public User FindUserByDocument(string document);
    public User FindUserById(int id);
}