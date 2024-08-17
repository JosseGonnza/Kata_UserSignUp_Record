using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        var emailValidator = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        if (!Regex.IsMatch(email, emailValidator, RegexOptions.IgnoreCase))
            throw new ArgumentException("Email inválido", nameof(email));

        return new Email(email);
    }
}


public class EmailConverter : ValueConverter<Email, string>
{
    public EmailConverter() : base(
        email => email.Value,  // Convertir de Email a string
        str => Email.Create(str))      // Convertir de string a Email
    {
    }
}