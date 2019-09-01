using System;
using System.Collections.Generic;
using System.Text;
using CalculatorService;
using CalculatorService.Primatives;

namespace Calculator
{
    class Program
    {
        private const string ExitPrompt = "Press Ctrl-C to Exit";
        private const string Prompt = "Enter numbers to add separated by a comma or newline (ex. 2,2):";

        static void Main(string[] args)
        {
            var configuration = new CalculatorConfiguration();
            var builder = new ConfigurationBuilder();
            builder.ProcessConfiguration(args, configuration);
            var calculator = new CalculationCoordinator(configuration);

            Console.WriteLine(ExitPrompt);
            while (true)
            {
                Console.WriteLine(Prompt);
                var promptResponse = Console.ReadLine();

                var result = calculator.Add(promptResponse);

                Console.WriteLine(result.Status == CalculationStatus.Ok
                    ? result.Formula
                    : result.CalculationException.Message);
            }
        }
    }
}
