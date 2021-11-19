namespace Electronic_Medical_Record.Helpers
{
    public static class StringHelpers
    {
        /// <summary>
        /// Add 's' char to the end of a string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Modified string.</returns>
        public static string MakePlular(this string input)
        {
            return input + "s";
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
