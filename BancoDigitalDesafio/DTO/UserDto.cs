using BancoDigitalDesafio.Domain.user;

namespace BancoDigitalDesafio.DTO;

public record UserDto(int Id,
                    string FirstName,
                    string LastName, 
                    string Document, 
                    string Email, 
                    string Password, 
                    int Balance, 
                    UserType UserType);