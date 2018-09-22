using System;
using System.Text.RegularExpressions;

namespace NetCoreConsoleDemo
{
    public static class RandomGeneratorExtension
    {
        public static string GenerateRandomId()
        {
            var rn = new Random();
            var charsToUse = "AzByCxDwEvFuGtHsIrJqKpLoMnNmOlPkQjRiShTgUfVeWdXcYbZa1234567890";

            MatchEvaluator RandomChar = m => charsToUse[rn.Next(charsToUse.Length)].ToString();
            return Regex.Replace("XXXX-XXXXX", "X", RandomChar);
        }
    }
}