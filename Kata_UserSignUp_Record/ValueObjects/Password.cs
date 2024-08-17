using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kata_UserSignUp_Record.ValueObjects;

public class Password
{
    public string Value { get; }

    private Password(string password)
    {
        Value = password;
    }

    public Password()
    {
    }

    public override string ToString() => Value;

    public static Password Create(string password)
    {
        if (password.Length < 8 || !password.Contains('_'))
        {
            throw new ArgumentException(password);
        }

        return new Password(password);
    }
}

public class PasswordConverter : ValueConverter<Password, string>
{
    public PasswordConverter() : base(
        password => password.Value,  // Convertir de Password a string
        str => Password.Create(str))         // Convertir de string a Password
    {
    }
}