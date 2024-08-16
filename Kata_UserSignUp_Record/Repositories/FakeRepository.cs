using Kata_UserSignUp_Record.Models;

namespace Kata_UserSignUp_Record.Repositories;

public class FakeRepository
{
    private List<User> users = new List<User>();

    public void Save(User user) => users.Add(user);

    public List<User> GetAll() => users;
}
