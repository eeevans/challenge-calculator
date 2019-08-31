using System;
using CalculatorService;
using CalculatorService.Primatives;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter two numbers to add separated by a comma (ex. 2,2):");
            var promptResponse = Console.ReadLine();

            var calculator = new CalculationCoordinator();
            var result = calculator.Add(promptResponse);

            if (result.Status == CalculationStatus.Ok)
                Console.WriteLine(result.Sum);
            else
            {
                Console.WriteLine(result.CalculationException);
            }
        }
    }
}
