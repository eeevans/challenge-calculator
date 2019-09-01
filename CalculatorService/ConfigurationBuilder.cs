using System;
using System.Collections.Generic;
using CalculatorService.Contracts;

namespace CalculatorService
{
    public interface IConfigurationBuilder
    {
        bool ProcessConfiguration(IEnumerable<string> args, ICalculatorConfiguration config);
    }

    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private Dictionary<string, Action<ICalculatorConfiguration, string>> _configStrategies = new Dictionary<string, Action<ICalculatorConfiguration, string>>()
        {
            {"-denynegativenumbers", (c,s) =>  c.AllowNegativeNumbers = !bool.Parse(s)  },
            {"-alternatedelimiter", (c,s) =>  c.AlternateDelimiter = char.Parse(s)  },
            {"-maxnumber", (c,s) =>  c.MaxNumber = int.Parse(s)  }
        };

        public bool ProcessConfiguration(IEnumerable<string> args, ICalculatorConfiguration config)
        {
            foreach (var arg in args)
            {
                if (arg.ToLower().Contains("help"))
                {
                    ShowHelp();
                    return false;
                }

                var option = arg.Split("=");
                if (option.Length > 1)
                {
                    if (_configStrategies.ContainsKey(option[0].ToLower()))
                    {
                        _configStrategies[option[0]](config, option[1]);
                    }
                }
            }

            return true;
        }

        private void ShowHelp()
        {
            Console.WriteLine(@"
Usage: Calculator [-DenyNegativeNumbers=(true,false)] [-AlternateDelimiter=(character)] [-MaxNumber=(number)]
    Optional Parameters:
        DenyNegativeNumbers     Specifies whether to deny negative numbers or allow them.

        AlternateDelimiter      Specifies a character to use as a delimiter in addition to a comma.

        MaxNumber               Specifies the maximum number allowed as input.");
        }
    }
}