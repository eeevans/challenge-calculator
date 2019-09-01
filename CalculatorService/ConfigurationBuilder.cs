using System;
using System.Collections.Generic;
using CalculatorService.Contracts;

namespace CalculatorService
{
    public interface IConfigurationBuilder
    {
        void ProcessConfiguration(IEnumerable<string> args, ICalculatorConfiguration config);
    }

    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private Dictionary<string, Action<ICalculatorConfiguration, string>> _configStrategies = new Dictionary<string, Action<ICalculatorConfiguration, string>>()
        {
            {"-DenyNegativeNumbers", (c,s) =>  c.AllowNegativeNumbers = !bool.Parse(s)  },
            {"-AlternateDelimiter", (c,s) =>  c.AlternateDelimiter = char.Parse(s)  },
            {"-MaxNumber", (c,s) =>  c.MaxNumber = int.Parse(s)  }
        };

        public void ProcessConfiguration(IEnumerable<string> args, ICalculatorConfiguration config)
        {
            foreach (var arg in args)
            {
                var option = arg.Split("=");
                if (option.Length > 1)
                {
                    if (_configStrategies.ContainsKey(option[0]))
                    {
                        _configStrategies[option[0]](config, option[1]);
                    }
                }
            }

        }
    }
}