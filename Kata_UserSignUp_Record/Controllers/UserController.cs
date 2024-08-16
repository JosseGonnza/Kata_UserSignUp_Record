using Microsoft.AspNetCore.Authorization.Infrastructure;
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
        User user = new User(Guid.NewGuid(), Email.Create(request.Email), Password.Create(request.Password));
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

public record User(Guid Id, Email Email, Password Password);

public class Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        var emailValidation = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";

        if (!Regex.IsMatch(email, emailValidation))
            throw new ArgumentException(email);

        return new Email(email);
    }
}

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