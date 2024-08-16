using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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
        User user = new User(Guid.NewGuid(), Email.Create(request.Email), Password.Create(request.Password));

        if (repository.GetAll().Any(u => u.Email.Value == request.Email))
            return BadRequest("El email " + request.Email + " ya existe.");

        repository.Save(user);

        return Accepted(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers() => Ok(repository.GetAll());
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
    public Email Email { get; private set; }
    public Password Password { get; private set; }

    public User(Guid id, Email email, Password password)
    {
        Id = id;
        Email = email;
        Password = password;
    }
}

public class Email
{
    public string Value { get; }

    private Email(string email)
    {
        Value = email;
    }

    public static Email Create(string email)
    {
        var validationEmail = "^[a-zA-Z0-9._%±]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";

        if (!Regex.IsMatch(email, validationEmail))
            throw new ArgumentException(email);

        return new Email(email);
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