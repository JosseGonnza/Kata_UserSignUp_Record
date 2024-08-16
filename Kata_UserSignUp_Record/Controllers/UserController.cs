using Microsoft.AspNetCore.Mvc;

namespace Kata_UserSignUp_Record.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly FakeRepository repository;

    public UserController(FakeRepository repository)
    {
        this.repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserRequest request)
    {
        User user = new User(Guid.NewGuid(), request.Email, Password.Create(request.Password));
        repository.Save(user);
        return base.Accepted(user);
    }

    [HttpGet]
    public async Task<List<User>> GetAllUsers()
    {
        return repository.GetAll();
    }

}

public class FakeRepository
{
    private List<User> users = new List<User>();

    public void Save(User user) => users.Add(user);

    public List<User> GetAll() => users;
}

public record User(Guid Id, string Email, Password Password);

public class Password
{
    public string Value { get; }

    private Password(string value)
    {
        Value = value;
    }

    public static Password Create(string password)
    {
        if (password.Length < 8 || !password.Contains('_'))
            throw new ArgumentException(password);

        return new Password(password);
    }
}

public record UserRequest(string Email, string Password);