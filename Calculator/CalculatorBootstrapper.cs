using CalculatorService;
using StructureMap;

namespace Calculator
{
    public class CalculatorBootstrapper
    {
        public IContainer Configure()
        {
            var container = new Container();
            container.Configure(c => c.AddRegistry(new CalculatorServiceRegistry()));

            return container;
        }
    }
}