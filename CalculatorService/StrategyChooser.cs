using System;
using System.Collections.Generic;
using CalculatorService.Contracts;

namespace CalculatorService
{
    class StrategyChooser<T> : IStrategyChooser<T>
    {
        private readonly IEnumerable<T> _strategies;

        public StrategyChooser(IEnumerable<T> strategies)
        {
            _strategies = strategies;
        }

        public T Choose(Func<T, bool> criteria)
        {
            foreach (var strategy in _strategies)
            {
                if (criteria(strategy))
                    return strategy;
            }
            throw new InvalidOperationException("Strategy for operation type missing!");
        }
    }
}