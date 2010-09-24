using System;

namespace Global.Global
{
    ///<summary>
    /// Performs actions on and with enumerators.
    ///</summary>
    public class EnumHelper
    {
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
    }
}
