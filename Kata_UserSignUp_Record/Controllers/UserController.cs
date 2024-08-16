using Kata_UserSignUp_Record.Models;
using Kata_UserSignUp_Record.Repositories;
using Kata_UserSignUp_Record.ValueObjects;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

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

        if(_repository.GetAll().Any(u => u.Email.Value == request.Email))
            return BadRequest("Este email ya existe.");

        _repository.Save(user);

        return base.Accepted(user);
    }

    [HttpGet]
    public async Task<List<User>> GetAllUsers() => _repository.GetAll();
}
