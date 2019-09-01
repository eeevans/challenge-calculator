using System.Collections.Generic;

namespace CalculatorService.Contracts
{
    public interface ICalculationStrategy
    {
        bool CanHandle(string operation);
        CalculationResult Calculate(IEnumerable<int> values);
    }
}