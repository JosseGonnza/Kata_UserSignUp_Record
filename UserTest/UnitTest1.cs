using FluentAssertions;
using Kata_UserSignUp_Record.Controllers;

namespace UserTest
{
    public class UnitTest1
    {
        [Fact(DisplayName = "Should be valid password")]
        public void should_be_valid_password()
        {
            var value = "12345678_";

            var password = Password.Create(value);

            password.password.Should().Be(value);
        }

        [Fact(DisplayName = "Should contains at least one under score")]
        public void should_contains_at_least_one_under_score()
        {
            var value = "12345678_";

            var password = Password.Create(value);

            password.password.Should().Be(value);
        }

        [Fact(DisplayName = "Should be a valid email")]
        public void should_be_a_valid_email()
        {
            var value = "test@test.com";

            var email = Email.Create(value);

            email.Value.Should().Be(value);
        }
    }
}