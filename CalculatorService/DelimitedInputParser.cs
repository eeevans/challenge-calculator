using CalculatorService.Contracts;
using CalculatorService.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CalculatorService
{
    public interface IDelimitedInputParser
    {
        /// <summary>
        /// Parse the incoming strings into values that can be operated on by
        /// the calculator.
        /// </summary>
        /// <param name="delimitedInput"></param>
        /// <returns>An array of the numbers for the calculator to operate on</returns>
        IEnumerable<int> ParseCalcArgs(string delimitedInput);
    }

    public class DelimitedInputParser : IDelimitedInputParser
    {
        private readonly ICalculatorConfiguration _configuration;

        public DelimitedInputParser(ICalculatorConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Parse the incoming strings into values that can be operated on by
        /// the calculator.
        /// </summary>
        /// <param name="delimitedInput"></param>
        /// <returns>An array of the numbers for the calculator to operate on</returns>
        public IEnumerable<int> ParseCalcArgs(string delimitedInput)
        {
            IEnumerable<string> calcTerms = new[] { delimitedInput };
            var (variableSizeCustomDelimiters, nonVariableSizeCustomDelimitedInput) = GetVariableLengthDelimiters(delimitedInput);
            calcTerms = new[] { nonVariableSizeCustomDelimitedInput };
            foreach (var variableSizeCustomDelimiter in variableSizeCustomDelimiters)
            {
                calcTerms = calcTerms.SelectMany(s => s.Split(variableSizeCustomDelimiter));
            }

            var customDelimiter = GetCustomDelimiter(nonVariableSizeCustomDelimitedInput).ToArray();
            var delimiters = _builtInDelimiters
                                .Union(new[] { _configuration.AlternateDelimiter })
                                .Union(customDelimiter).ToArray();
            calcTerms = calcTerms.SelectMany(s => s.Split(delimiters));

            var termValues = calcTerms.Select(s => s.ToInt()).ToArray();

            var (valid, termList) = ValidateTerms(termValues);
            if (valid)
                throw new InvalidOperationException($"Invalid negative numbers: {string.Join(", ", termList.Select(i => i.ToString()))}");

            return termList.ToArray();
        }

        private IEnumerable<char> GetCustomDelimiter(string delimitedInput)
        {
            var variableLengthDelimiter = GetVariableLengthDelimiters(delimitedInput);
            return delimitedInput.StartsWith(_customDelimiterToken)
                ? delimitedInput.Substring(_customDelimiterToken.Length, 1).ToCharArray()
                : Enumerable.Empty<char>();
        }

        private (IEnumerable<String> customDelimiters, string nonCustomDelimitedInput) GetVariableLengthDelimiters(string delimitedInput)
        {
            var variableLengthCustomDelimiters = new List<string>();
            var variableLengthDelimiterPatternRegex = new Regex(_variableLengthDelimiterPattern);
            var variableLengthDelimiterMatch = variableLengthDelimiterPatternRegex.Match(delimitedInput);
            if (!variableLengthDelimiterMatch.Success == true)
                return (Enumerable.Empty<string>(), delimitedInput);

            var variableLengthDelimiterValueRegex = new Regex(_variableLengthDelimiterValuePattern);
            var variableLengthDelimiterValueMatches = variableLengthDelimiterValueRegex.Matches(variableLengthDelimiterMatch.Value);
            if (variableLengthDelimiterValueMatches.Count > 0)
            {
                var valuePattern = new Regex(_variableLengthDelimiterValuePattern);
                foreach (var valuePatternMatch in variableLengthDelimiterValueMatches)
                {
                    var valueMatch = valuePattern.Match(valuePatternMatch.ToString());
                    if (valueMatch.Success == true)
                        variableLengthCustomDelimiters.Add(valueMatch.Value);
                }
            }

            return (variableLengthCustomDelimiters, delimitedInput.Replace(variableLengthDelimiterMatch.Value, string.Empty));


        }

        private readonly char[] _builtInDelimiters = { ',' };
        private readonly string _customDelimiterToken = "//";
        private readonly string _variableLengthDelimiterPattern = @"^\/\/(\[[^\[\]]+\])+";
        private readonly string _variableLengthDelimiterValuePattern = @"[^\[\]\/]+";

        private (bool Valid, IEnumerable<int> termList) ValidateTerms(IEnumerable<int> calcTerms)
        {
            var negativeTerms = new int[] { };
            var distinctNegativeTerms = new int[] { };
            var terms = calcTerms.ToArray();
            if (_configuration.AllowNegativeNumbers == false)
            {
                negativeTerms = terms.Where(t => t < 0).ToArray();
                distinctNegativeTerms = negativeTerms.Distinct().ToArray();
            }

            var validTerms = terms.Select(t => t <= _configuration.MaxNumber ? t : 0).ToArray();
            return negativeTerms.Any() ? (true, distinctNegativeTerms) : (false, validTerms);
        }

    }
}