using CalculatorService.Contracts;
using System;
using System.Linq;

namespace CalculatorService
{
    public interface ICalculationCoordinator
    {
        /// <summary>
        /// Responsible for addition operation
        /// </summary>
        /// <param name="delimitedInput"></param>
        /// <returns>Result of addition operation</returns>
        CalculationResult Calculate(string delimitedInput);
    }

    /// <summary>
    /// Coordinator to provide calculator operations
    /// </summary>
    public class CalculationCoordinator : ICalculationCoordinator
    {
        private readonly IDelimitedInputParser _parser;
        private readonly IStrategyChooser<ICalculationStrategy> _chooser;

        public CalculationCoordinator(IDelimitedInputParser parser,
            IStrategyChooser<ICalculationStrategy> chooser)
        {
            _parser = parser;
            _chooser = chooser;
        }

        /// <summary>
        /// Responsible for addition operation
        /// </summary>
        /// <param name="delimitedInput"></param>
        /// <returns>Result of addition operation</returns>
        public CalculationResult Calculate(string delimitedInput)
        {
            try
            {
                var operation = delimitedInput.Substring(0, 1).ToLower();
                if (!"asmd".Contains(operation))
                    throw new InvalidOperationException("Operation missing at start of input!");

                var strategy = _chooser.Choose(c => c.CanHandle(operation));
                var addends = _parser.ParseCalcArgs(delimitedInput.Substring(1)).ToArray();

                return strategy.Calculate(addends);
            }
            catch (Exception e)
            {
                return new CalculationResult(e);
            }
        }
    }
}