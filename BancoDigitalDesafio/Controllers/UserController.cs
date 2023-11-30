using BancoDigitalDesafio.DTO;
using BancoDigitalDesafio.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BancoDigitalDesafio.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Get(int id)
    {
        var user = _userRepository.GetUserById(id) 
                   ?? throw new NullReferenceException("User not found");
        return Ok(user);
    }


    [HttpPost]
    public IActionResult Post(UserDto user)
    {
        var newUser = _userRepository.CreateUser(user);
        return new CreatedAtRouteResult("Get", newUser);
    }
}