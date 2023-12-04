using System;
using System.Collections.Generic;
using System.Text;

namespace PokédexLibrary.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// PokéAPI adds escaped characters to longer texts, this extension method removes them.
        /// </summary>
        public static string Unescape(this string str)
        {
            return str.Replace(Environment.NewLine, " ").Replace('\n', ' ').Replace('\f', ' ');
        }
    }
}
