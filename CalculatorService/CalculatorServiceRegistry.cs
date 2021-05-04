using CalculatorService.Contracts;
using Lamar;

namespace CalculatorService
{
    public class CalculatorServiceRegistry : ServiceRegistry
    {
        public CalculatorServiceRegistry()
        {
            For(typeof(IStrategyChooser<>)).Use(typeof(StrategyChooser<>));
            For<IConfigurationBuilder>().Use(new ConfigurationBuilder());
            Scan(_ =>
                {
                    _.AssemblyContainingType<CalculatorServiceRegistry>();
                    _.AddAllTypesOf<ICalculationStrategy>();
                    _.WithDefaultConventions();
                }
            );
            For<ICalculatorConfiguration>().Use(new CalculatorConfiguration()).Singleton();
        }
    }
}