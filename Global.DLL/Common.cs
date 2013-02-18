using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Converts the index value into the corresponding enum value.
        /// </summary>
        /// <typeparam name="T">The enum object.</typeparam>
        /// <param name="number">The index of the enum value.</param>
        /// <returns>The enum object value.</returns>
        public static T NumToEnum<T>(int number)
        {
            return (T)Enum.ToObject(typeof(T), number);
        }

        /// <summary>
        /// Converts the string value into the corresponding enum value.
        /// </summary>
        /// <typeparam name="T">The enum object.</typeparam>
        /// <param name="value">The string value of the corresponding enum value.</param>
        /// <returns>The enum object value.</returns>
        public static T StringToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Converts the string value into the corresponding enum value.
        /// </summary>
        /// <typeparam name="T">The enum object.</typeparam>
        /// <param name="value">The string value of the corresponding enum value.</param>
        /// <param name="ignoreCase">Flag to choose to ignore case or not.</param>
        /// <returns>The enum object value.</returns>
        public static T StringToEnum<T>(string value, bool ignoreCase)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        /// <summary>
        /// Verifies if the list if null or empty.
        /// </summary>
        /// <typeparam name="TDocument">The generic time document.</typeparam>
        /// <param name="list">The list to validate.</param>
        /// <returns>A boolean indicating if the list is valid or not.</returns>
        public static bool IsNullOrEmpty<TDocument>(this IEnumerable<TDocument> list)
        {
            return list == null || list.Count() <= 0;
        }
        /// <summary>
        /// Converts from unix time to C#'s DateTime format.
        /// </summary>
        /// <param name="unixTime">The unix time.</param>
        /// <returns>A DateTime representation of the unix time.</returns>
        public static DateTime FromUnixTime(this long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }
        /// <summary>
        /// Converts from C#'s DateTime format to unix time.
        /// </summary>
        /// <param name="date">The DateTime in C# format.</param>
        /// <returns>A long with the converted unix time.</returns>
        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
        /// <summary>
        /// Adds functionality to insert a skip and take operations on the returned queryable list.
        /// </summary>
        /// <typeparam name="TDocument">The type of document.</typeparam>
        /// <param name="queryable">The current queryable object.</param>
        /// <param name="skip">The skip value.</param>
        /// <param name="take">The take value.</param>
        /// <returns>A list of the document type object.</returns>
        public static List<TDocument> ToList<TDocument>(this IQueryable<TDocument> queryable, int skip, int take)
        {
            if (skip > 0) queryable = queryable.Skip(skip);
            if (take > 0) queryable = queryable.Take(take);
            return queryable.ToList();
        }
    }
}
