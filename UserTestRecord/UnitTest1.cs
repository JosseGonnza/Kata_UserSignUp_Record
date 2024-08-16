using FluentAssertions;
using Kata_UserSignUp_Record.Controllers;

namespace UserTestRecord
{
    public class UnitTest1
    {
        [Fact(DisplayName = "Should be a valid format")]
        public void should_be_a_valid_format()
        {
            var value = "12345678_";

            Password password = Password.Create(value);

            password.Value.Should().Be(value);
        }

        [Fact(DisplayName = "Should contains at least one under score")]
        public void should_contains_at_least_one_under_score()
        {
            var value = "12345678_";

            Password password = Password.Create(value);

            password.Value.Should().Be(value);
        }

        [Fact(DisplayName = "Should be a valid email format")]
        public void should_be_a_valid_email_format()
        {
            var value = "test@tets.com";

            Email email = Email.Create(value);

            email.Value.Should().Be(value);
        }
    }
}