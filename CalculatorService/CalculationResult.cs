using CalculatorService.Primatives;
using System;

namespace CalculatorService
{
    public class CalculationResult
    {
        public CalculationResult(int answer, string formula)
        {
            Status = CalculationStatus.Ok;
            Answer = answer;
            Formula = formula;
        }

        public CalculationResult(Exception e)
        {
            Status = CalculationStatus.Error;
            CalculationException = e;
        }

        public string Formula { get; }

        public int Answer { get; }
        public CalculationStatus Status { get; }
        public Exception CalculationException { get; }
    }
}