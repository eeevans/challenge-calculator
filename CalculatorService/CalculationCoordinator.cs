using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CalculatorService.Contracts;
using CalculatorService.Core.Extensions;

namespace CalculatorService
{
    public interface ICalculationCoordinator
    {
        /// <summary>
        /// Responsible for addition operation
        /// </summary>
        /// <param name="delimitedInput"></param>
        /// <returns>Result of addition operation</returns>
        AdditionResult Add(string delimitedInput);
    }

    /// <summary>
    /// Coordinator to provide calculator operations
    /// </summary>
    public class CalculationCoordinator : ICalculationCoordinator
    {
        private readonly IDelimitedInputParser _parser;

        public CalculationCoordinator(IDelimitedInputParser parser)
        {
            _parser = parser;
        }

        /// <summary>
        /// Responsible for addition operation
        /// </summary>
        /// <param name="delimitedInput"></param>
        /// <returns>Result of addition operation</returns>
        public AdditionResult Add(string delimitedInput)
        {
            try
            {
                var addends = _parser.ParseCalcArgs(delimitedInput).ToArray();

                return new AdditionResult(addends.Sum(), GetCalculationFormula(addends));
            }
            catch (Exception e)
            {
                return new AdditionResult(e);
            }
        }

        private string GetCalculationFormula(IEnumerable<int> terms)
        {
            var addends = terms;
            return $"{string.Join("+", addends)} = {addends.Sum()}";
        }
    }
}