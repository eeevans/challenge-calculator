namespace CalculatorService.Contracts
{
    public interface ICalculatorConfiguration
    {
        char AlternateDelimiter { get; set; }
        bool AllowNegativeNumbers { get; set; }
        int MaxNumber { get; set; }
    }
}