using System;

namespace CalculatorService.Contracts
{
    public interface IStrategyChooser<T>
    {
        T Choose(Func<T, bool> criteria);
    }
}