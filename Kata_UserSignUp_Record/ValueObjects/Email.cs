using System.Text.RegularExpressions;

namespace Kata_UserSignUp_Record.ValueObjects;

public class Email
{
    public string Value { get; }

    public Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        var emailValidator = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";

        if (!Regex.IsMatch(email, emailValidator))
            throw new Exception(email);

        return new Email(email);
    }
}
