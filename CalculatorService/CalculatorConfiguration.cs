using CalculatorService.Contracts;

namespace CalculatorService
{
    public class CalculatorConfiguration : ICalculatorConfiguration
    {
        public CalculatorConfiguration(char alternateDelimiter, bool allowNegativeNumbers, int maxNumber)
        {
            AlternateDelimiter = alternateDelimiter;
            AllowNegativeNumbers = allowNegativeNumbers;
            MaxNumber = maxNumber;
        }

        public CalculatorConfiguration() :
            this('\n', false, 1000)
        {

        }

        public char AlternateDelimiter { get; set; }
        public bool AllowNegativeNumbers { get; set; }
        public int MaxNumber { get; set; }
    }
}