using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            var (variableSizeCustomDelimiter, nonVariableSizeCustomDelimitedInput) = GetVariableLengthDelimiter(delimitedInput);
            calcTerms = nonVariableSizeCustomDelimitedInput.Split(variableSizeCustomDelimiter);

            var customDelimiter = GetCustomDelimiter(nonVariableSizeCustomDelimitedInput).ToArray();
            var delimiters = _builtInDelimiters.Union(customDelimiter).ToArray();
            calcTerms = calcTerms.SelectMany(s => s.Split(delimiters));

            var termValues = calcTerms.Select(s => s.ToInt()).ToArray();

            var (valid, termList) = ValidateTerms(termValues);
            if (valid)
                throw new InvalidOperationException($"Invalid negative numbers: {string.Join(", ", termList.Select(i => i.ToString()))}");

            return termList.ToArray(); 
        }

        private IEnumerable<char> GetCustomDelimiter(string delimitedInput)
        {
            var variableLengthDelimiter = GetVariableLengthDelimiter(delimitedInput);
            return delimitedInput.StartsWith(_customDelimiterToken)
                ? delimitedInput.Substring(_customDelimiterToken.Length, 1).ToCharArray()
                : Enumerable.Empty<char>();
        }

        private (string customDelimiter, string nonCustomDelimitedInput) GetVariableLengthDelimiter(string delimitedInput)
        {
            var pattern = new Regex(_variableLengthDelimiterPattern);
            var match = pattern.Match(delimitedInput);
            if (match.Success == false)
                return (String.Empty, delimitedInput);

            var delimiterPattern = new Regex(_variableLengthDelimiterValuePattern);
            var valueMatch = delimiterPattern.Match(match.Value);
            return valueMatch.Success == true ? (valueMatch.Value, delimitedInput.Replace(match.Value, string.Empty))
                : (string.Empty, delimitedInput);
        }

        private readonly char[] _builtInDelimiters = { ',', '\n' };
        private readonly string _customDelimiterToken = "//";
        private readonly string _variableLengthDelimiterPattern = @"\/\/\[[^\[\]]+\]";
        private readonly string _variableLengthDelimiterValuePattern = @"[^\[\]\/]+";

        private (bool Valid, IEnumerable<int> termList) ValidateTerms(IEnumerable<int> calcTerms)
        {
            var terms = calcTerms.ToArray();
            var negativeTerms = terms.Where(t => t < 0).ToArray();
            var distinctNegativeTerms = negativeTerms.Distinct().ToArray();
            var validTerms = terms.Where(t => t <= 1000).ToArray();
            return negativeTerms.Any() ? (true, distinctNegativeTerms) : (false, validTerms);
        }

    }
}