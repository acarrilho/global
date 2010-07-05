using System;
using System.Text;
using System.IO;
using System.Net;

namespace Global.Http
{
    /// <summary>
    /// Returns the request type to be made.
    /// </summary>
    public enum HttpRequestType
    {
        ///<summary>
        /// Makes a POST request.
        ///</summary>
        POST,
        ///<summary>
        /// Makes a GET request.
        ///</summary>
        GET
    }

    ///<summary>
    /// Performs some http related actions like GET and POST requests.
    ///</summary>
    public class HttpHelper
    {
        /// <summary>
        /// Sets a collection of header values to be sent in the request.
        /// </summary>
        public WebHeaderCollection WebHeaderCollection { get; set; }

        private Encoding _encoding;
        /// <summary>
        /// Returns the encoding type of the web request. The default is UTF-8.
        /// </summary>
        public Encoding Encoding
        {
            get
            {
                return _encoding ?? (_encoding = Encoding.UTF8);
            }
        }

        /// <summary>
        /// Sends a POST request to the specified url.
        /// </summary>
        /// <param name="url">The request url.</param>
        /// <param name="postData">The data to be sent.</param>
        /// <returns>A string containing a response from the request Url.</returns>
        public string SendPost(string url, string postData)
        {
            return SendPost(url, postData, null);
        }

        /// <summary>
        /// Sends a POST request to the specified url.
        /// </summary>
        /// <param name="url">The request url.</param>
        /// <param name="postData">The data to be sent.</param>
        /// <param name="contentType">The request content type. The default is 'application/x-www-form-urlencoded'.</param>
        /// <returns>A string containing a response from the request Url.</returns>
        public string SendPost(string url, string postData, string contentType)
        {
            return SendPost(url, postData, contentType, null);
        }

        /// <summary>
        /// Sends a POST request to the specified url.
        /// </summary>
        /// <param name="url">The request url.</param>
        /// <param name="postData">The data to be sent.</param>
        /// <param name="contentType">The request content type. The default is 'application/x-www-form-urlencoded'.</param>
        /// <param name="credentials">The credentials to be used while making the request.</param>
        /// <returns>A string containing a response from the request Url.</returns>
        public string SendPost(string url, string postData, string contentType, ICredentials credentials)
        {
            return SendRequest(url, postData, HttpRequestType.POST, contentType, null, null, credentials, null, null, Encoding);
        }

        /// <summary>
        /// Sends a GET request to the specified url.
        /// </summary>
        /// <param name="url">The request url.</param>
        /// <returns>A string containing a response from the request Url.</returns>
        public string SendGet(string url)
        {
            return SendGet(url, null);
        }

        /// <summary>
        /// Sends a GET request to the specified url.
        /// </summary>
        /// <param name="url">The request url.</param>
        /// <param name="contentType">The request content type. The default is 'application/x-www-form-urlencoded'.</param>
        /// <returns>A string containing a response from the request Url.</returns>
        public string SendGet(string url, string contentType)
        {
            return SendGet(url, contentType, null);
        }

        /// <summary>
        /// Sends a GET request to the specified url.
        /// </summary>
        /// <param name="url">The request url.</param>
        /// <param name="contentType">The request content type. The default is 'application/x-www-form-urlencoded'.</param>
        /// <param name="credentials">The credentials to be used while making the request.</param>
        /// <returns>A string containing a response from the request Url.</returns>
        public string SendGet(string url, string contentType, ICredentials credentials)
        {
            return SendRequest(url, null, HttpRequestType.GET, contentType, null, null, credentials, null, null, Encoding);
        }

        /// <summary>
        /// Sends a request to the specified url.
        /// </summary>
        /// <param name="url">The request url.</param>
        /// <param name="postData">The data to be sent.</param>
        /// <param name="requestType">The request type.</param>
        /// <param name="contentType">The request content type. The default is 'application/x-www-form-urlencoded'.</param>
        /// <param name="userAgent">The request user agent.</param>
        /// <param name="keepAlive">Indicates whether the connection must be persisted.</param>
        /// <param name="credentials">The credentials to be used while making the request.</param>
        /// <param name="proxy">Provide if the connection uses a proxy.</param>
        /// <param name="timeout">The connection timeout.</param>
        /// <param name="encoding">Set the response encoding.</param>
        /// <returns>A string containing a response from the requested Url.</returns>
        public string SendRequest(string url, string postData, HttpRequestType requestType, string contentType,
            string userAgent, bool? keepAlive, ICredentials credentials, IWebProxy proxy, int? timeout,
            Encoding encoding)
        {
            if (!String.IsNullOrEmpty(url))
            {
                HttpWebRequest webReq;
                HttpWebResponse webResp = null;

                try
                {
                    // Prepare web request...
                    webReq = (HttpWebRequest)WebRequest.Create(url);
                    webReq.Method = requestType.ToString();

                    if (WebHeaderCollection != null) webReq.Headers = WebHeaderCollection;
                    if (credentials != null) webReq.Credentials = credentials;
                    if (proxy != null) webReq.Proxy = proxy;
                    if (timeout != null) webReq.Timeout = (int)timeout;
                    webReq.ContentType = !String.IsNullOrEmpty(contentType)
                                             ? contentType
                                             : "application/x-www-form-urlencoded";
                    if (!String.IsNullOrEmpty(userAgent)) webReq.UserAgent = userAgent;
                    if (keepAlive != null) webReq.KeepAlive = (bool)keepAlive;

                    var ascii = new ASCIIEncoding();
                    byte[] data = !String.IsNullOrEmpty(postData) ? ascii.GetBytes(postData) : ascii.GetBytes(String.Empty);
                    webReq.ContentLength = data.Length;

                    //requests the data
                    webResp = (HttpWebResponse)webReq.GetResponse();

                    string response;
                    // Get the stream associated with the response.
                    using (var receiveStream = webResp.GetResponseStream())
                    {
                        // Pipes the stream to a higher level stream reader with the required encoding format.
                        using (var readStream = new StreamReader(receiveStream, encoding))
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
        /// Downloads a file via http.
        /// </summary>
        /// <param name="sourceUrl">The url where the source file resides.</param>
        /// <param name="destinationPath">The local destination path where the file will be stored.</param>
        /// <returns>A boolean indicating if the download was successful.</returns>
        public bool DownloadFile(string sourceUrl, string destinationPath)
        {
            var webClient = new WebClient();
            webClient.DownloadFile(sourceUrl, destinationPath);

            return true;
        }
    }
}

