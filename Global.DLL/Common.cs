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
            return Random(size, false);
        }

        /// <summary>
        /// Generates a sequence of random strings based on the size required.
        /// </summary>
        /// <param name="size">The size of the string.</param>
        /// <param name="toLower">Specified if the returned string is set to upper or not.</param>
        /// <returns>A random string.</returns>
        public static string Random(int size, bool toLower)
        {
            var builder = new StringBuilder();
            
            // This is a workaround because if a random number with the same length is asked one after the other,
            // the result would be the same. This way we guarantee that the number it's different everytime
            System.Threading.Thread.Sleep(1);

            var random = new Random((int)DateTime.Now.Ticks);
            for (var i = 0; i < size; i++)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))));
            }
            return (toLower) ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
