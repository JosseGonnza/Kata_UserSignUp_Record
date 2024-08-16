using Microsoft.AspNetCore.Mvc;

namespace Kata_UserSignUp_Record.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly FakeUserRepository repository;

    public UserController(FakeUserRepository repository)
    {
        this.repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserRequest request)
    {
        User user = new User(Guid.NewGuid(), request.Email, Password.Create(request.Password));
        repository.Save(user);

        return Accepted(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(repository.GetAll());
    }
}

public class FakeUserRepository
{
    private List<User> users = new List<User>();

    public void Save(User user)
    {
        users.Add(user);
    }

    public List<User> GetAll()
    {
        return users;
    }
}

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public Password Password { get; private set; }

    public User(Guid id, string email, Password password)
    {
        Id = id;
        Email = email;
        Password = password;
    }
}

public class Password
{
    public string password { get; }

    private Password(string password)
    {
        this.password = password;
    }

    public static Password Create(string password)
    {
        if (password.Length < 8 || !password.Contains('_'))
            throw new ArgumentException(password);

        return new Password(password);
    }
}

public record UserRequest(string Email, string Password);