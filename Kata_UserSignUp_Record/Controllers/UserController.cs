using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Kata_UserSignUp_Record.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly FakeUserRepository _repository;

    public UserController(FakeUserRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserRequest request)
    {
        User user = new User(Guid.NewGuid(), request.Email, request.Password);
        _repository.Save(user);

        return base.Accepted(user);
    }

    [HttpGet]
    public async Task<List<User>> GetAllUsers() => _repository.GetAll();
}

public class FakeUserRepository
{
    private List<User> users = new List<User>();

    public void Save(User user) => users.Add(user);

    public List<User> GetAll() => users;
}

public record User(Guid Id, string Email, string Password);


public record UserRequest(string Email, string Password);