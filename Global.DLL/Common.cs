using System;
using System.Text;

namespace Global
{
    /// <summary>
    /// Common tasks common to every project (including this one).
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Generates a sequence of random strings based on the size required.
        /// </summary>
        /// <param name="size">The size of the string.</param>
        /// <returns>A random string.</returns>
        public static string Random(int size)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))));
            }
            return builder.ToString();
        }
    }
}
