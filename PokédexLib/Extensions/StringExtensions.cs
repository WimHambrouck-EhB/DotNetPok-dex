using System;
using System.Linq;

namespace PokédexLib.Extensions
{
    public static class StringExtensions
    {
        public static string FormatAsName(this string invoer)
        {
            if (invoer == null)
                throw new ArgumentNullException(nameof(invoer));

            if (string.IsNullOrWhiteSpace(invoer))
                return invoer;

            return invoer.First().ToString().ToUpper() + invoer.Substring(1);
        }
    }
}
