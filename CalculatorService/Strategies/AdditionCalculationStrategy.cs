using CalculatorService.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorService.Strategies
{
    public class AdditionCalculationStrategy : ICalculationStrategy
    {
        public bool CanHandle(string operation)
        {
            return operation.ToLower().Equals("a");
        }

        public CalculationResult Calculate(IEnumerable<int> terms)
        {
            var addends = terms.ToArray();
            return new CalculationResult(addends.Sum(), GetCalculationFormula(addends));
        }

        private string GetCalculationFormula(int[] addends)
        {
            return $"{string.Join("+", addends)} = {addends.Sum()}";
        }
    }
}