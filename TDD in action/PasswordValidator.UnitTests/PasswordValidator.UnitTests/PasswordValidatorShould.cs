
using FluentAssertions;

namespace PasswordValidator.UnitTests
{
    public class PasswordValidatorShould
    {
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("abc")]
        [InlineData("abcd")]
        [InlineData("abcd1")]
        [InlineData("abcd12")]
        [InlineData("abcd123")]
        public void ThrowArguementException_WhenShortPassword(string password)
        {
            Assert.Throws<ArgumentException>(() => PasswordValidator.ValidatePassword(password));
        }

        [Theory]
        [InlineData("ABCDEf1_")]
        [InlineData("ABCDef1_")]
        [InlineData("ABCDe12_")]
        public void ReturnsTrue_WhenPasswordIsValid(string password)
        {
            var isValid = PasswordValidator.ValidatePassword(password);

            isValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("abcde12_")]
        [InlineData("abcde13_")]
        [InlineData("abcde14_")]
        public void ReturnsFalse_WhenPasswordDoesNotHaveUpperCase(string password)
        {
            var isValid = PasswordValidator.ValidatePassword(password);

            isValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("ABCDEF1_")]
        [InlineData("ABCDEF2_")]
        [InlineData("ABCDEF3_")]
        public void ReturnsFalse_WhenPasswordDoesNotHaveLowerCase(string password)
        {
            var isValid = PasswordValidator.ValidatePassword(password);

            isValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("ABCDEFg_")]
        [InlineData("__CDEfg_")]
        [InlineData("_BCDefg_")]
        public void ReturnsFalse_WhenPasswordDoesNotHaveUigit(string password)
        {
            var isValid = PasswordValidator.ValidatePassword(password);

            isValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("ABcd1234")]
        [InlineData("ABcdde34")]
        [InlineData("ABcdfg34")]
        public void ReturnsFalse_WhenPasswordDoesNotHaveUnderscore(string password)
        {
            var isValid = PasswordValidator.ValidatePassword(password);

            isValid.Should().BeFalse();
        }
    }

    public class PasswordValidator
    {
        internal static bool ValidatePassword(string password)
        {
            if (password.Length < 8)
            {
                throw new ArgumentException();
            }

            var hasUpperCase = false;
            var hasDigit = false;
            var hasLowerCase = false;
            var hasUnderscore = false;

            foreach (char character in password)
            {
                if (char.IsUpper(character))
                    hasUpperCase = true;

                if (char.IsLower(character))
                    hasLowerCase = true;

                if (char.IsDigit(character))
                    hasDigit = true;

                if (character == '_')
                    hasUnderscore = true;
            }

            return hasUpperCase && hasDigit && hasLowerCase && hasUnderscore;
        }
    }
}