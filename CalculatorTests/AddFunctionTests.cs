using CalculatorService;
using CalculatorService.Primatives;
using Xunit;

namespace CalculatorTests
{
    public class CalcFunctionTests
    {
        private readonly ICalculationCoordinator _calculator;

        public CalcFunctionTests()
        {
            IContainer container = new Container();
            container.Configure(c => c.AddRegistry(new CalculatorServiceRegistry()));
            _calculator = container.GetInstance<ICalculationCoordinator>();
            container.Dispose();
        }

        // Step 1
        [Fact]
        void should_add_one_number()
        {
            var result = _calculator.Calculate("a5");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(5, result.Answer);
            Assert.Null(result.CalculationException);
        }

        [Fact]
        void should_set_invalid_numbers_to_zero()
        {
            var result = _calculator.Calculate("a5,tytyt");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(5, result.Answer);
            Assert.Null(result.CalculationException);
        }

        [Fact]
        void should_set_missing_numbers_to_zero()
        {
            var result = _calculator.Calculate("a");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(0, result.Answer);
            Assert.Null(result.CalculationException);
        }

        // Step 2
        [Fact]
        void should_support_more_than_two_numbers()
        {
            var result = _calculator.Calculate("a4,5,6");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(15, result.Answer);
            Assert.Null(result.CalculationException);
        }

        // Step 3
        [Fact]
        void should_alternatively_allow_newline_as_delimiter()
        {
            var result = _calculator.Calculate("a1\n2,3");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(6, result.Answer);
            Assert.Null(result.CalculationException);
        }

        // Step 4
        [Fact]
        void should_throw_exception_and_reject_negative_numbers()
        {
            var result = _calculator.Calculate("a1\n2,3,-4");
            Assert.Equal(CalculationStatus.Error, result.Status);
            Assert.NotNull(result.CalculationException);
        }

        // Step 5
        [Fact]
        void should_ignore_numbers_greater_than_1000()
        {
            var result = _calculator.Calculate("a2,1001,6");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(8, result.Answer);
            Assert.Null(result.CalculationException);
        }

        // Step 6
        [Fact]
        void should_support_one_custom_delimiter_of_one_char_length()
        {
            var result = _calculator.Calculate("a//;\n2;5");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(7, result.Answer);
            Assert.Null(result.CalculationException);
        }

        // Step 7
        [Fact]
        void should_support_custom_delimiter_of_variable_char_length()
        {
            var result = _calculator.Calculate("a//[***]\n11***22***33");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(66, result.Answer);
            Assert.Null(result.CalculationException);
        }

        // Step 8
        [Fact]
        void should_support_multiple_custom_delimiter_of_variable_char_length()
        {
            var result = _calculator.Calculate("a//[*][!!][rrr]\n11rrr22*33!!44");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(110, result.Answer);
            Assert.Null(result.CalculationException);
        }

        // Stretch 1
        [Fact]
        void should_display_formula_uses_to_calculate_result()
        {
            var result = _calculator.Calculate("a2,4,rrrr,1001,6");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(12, result.Answer);
            Assert.Null(result.CalculationException);
            Assert.Equal("2+4+0+0+6 = 12", result.Formula);
        }
    }
}