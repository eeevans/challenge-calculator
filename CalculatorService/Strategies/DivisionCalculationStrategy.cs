using CalculatorService.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorService.Strategies
{
    public class DivisionCalculationStrategy : ICalculationStrategy
    {
        public bool CanHandle(string operation)
        {
            return operation.ToLower().Equals("d");
        }

        public CalculationResult Calculate(IEnumerable<int> terms)
        {
            var divisors = terms.ToArray();
            var quotient = 0;
            bool hasInitialized = false;
            foreach (var term in terms)
            {
                if (!hasInitialized)
                {
                    quotient = term;
                    hasInitialized = true;
                }
                else
                {
                    quotient /= term;
                }
            }

            return new CalculationResult(quotient, GetCalculationFormula(divisors, quotient));
        }

        private string GetCalculationFormula(int[] divisors, int quotient)
        {
            return $"{string.Join("/", divisors)} = {quotient}";
        }
    }
}