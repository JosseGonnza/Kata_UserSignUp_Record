using System.Text.RegularExpressions;

namespace Kata_UserSignUp_Record.ValueObjects;

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
