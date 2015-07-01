using System;

namespace Global.Http
{
    ///<summary>
    /// Helper class to build a url string.
    ///</summary>
    public class UrlBuilder
    {
        private string _baseUrl;
        private UrlParameters _urlParameters;

        ///<summary>
        /// Defines the base url.
        ///</summary>
        ///<param name="baseUrl">The base url.</param>
        ///<returns>The same instance of the UrlBuilder.</returns>
        public UrlBuilder BaseUrl(string baseUrl)
        {
            _baseUrl = baseUrl;
            return this;
        }

        ///<summary>
        /// Defines the url Parameters.
        ///</summary>
        ///<param name="urlParameters">The url Parameters.</param>
        ///<returns>The same instance of the UrlBuilder.</returns>
        public UrlBuilder Parameters(Action<UrlParameters> urlParameters)
        {
            urlParameters(_urlParameters ?? (_urlParameters = new UrlParameters()));
            return this;
        }

        ///<summary>
        /// Builds the the final url.
        ///</summary>
        ///<returns>The final url string.</returns>
        public string Build()
        {
            string url = _baseUrl;

            if (_urlParameters != null && _urlParameters.Parameters != null && _urlParameters.Parameters.Count > 0)
            {
                url = String.Format(_baseUrl.Contains("?") ? "{0}&" : "{0}?", url);
                foreach (var parameter in _urlParameters.Parameters)
                {
                    url = String.Format("{0}{1}={2}&", url, parameter.Key, parameter.Value);
                }
            }

            //if (!_baseUrl.EndsWith("?") && _urlParameters != null && _urlParameters.Parameters != null && _urlParameters.Parameters.Count > 0)
            //{
            //    url = String.Format("{0}?", url);
            //    foreach (var parameter in _urlParameters.Parameters)
            //    {
            //        url = String.Format("{0}{1}={2}&", url, parameter.Key, parameter.Value);
            //    }
            //}

            // Remove last "&" if it exists
            return (url.EndsWith("&")) ? url.Remove((url.Length - 1), 1) : url;
        }
    }
}