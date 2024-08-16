using Kata_UserSignUp_Record.Models;
using Kata_UserSignUp_Record.Repositories;
using Kata_UserSignUp_Record.ValueObjects;
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
        User user = new User(Guid.NewGuid(), Email.Create(request.Email), Password.Create(request.Password));

        if (repository.GetAll().Any(u => u.Email.Value == request.Email))
            return BadRequest("El email ya existe.");

        repository.Save(user);
        return base.Accepted(user);
    }

    [HttpGet]
    public async Task<List<User>> GetAllUsers()
    {
        return repository.GetAll();
    }

}
