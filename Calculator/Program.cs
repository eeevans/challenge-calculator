using CalculatorService;
using CalculatorService.Contracts;
using CalculatorService.Primatives;
using Lamar;
using System;
using System.Xml.Xsl;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator
{
    class Program
    {
        private const string ExitPrompt = "Press Ctrl-C to Exit";
        private const string Prompt = "Enter numbers to add separated by a comma or {0} (ex. 2,2):";

        static void Main(string[] args)
        {
            IContainer container = new CalculatorBootstrapper().Configure();
            var configuration = container.GetInstance<ICalculatorConfiguration>();
            var builder = container.GetInstance<IConfigurationBuilder>();
            if (builder.ProcessConfiguration(args, configuration) == false)
                return;
            container.Configure(c => c.AddSingleton<ICalculatorConfiguration>(configuration));
            var calculator = container.GetInstance<ICalculationCoordinator>();

            Console.WriteLine(ExitPrompt);
            while (true)
            {
                Console.WriteLine(string.Format(Prompt, configuration.AlternateDelimiter == '\n' ? "newline" : $"'{configuration.AlternateDelimiter}'"));
                var promptResponse = Console.ReadLine();

                var result = calculator.Calculate(promptResponse);

                Console.WriteLine(result.Status == CalculationStatus.Ok
                    ? result.Formula
                    : result.CalculationException.Message);
            }
        }
    }
}
