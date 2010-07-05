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
        public UrlParameters Add(string name, string value)
        {
            Parameters.Add(name, value);
            return this;
        }

    }
}