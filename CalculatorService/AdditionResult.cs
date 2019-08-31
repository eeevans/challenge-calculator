using System;
using CalculatorService.Primatives;

namespace CalculatorService
{
    public class AdditionResult
    {
        public AdditionResult(int sum)
        {
            Status = CalculationStatus.Ok;
            Sum = sum;
        }

        public AdditionResult(Exception e)
        {
            Status = CalculationStatus.Error;
            CalculationException = e;
        }

        public int Sum { get; set; }
        public CalculationStatus Status { get; set; }
        public Exception CalculationException { get; set; }
    }
}