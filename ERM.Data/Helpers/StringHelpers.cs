using System;
using System.Security.Cryptography;
using System.Text;

namespace EMR.Data.Helpers
{
    public static class StringHelpers
    {
        /// <summary>
        /// Add 't' char to first position of a string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Modified string.</returns>
        public static string ConvertToTableName(this string input)
        {
            return $"t{input}";
        }

        /// <summary>
        /// Make the first letter in a string uppercase.
        /// </summary>
        /// <param name="input">Iinput string.</param>
        /// <returns>Modified string.</returns>
        public static string MakeFirstCharUppercase(this string input) =>
            input switch
            {
                null => input,
                "" => input,
                _ => input[0].ToString().ToUpper() + input[1..]
            };
    }
}
