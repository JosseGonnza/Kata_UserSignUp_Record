namespace Kata_UserSignUp_Record.Controllers;

public class UserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }

    public UserRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
