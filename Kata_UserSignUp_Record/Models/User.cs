using Kata_UserSignUp_Record.ValueObjects;

namespace Kata_UserSignUp_Record.Models;

public record User(Guid Id, Email Email, Password Password);
