using BancoDigitalDesafio.Data;
using BancoDigitalDesafio.Domain.user;
using BancoDigitalDesafio.DTO;
using AutoMapper;
using BancoDigitalDesafio.Data.CustomException;
using Microsoft.EntityFrameworkCore;

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


    public UserDto GetUserById(int id)
    {
       var user = _context.Users.AsNoTracking()
                      .Include(x => x.SentTransactions)
                      .Include(x => x.ReceivedTransactions)
                      .FirstOrDefault(x => x.Id == id)
                  ?? throw new HttpException(StatusCodes.Status404NotFound,"User not found");
       
       return _mapper.Map<UserDto>(user);
    }

    public User CreateUser(UserDto user)
    {
        SearchSimilarDocument(_mapper.Map<User>(user));
        
        var newUser = new User()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Document = user.Document,
            Email = user.Email,
            Password = user.Password,
            Balance = user.Balance,
            UserType = (UserType) user.UserType
        };

        var userMapped = _mapper.Map<User>(newUser);
        _context.Users.Add(userMapped);
        _context.SaveChanges();
        return userMapped;
    }
    
    private async void SearchSimilarDocument(User user)
    {
        var userFound = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
        if (userFound != null)
            throw new HttpException(StatusCodes.Status400BadRequest,"Document inserted previously registered.");
        userFound = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
        if (userFound != null)
            throw new HttpException(StatusCodes.Status400BadRequest,"Email inserted previously registered.");
    }
}