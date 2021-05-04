using CalculatorService.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorService.Strategies
{
    public class SubtractionCalculationStrategy : ICalculationStrategy
    {
        public bool CanHandle(string operation)
        {
            return operation.ToLower().Equals("s");
        }

        public CalculationResult Calculate(IEnumerable<int> terms)
        {
            var subtrahends = terms.ToArray();
            var difference = 0;
            bool hasInitialized = false;
            foreach (var term in terms)
            {
                if (!hasInitialized)
                {
                    difference = term;
                    hasInitialized = true;
                }
                else
                {
                    difference -= term;
                }
            }

            return new CalculationResult(difference, GetCalculationFormula(subtrahends, difference));
        }

        private string GetCalculationFormula(int[] subtrahends, int difference)
        {
            return $"{string.Join("-", subtrahends)} = {difference}";
        }
    }
}