using System.Collections.Generic;

namespace Global.Http
{
    ///<summary>
    /// A collection of url Parameters.
    ///</summary>
    public class UrlParameters
    {
        ///<summary>
        /// A dictionary os parameter name/value pair.
        ///</summary>
        public Dictionary<string, string> Parameters = new Dictionary<string, string>();

        ///<summary>
        /// Adds a new querystring parameter to the Parameters collection.
        ///</summary>
        ///<param name="name">The name of parameter.</param>
        ///<param name="value">The value of the parameter value.</param>
        ///<returns>The same instance of the parameter value.</returns>
        public UrlParameters Add(string name, object value)
        {
            return Add(name, value, true);
        }

        ///<summary>
        /// Adds a new querystring parameter to the Parameters collection.
        ///</summary>
        ///<param name="name">The name of parameter.</param>
        ///<param name="value">The value of the parameter value.</param>
        ///<param name="allowNulls">Specifies if the it will allow null values. If this value is false and the value provided is null then and empty string will be used.</param>
        ///<returns>The same instance of the parameter value.</returns>
        public UrlParameters Add(string name, object value, bool allowNulls)
        {
            if (allowNulls) Parameters.Add(name, value != null ? value.ToString() : string.Empty);
            else if (value != null) Parameters.Add(name, value.ToString());

            return this;
        }

    }
}