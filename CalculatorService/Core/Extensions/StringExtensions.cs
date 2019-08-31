namespace CalculatorService.Core.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt(this string number)
        {
            return int.TryParse(number, out var intValue) ? intValue : 0;
        }
    }
}