using BancoDigitalDesafio.Domain.user;

namespace BancoDigitalDesafio.DTO;

public record UserDto(string FirstName,
                    string LastName, 
                    string Document, 
                    string Email, 
                    string Password, 
                    int Balance, 
                    UserType UserType);