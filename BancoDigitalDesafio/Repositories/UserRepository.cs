using BancoDigitalDesafio.Data;
using BancoDigitalDesafio.Domain.user;

namespace BancoDigitalDesafio.Repositories;

public class UserRepository : IUserRepository
{ 
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public void ValidateTransaction(User sender, int amount)
    {
        if (sender.UserType == UserType.MERCHANT)
            throw new Exception("Usuário do tipo lojista não está autorizado a realizar transação");

        if (sender.Balance.CompareTo(amount) < 0)
            throw new Exception("Usuário não tem saldo suficiente para executar a ação");
    }

    public User FindUserByDocument(string document)
        => _context.Users.FirstOrDefault(x => x.Document == document) ?? throw new Exception("Usuário não encontrado");

    public User FindUserById(int id)
        => _context.Users.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Usuário não encontrado");

    public void SaveUser(User user)
        => _context.SaveChanges();

}