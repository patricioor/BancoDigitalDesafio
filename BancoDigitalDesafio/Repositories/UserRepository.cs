using BancoDigitalDesafio.Data;
using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.DTO;
using AutoMapper;

namespace BancoDigitalDesafio.Repositories;

public class UserRepository : IUserRepository
{ 
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public User GetUserById(int id)
        => _context.Users.FirstOrDefault(x => x.Id == id) 
           ?? throw new NullReferenceException("User not found");

    public User CreateUser(UserDto user)
    {
        var newUser = new User()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Document = user.Document,
            Email = user.Email,
            Password = user.Password,
            Balance = user.Balance,
            UserType = user.UserType
        };

        var userMapped = _mapper.Map<User>(newUser);
        _context.Users.Add(userMapped);
        _context.SaveChanges();
        return userMapped;
    }

    public User FindUserByDocument(string document)
        => _context.Users.FirstOrDefault(x => x.Document == document) ?? throw new Exception("Usuário não encontrado");

    public User FindUserById(int id)
        => _context.Users.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Usuário não encontrado");
}