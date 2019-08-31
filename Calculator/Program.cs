using System;
using CalculatorService;
using CalculatorService.Primatives;

namespace Calculator
{
    class Program
    {
        private const string Prompt = "Enter numbers to add separated by a comma or newline (ex. 2,2):";

        static void Main(string[] args)
        {
            Console.WriteLine(Prompt);
            var promptResponse = Console.ReadLine();

            var calculator = new CalculationCoordinator();
            var result = calculator.Add(promptResponse);

            if (result.Status == CalculationStatus.Ok)
                Console.WriteLine(result.Sum);
            else
            {
                Console.WriteLine(result.CalculationException.Message);
            }
        }
    }
}
