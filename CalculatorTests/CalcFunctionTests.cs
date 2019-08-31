using CalculatorService;
using CalculatorService.Primatives;
using Xunit;

namespace CalculatorTests
{
    public class CalcFunctionTests
    {
        private CalculationCoordinator calculator;
        public CalcFunctionTests()
        {
            calculator = new CalculationCoordinator();
        }

        [Fact]
        void should_add_one_number()
        {
            var result = calculator.Add("5");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(5, result.Sum);
            Assert.Null(result.CalculationException);
        }

        [Fact]
        void should_add_two_numbers()
        {
            var result = calculator.Add("4,5");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(9, result.Sum);
            Assert.Null(result.CalculationException);
        }

        [Fact]
        void should_support_a_more_than_two_numbers()
        {
            var result = calculator.Add("4,5,6");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(15, result.Sum);
            Assert.Null(result.CalculationException);
        }

        [Fact]
        void should_set_invalid_numbers_to_zero()
        {
            var result = calculator.Add("5,tytyt");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(5, result.Sum);
            Assert.Null(result.CalculationException);
        }

        [Fact]
        void should_set_missing_numbers_to_zero()
        {
            var result = calculator.Add("");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(0, result.Sum);
            Assert.Null(result.CalculationException);
        }

        [Fact]
        void should_alternatively_allow_newline_as_delimiter()
        {
            var result = calculator.Add("1\n2,3");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(6, result.Sum);
            Assert.Null(result.CalculationException);
        }

        [Fact]
        void should_throw_exception_and_reject_negative_numbers()
        {
            var result = calculator.Add("1\n2,3,-4");
            Assert.Equal(CalculationStatus.Error, result.Status);
            Assert.NotNull(result.CalculationException);
        }

        [Fact]
        void should_ignore_numbers_greater_than_1000()
        {
            var result = calculator.Add("2,1001,6");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(8, result.Sum);
            Assert.Null(result.CalculationException);
        }

        [Fact]
        void should_support_one_custom_delimiter_of_one_char_length()
        {
            var result = calculator.Add("//;\n2;5");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(7, result.Sum);
            Assert.Null(result.CalculationException);
        }
    }
}