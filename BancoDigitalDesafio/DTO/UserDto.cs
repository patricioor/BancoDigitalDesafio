namespace BancoDigitalDesafio.DTO;

public class UserDto
{
    public UserDto(string? firstName, string? lastName, string? document, string? email, string? password, int balance, UserTypeDto userType)
    {
        FirstName = firstName;
        LastName = lastName;
        Document = document;
        Email = email;
        Password = password;
        Balance = balance;
        UserType = userType;
    }
    
    public string? FirstName { get; }
    public string? LastName { get; }
    public string? Document { get; }
    public string? Email { get; }
    public string? Password { get; }
    public int Balance { get; }
    public UserTypeDto UserType { get; }
};