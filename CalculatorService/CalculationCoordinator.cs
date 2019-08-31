using System;
using System.Collections.Generic;
using System.Linq;
using CalculatorService.Core.Extensions;

namespace CalculatorService
{
    /// <summary>
    /// Coordinator to provide calculator operations
    /// </summary>
    public class CalculationCoordinator
    {
        /// <summary>
        /// Responsible for addition operation
        /// </summary>
        /// <param name="delimitedInput"></param>
        /// <returns>Result of addition operation</returns>
        public AdditionResult Add(string delimitedInput)
        {
            try
            {
                var addends = ParseCalcArgs(delimitedInput);

                return new AdditionResult(addends.Sum());
            }
            catch (Exception e)
            {
                return new AdditionResult(e);
            }
        }

        /// <summary>
        /// Parse the incoming strings into values that can be operated on by
        /// the calculator.
        /// </summary>
        /// <param name="delimitedInput"></param>
        /// <returns>An array of the numbers for the calculator to operate on</returns>
        private IEnumerable<int> ParseCalcArgs(string delimitedInput)
        {
            IEnumerable<string> calcTerms = new []{delimitedInput};
            calcTerms = calcTerms.SelectMany(s => s.Split(_delimiters));
            var termValues = calcTerms.Select(s => s.ToInt()).ToArray();

            var (valid, invalidTerms) = ValidateTerms(termValues);
            if (valid)
                throw new InvalidOperationException($"Invalid negative numbers: {string.Join(", ", invalidTerms.Select(i => i.ToString()))}");

            return termValues; 
        }

        private readonly char[] _delimiters = { ',', '\n' };

        private (bool Valid,IEnumerable<int> invalidTerms) ValidateTerms(IEnumerable<int> calcTerms)
        {
            var negativeTerms = calcTerms.Where(t => t < 0);
            var distinctNegativeTerms = negativeTerms.Distinct().ToArray();
            return negativeTerms.Any() ? (true, distinctNegativeTerms) : (false, new int[] { });
        }

    }
}