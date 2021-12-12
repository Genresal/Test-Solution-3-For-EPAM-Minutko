using System;
using System.Security.Cryptography;
using System.Text;

namespace EMR.Business.Helpers
{
    public static class StringHelpers
    {
        public static string HashString(this string input)
        {
            byte[] data = Encoding.Default.GetBytes(input);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(data);
            return Convert.ToBase64String(result);
        }
    }
}
