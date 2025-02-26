
using FluentAssertions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FizzBuzz.UnitTests
{
    public class FizzBuzzShould
    {
        [Fact]
        public void ThrowException_WhenNotAPositiveNumber()
        {
            Assert.Throws<Exception>(()=> FizzBuzz.Calculate(-1));
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(2, "2")]
        [InlineData(4, "4")]
        [InlineData(14, "14")]
        public void Return_SimpleNumber(int number, string expectedSimpleNumber)
        {
            var simpleNumber = FizzBuzz.Calculate(number);

            simpleNumber.Should().Be(expectedSimpleNumber);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(9)]
        [InlineData(18)]
        [InlineData(21)]
        public void Return_FizzNumber(int number)
        {
            var simpleNumber = FizzBuzz.Calculate(number);

            simpleNumber.Should().Be("Fizz");
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        public void Retur_BuzzNumber(int number)
        {
            var simpleNumber = FizzBuzz.Calculate(number);

            simpleNumber.Should().Be("Buzz");
        }

        [Theory]
        [InlineData(15)]
        [InlineData(30)]
        [InlineData(45)]
        public void Retur_FizzBuzzNumber(int number)
        {
            var simpleNumber = FizzBuzz.Calculate(number);

            simpleNumber.Should().Be("FizzBuzz");
        }
    }

    public class FizzBuzz
    {
        internal static string Calculate(int number)
        {
            if (number < 0)
                throw new Exception("Only positive itegers allowed");

            if (number % 3 == 0 && number % 5 == 0) 
                return "FizzBuzz";

            if (number % 3 == 0)
                return "Fizz";

            if (number % 5 == 0)
                return "Buzz";

            return number.ToString();
        }
    }
}