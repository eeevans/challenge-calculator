using StructureMap;

namespace CalculatorService
{
    public class CalculatorServiceRegistry : Registry
    {
        public CalculatorServiceRegistry()
        {
            Scan(_ =>
                {
                    _.AssemblyContainingType<CalculatorServiceRegistry>();
                    _.WithDefaultConventions();
                }
            );
        }
    }
}