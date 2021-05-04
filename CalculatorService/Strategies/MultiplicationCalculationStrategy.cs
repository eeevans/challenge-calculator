using CalculatorService.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorService.Strategies
{
    public class MultiplicationCalculationStrategy : ICalculationStrategy
    {
        public bool CanHandle(string operation)
        {
            return operation.ToLower().Equals("m");
        }

        public CalculationResult Calculate(IEnumerable<int> terms)
        {
            var multiplicands = terms.ToArray();
            var product = 0;
            bool hasInitialized = false;
            foreach (var term in terms)
            {
                if (!hasInitialized)
                {
                    product = term;
                    hasInitialized = true;
                }
                else
                {
                    product *= term;
                }
            }

            return new CalculationResult(product, GetCalculationFormula(multiplicands, product));
        }

        private string GetCalculationFormula(int[] multiplicands, int product)
        {
            return $"{string.Join("*", multiplicands)} = {product}";
        }
    }
}