using CalculatorService;
using Lamar;

namespace Calculator
{
    public class CalculatorBootstrapper
    {
        public IContainer Configure()
        {
            var container = new Container(new CalculatorServiceRegistry());

            return container;
        }
    }
}