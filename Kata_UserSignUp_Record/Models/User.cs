using Kata_UserSignUp_Record.ValueObjects;

namespace Kata_UserSignUp_Record.Models;

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

    public User()
    {
    }

    public void UpdateEmail(Email newEmail)
    {
        Email = newEmail;
    }

    public void UpdatePassword(Password newPassword)
    {
        Password = newPassword;
    }
}
