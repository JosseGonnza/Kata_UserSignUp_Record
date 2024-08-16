namespace Kata_UserSignUp_Record.ValueObjects;

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
