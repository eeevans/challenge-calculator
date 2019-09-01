using System;
using CalculatorService.Primatives;

namespace CalculatorService
{
    public class AdditionResult
    {
        public AdditionResult(int sum, string formula)
        {
            Status = CalculationStatus.Ok;
            Sum = sum;
            Formula = formula;
        }

        public AdditionResult(Exception e)
        {
            Status = CalculationStatus.Error;
            CalculationException = e;
        }

        public string Formula { get; }

        public int Sum { get; }
        public CalculationStatus Status { get; }
        public Exception CalculationException { get; }
    }
}