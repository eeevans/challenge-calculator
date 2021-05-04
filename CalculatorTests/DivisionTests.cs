using CalculatorService;
using CalculatorService.Primatives;
using Xunit;

namespace CalculatorTests
{
    public class DivisionTests
    {
        private readonly ICalculationCoordinator _calculator;

        public DivisionTests()
        {
            IContainer container = new Container();
            container.Configure(c => c.AddRegistry(new CalculatorServiceRegistry()));
            _calculator = container.GetInstance<ICalculationCoordinator>();
            container.Dispose();
        }

        [Fact]
        void should_divide_two_number()
        {
            var result = _calculator.Calculate("d10,5");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(2, result.Answer);
            Assert.Null(result.CalculationException);
        }
    }
}