using System;
using System.IO;
using System.Net;
using System.Text;

namespace Global.Http
{
    /// <summary>
    /// Send http requests using a fluent api.
    /// </summary>
    public class FluentHttp
    {
        private string _url;
        private string _method;
        private string _postData;
        private Encoding _postDataEncoding;
        private string _contentType;
        private string _userAgent;
        private bool? _keepAlive;
        private int? _timeout;
        private Encoding _encoding;
        private ICredentials _credentials;
        private IWebProxy _webProxy;
        private WebHeaderCollection _headers;

        /// <summary>
        /// Set the url to request to.
        /// </summary>
        /// <param name="url">The specified url.</param>
        /// <returns></returns>
        public FluentHttp Url(string url)
        {
            _url = url;
            return this;
        }
        /// <summary>
        /// Sets the method.
        /// </summary>
        /// <param name="method">The specified method.</param>
        /// <returns></returns>
        public FluentHttp Method(string method)
        {
            _method = method;
            return this;
        }
        /// <summary>
        /// Sets the post data if its a POST request.
        /// </summary>
        /// <param name="postData">The specified post data.</param>
        /// <returns></returns>
        public FluentHttp PostData(string postData)
        {
            _postData = postData;
            return this;
        }
        /// <summary>
        /// Sets the post data encoding.
        /// </summary>
        /// <param name="postDataEncoding">The specified post data encoding.</param>
        /// <returns></returns>
        public FluentHttp PostDataEncoding(Encoding postDataEncoding)
        {
            _postDataEncoding = postDataEncoding;
            return this;
        }
        /// <summary>
        /// Sets the request content type.
        /// </summary>
        /// <param name="contentType">The sepecified content type.</param>
        /// <returns></returns>
        public FluentHttp ContentType(string contentType)
        {
            _contentType = contentType;
            return this;
        }
        /// <summary>
        /// Sets the request user agent.
        /// </summary>
        /// <param name="userAgent">The specified user agent.</param>
        /// <returns></returns>
        public FluentHttp UserAgent(string userAgent)
        {
            _userAgent = userAgent;
            return this;
        }
        /// <summary>
        /// Sets the request user agent.
        /// </summary>
        /// <param name="keepAlive">The specified user agent.</param>
        /// <returns>Itself.</returns>
        public FluentHttp KeepAlive(bool keepAlive)
        {
            _keepAlive = keepAlive;
            return this;
        }
        /// <summary>
        /// Sets the request timeout.
        /// </summary>
        /// <param name="timeout">The specified timeout.</param>
        /// <returns>Itself.</returns>
        public FluentHttp Timeout(int timeout)
        {
            _timeout = timeout;
            return this;
        }
        /// <summary>
        /// Sets the encoding for decoding the response stream.
        /// </summary>
        /// <param name="encoding">The specified encoding.</param>
        /// <returns>Itself.</returns>
        public FluentHttp Encoding(Encoding encoding)
        {
            _encoding = encoding;
            return this;
        }
        /// <summary>
        /// Sets the headers to be used in the request.
        /// </summary>
        /// <param name="headers">Add the headers.</param>
        /// <returns>Itself.</returns>
        public FluentHttp Headers(Func<AddHeaders, AddHeaders> headers)
        {
            _headers = headers(new AddHeaders()).Collection;
            return this;
        }
        /// <summary>
        /// Sets the credentials to be used in the request. By default it uses the NetworkCredential class.
        /// </summary>
        /// <param name="username">The specified username.</param>
        /// <param name="password">The specified password.</param>
        /// <returns>Itself.</returns>
        public FluentHttp Credentials(string username, string password)
        {
            _credentials = new NetworkCredential(username, password);
            return this;
        }
        /// <summary>
        /// Sets the credentials to be used in the request. By default it uses the NetworkCredential class.
        /// </summary>
        /// <param name="username">The specified username.</param>
        /// <param name="password">The specified password.</param>
        /// <param name="domain">The specified domain,</param>
        /// <returns>Itself.</returns>
        public FluentHttp Credentials(string username, string password, string domain)
        {
            _credentials = new NetworkCredential(username, password, domain);
            return this;
        }
        /// <summary>
        /// Sets the credentials to be used in the request.
        /// </summary>
        /// <param name="credentials">The specified credentials object.</param>
        /// <returns>Itself.</returns>
        public FluentHttp Credentials(ICredentials credentials)
        {
            _credentials = credentials;
            return this;
        }
        /// <summary>
        /// Configures a proxy to be used in the request.
        /// </summary>
        /// <param name="webProxy">Set specific proxy configuration.</param>
        /// <returns>Itself.</returns>
        public FluentHttp Proxy(Func<ProxySettings, ProxySettings> webProxy)
        {
            _webProxy = webProxy(new ProxySettings()).WebProxy;
            return this;
        }
        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <returns>A string containing the response.</returns>
        public string Send()
        {
            if (!String.IsNullOrEmpty(_url))
            {
                HttpWebRequest webReq;
                HttpWebResponse webResp = null;

                try
                {
                    // Prepare web request...
                    webReq = (HttpWebRequest)WebRequest.Create(_url);
                    webReq.Method = _method;

                    if (_headers != null) webReq.Headers = _headers;
                    if (_credentials != null) webReq.Credentials = _credentials;
                    if (_webProxy != null) webReq.Proxy = _webProxy;
                    if (_timeout != null) webReq.Timeout = (int)_timeout;
                    webReq.ContentType = !String.IsNullOrEmpty(_contentType) ? _contentType : "text/xml";
                    if (!String.IsNullOrEmpty(_userAgent)) webReq.UserAgent = _userAgent;
                    if (_keepAlive != null) webReq.KeepAlive = (bool)_keepAlive;

                    if (_method.ToUpper().Equals("POST") || _method.ToUpper().Equals("PUT"))
                    {
                        var encoding = _postDataEncoding ?? new ASCIIEncoding();
                        byte[] data = !String.IsNullOrEmpty(_postData)
                                          ? encoding.GetBytes(_postData)
                                          : encoding.GetBytes(String.Empty);
                        webReq.ContentLength = data.Length;
                    }

                    //requests the data
                    webResp = (HttpWebResponse)webReq.GetResponse();

                    string response;
                    // Get the stream associated with the response.
                    using (var receiveStream = webResp.GetResponseStream())
                    {
                        // Pipes the stream to a higher level stream reader with the required encoding format.
                        using (var readStream = new StreamReader(receiveStream, _encoding))
                            response = readStream.ReadToEnd();
                    }

                    return response;
                }
                finally
                {
                    // Close web response
                    if (webResp != null) webResp.Close();
                }
            }

            throw new Exception("Must specify a valid Url.");
        }

