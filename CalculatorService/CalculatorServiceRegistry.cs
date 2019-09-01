using CalculatorService.Contracts;
using StructureMap;

namespace CalculatorService
{
    public class CalculatorServiceRegistry : Registry
    {
        public CalculatorServiceRegistry()
        {
            For(typeof(IStrategyChooser<>)).Use(typeof(StrategyChooser<>));
            Scan(_ =>
                {
                    _.AssemblyContainingType<CalculatorServiceRegistry>();
                    _.AddAllTypesOf<ICalculationStrategy>();
                    _.WithDefaultConventions();
                }
            );
        }
    }
}