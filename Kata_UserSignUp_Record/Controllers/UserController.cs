using Kata_UserSignUp_Record.Context;
using Kata_UserSignUp_Record.Models;
using Kata_UserSignUp_Record.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kata_UserSignUp_Record.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public UserController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] UserRequest request)
    {
        User user = new User(Guid.NewGuid(), Email.Create(request.Email), Password.Create(request.Password));

        //var existingUser = _appDbContext.Users.FirstOrDefault(u => u.Email.Value == request.Email);
        //if (existingUser != null)
        //{
        //    return BadRequest("El email ya existe.");
        //}

        _appDbContext.Users.Add(user);
        await _appDbContext.SaveChangesAsync();

        return CreatedAtAction("CreateUser", new { id = user.Id }, user);
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        return await _appDbContext.Users.ToListAsync();
    }

}
