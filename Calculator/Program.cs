using System;
using System.Collections.Generic;
using System.Text;
using CalculatorService;
using CalculatorService.Contracts;
using CalculatorService.Primatives;
using StructureMap;

namespace Calculator
{
    class Program
    {
        private const string ExitPrompt = "Press Ctrl-C to Exit";
        private const string Prompt = "Enter numbers to add separated by a comma or newline (ex. 2,2):";

        static void Main(string[] args)
        {
            IContainer container = new CalculatorBootstrapper().Configure();
            var configuration = container.GetInstance<ICalculatorConfiguration>();
            var builder = container.GetInstance<IConfigurationBuilder>();
            if (builder.ProcessConfiguration(args, configuration) == false)
                return;

            var calculator = container.With<ICalculatorConfiguration>(configuration)
                    .GetInstance<ICalculationCoordinator>();

            Console.WriteLine(ExitPrompt);
            while (true)
            {
                Console.WriteLine(Prompt);
                var promptResponse = Console.ReadLine();

                var result = calculator.Calculate(promptResponse);

                Console.WriteLine(result.Status == CalculationStatus.Ok
                    ? result.Formula
                    : result.CalculationException.Message);
            }
        }
    }
}
