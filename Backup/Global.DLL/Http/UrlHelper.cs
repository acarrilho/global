using System;

namespace Global.Http
{
    ///<summary>
    /// Defines costum url related actions.
    ///</summary>
    public class UrlHelper
    {
        /// <summary>
        /// Adds a "/" at the end of the url if it does not exist.
        /// </summary>
        /// <param name="url">The specified url.</param>
        /// <returns></returns>
        public static string ValidateUrlEndSlash(string url)
        {
            return !url.EndsWith(@"/") ? String.Concat(url, "/") : url;
        }
    }
}
