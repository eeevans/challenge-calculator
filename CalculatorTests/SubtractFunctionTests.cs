using CalculatorService;
using CalculatorService.Primatives;
using StructureMap;
using Xunit;

namespace CalculatorTests
{
    public class SubtractFunctionTests
    {
        private readonly ICalculationCoordinator _calculator;

        public SubtractFunctionTests()
        {
            IContainer container = new Container();
            container.Configure(c => c.AddRegistry(new CalculatorServiceRegistry()));
            _calculator = container.GetInstance<ICalculationCoordinator>();
            container.Dispose();
        }

        [Fact]
        void should_subtract_two_number()
        {
            var result = _calculator.Calculate("s10,5");
            Assert.Equal(CalculationStatus.Ok, result.Status);
            Assert.Equal(5, result.Answer);
            Assert.Null(result.CalculationException);
        }
    }
}