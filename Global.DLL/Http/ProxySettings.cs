using System;
using System.Net;

namespace Global.Http
{
    /// <summary>
    /// Proxy settings used when making a request.
    /// </summary>
    public class ProxySettings
    {
        /// <summary>
        /// The IWebProxy class.
        /// </summary>
        internal WebProxy WebProxy { get; set; }
        /// <summary>
        /// Sets the proxy bypass rules. Add as many as needed.
        /// </summary>
        /// <param name="rule">The specified rule.</param>
        /// <returns>Itself.</returns>
        public ProxySettings AddBypassRule(string rule)
        {
            WebProxy.BypassArrayList.Add(rule);
            return this;
        }
        /// <summary>
        /// Flag to bypass local request.
        /// </summary>
        /// <returns>Itself.</returns>
        public ProxySettings BypassLocally()
        {
            WebProxy.BypassProxyOnLocal = true;
            return this;
        }
        /// <summary>
        /// Sets the address url.
        /// </summary>
        /// <param name="url">The specified url.</param>
        /// <returns>Itself.</returns>
        public ProxySettings Address(string url)
        {
            WebProxy.Address = new Uri(url);
            return this;
        }
        /// <summary>
        /// Sets the address uri.
        /// </summary>
        /// <param name="uri">The specified uri.</param>
        /// <returns>Itself.</returns>
        public ProxySettings Address(Uri uri)
        {
            WebProxy.Address = uri;
            return this;
        }
        /// <summary>
        /// Sets the credentials to be used in the proxy. By default it uses the NetworkCredential class.
        /// </summary>
        /// <param name="username">The specified username.</param>
        /// <param name="password">The specified password.</param>
        /// <returns>Itself.</returns>
        public ProxySettings Credentials(string username, string password)
        {
            WebProxy.Credentials = new NetworkCredential(username, password);
            return this;
        }
        /// <summary>
        /// Sets the credentials to be used in the proxy. By default it uses the NetworkCredential class.
        /// </summary>
        /// <param name="username">The specified username.</param>
        /// <param name="password">The specified password.</param>
        /// <param name="domain">The specified domain,</param>
        /// <returns>Itself.</returns>
        public ProxySettings Credentials(string username, string password, string domain)
        {
            WebProxy.Credentials = new NetworkCredential(username, password, domain);
            return this;
        }
        /// <summary>
        /// Sets the credentials to be used in the proxy.
        /// </summary>
        /// <param name="credentials">The specified credentials object.</param>
        /// <returns>Itself.</returns>
        public ProxySettings Credentials(ICredentials credentials)
        {
            WebProxy.Credentials = credentials;
            return this;
        }
    }
}