        /// <summary>
        /// Sets the headers to be used in the request.
        /// </summary>
        public class AddHeaders
        {
            private WebHeaderCollection _collection;
            /// <summary>
            /// The header collection.
            /// </summary>
            internal WebHeaderCollection Collection
            {
                get
                {
                    return _collection ?? (_collection = new WebHeaderCollection());
                }
            }
            /// <summary>
            /// Sets the request header. Add as many as needed.
            /// </summary>
            /// <param name="header">The header.</param>
            /// <returns>Itself.</returns>
            public AddHeaders AddRequestHeader(string header)
            {
                Collection.Add(header);
                return this;
            }
            /// <summary>
            /// Sets the request header. Add as many as needed.
            /// </summary>
            /// <param name="name">The header name.</param>
            /// <param name="value">The header value.</param>
            /// <returns>Itself.</returns>
            public AddHeaders AddRequestHeader(string name, string value)
            {
                Collection.Add(name, value);
                return this;
            }
            /// <summary>
            /// Sets the request header. Add as many as needed.
            /// </summary>
            /// <param name="header">The header name.</param>
            /// <param name="value">The header value.</param>
            /// <returns>Itself.</returns>
            public AddHeaders AddRequestHeader(HttpRequestHeader header, string value)
            {
                Collection.Add(header, value);
                return this;
            }
            /// <summary>
            /// Sets the response headers. Add as many as neesed.
            /// </summary>
            /// <param name="header">The header name.</param>
            /// <param name="value">The header value.</param>
            /// <returns>Itself.</returns>
            public AddHeaders AddResponseHeader(HttpResponseHeader header, string value)
            {
                Collection.Add(header, value);
                return this;
            }
        }

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
}