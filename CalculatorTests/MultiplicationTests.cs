using CalculatorService;
using CalculatorService.Primatives;
using Xunit;

namespace CalculatorTests
{
    public class MultiplicationTests
    {
        private readonly ICalculationCoordinator _calculator;

        public MultiplicationTests()
        {
            IContainer container = new Container();
            container.Configure(c => c.AddRegistry(new CalculatorServiceRegistry()));
            _calculator = container.GetInstance<ICalculationCoordinator>();
            container.Dispose();
        }

        [Fact]
        void should_multiply_two_number()
        {
            var result = _calculator.Calculate("m10,5");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(50, result.Answer);
            Assert.Null(result.CalculationException);
        }
    }
}