using System;
using System.Linq;

namespace PokédexLib.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Maakt van het eerste karakter in een string een hoofdletter.
        /// </summary>
        /// <param name="invoer">De aan te passen string.</param>
        /// <returns>String waarvan het eerste karater een hoofdletter is. A</returns>
        /// <exception cref="ArgumentException">Wordt gegooid als <paramref name="invoer"/> null is.</exception>
        public static string FirstToUpper(this string invoer)
        {
            if (invoer == null)
                throw new ArgumentNullException(nameof(invoer));

            if (string.IsNullOrWhiteSpace(invoer))
                return invoer;

            return invoer.First().ToString().ToUpper() + invoer.Substring(1);
        }
    }
}
