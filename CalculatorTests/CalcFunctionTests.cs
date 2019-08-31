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
            var result = calculator.Add("5000");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(5000, result.Sum);
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
    }
}