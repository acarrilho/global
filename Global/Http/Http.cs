using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using Global.Serialization;

namespace Global.Http
{
    /// <summary>
    /// Handles http requests.
    /// </summary>
    public class Http
    {
        /// <summary>
        /// Indicates the status of the request after it finished to help catch non 200 http responses.
        /// </summary>
        public HttpResponseMessage Response { get; set; }

        /// <summary>
        /// Specifies the transfer-encoding header.
        /// </summary>
        public string TransferEncoding { get { return WebReq.TransferEncoding; } set { WebReq.TransferEncoding = value; } }
        /// <summary>
        /// Specifies the transfer-encoding header.
        /// </summary>
        /// <param name="transferEncoding">The header value.</param>
        /// <returns>Itself.</returns>
        public Http SetTranferEncoding(string transferEncoding)
        {
            if (!string.IsNullOrEmpty(transferEncoding)) TransferEncoding = transferEncoding;
            return this;
        }

        /// <summary>
        /// Specifies the referer header.
        /// </summary>
        public string Referer { get { return WebReq.Referer; } set { WebReq.Referer = value; } }
        /// <summary>
        /// Specifies the referer header.
        /// </summary>
        /// <param name="referer">The header value.</param>
        /// <returns>Itself.</returns>
        public Http SetReferer(string referer)
        {
            if (!string.IsNullOrEmpty(referer)) Referer = referer;
            return this;
        }

        /// <summary>
        /// Specifies the connection limit.
        /// </summary>
        public int ConnectionLimit { get { return WebReq.ServicePoint.ConnectionLimit; } set { WebReq.ServicePoint.ConnectionLimit = value; } }
        /// <summary>
        /// Specifies the connection limit.
        /// </summary>
        /// <param name="limit">The connection limit value.</param>
        /// <returns>Itself.</returns>
        public Http SetConnectionLimit(int limit)
        {
            ConnectionLimit = limit;
            return this;
        }

        /// <summary>
        /// Specifies the range header.
        /// </summary>
        public int Range { set { WebReq.AddRange(value); } }
        /// <summary>
        /// Specifies the range header.
        /// </summary>
        /// <param name="range">The header value.</param>
        /// <returns>Itself.</returns>
        public Http SetRange(int range)
        {
            WebReq.AddRange(range);
            return this;
        }
        /// <summary>
        /// Specifies the range header.
        /// </summary>
        public int[] RangeFromTo { set { if (value.Length == 2) WebReq.AddRange(value[0], value[1]); } }
        /// <summary>
        /// Specifies the range header.
        /// </summary>
        /// <param name="from">The start range value.</param>
        /// <param name="to">The end range value.</param>
        /// <returns>Itself.</returns>
        public Http SetRange(int from, int to)
        {
            WebReq.AddRange(from, to);
            return this;
        }

        /// <summary>
        /// Specifies th if modified since header.
        /// </summary>
        public DateTime IfModifiedSince { get { return WebReq.IfModifiedSince; } set { WebReq.IfModifiedSince = value; } }
        /// <summary>
        /// Specifies th if modified since header.
        /// </summary>
        /// <param name="ifModifiedSince">The if modified since value.</param>
        /// <returns>Itself.</returns>
        public Http SetIfModifiedSince(DateTime ifModifiedSince)
        {
            IfModifiedSince = ifModifiedSince;
            return this;
        }

        /// <summary>
        /// Specifies the expect header.
        /// </summary>
        public string Expect { get { return WebReq.Expect; } set { WebReq.Expect = value; } }
        /// <summary>
        /// Specifies the expect header.
        /// </summary>
        /// <param name="expect">The expect value.</param>
        /// <returns>Itself.</returns>
        public Http SetExpect(string expect)
        {
            if (!string.IsNullOrEmpty(expect)) Expect = expect;
            return this;
        }

        /// <summary>
        /// Specifies the connection header.
        /// </summary>
        public string Connection { get { return WebReq.Connection; } set { WebReq.Connection = value; } }
        /// <summary>
        /// Specifies the connection header.
        /// </summary>
        /// <param name="connection">The connection value.</param>
        /// <returns>Itself.</returns>
        public Http SetConnection(string connection)
        {
            if (!string.IsNullOrEmpty(connection)) Connection = connection;
            return this;
        }

        /// <summary>
        /// Specifies the accept header.
        /// </summary>
        public string Accept { get { return WebReq.Accept; } set { WebReq.Accept = value; } }
        /// <summary>
        /// Specifies the accept header.
        /// </summary>
        /// <param name="accept">The accept value.</param>
        /// <returns>Itself.</returns>
        public Http SetAccept(string accept)
        {
            if (!string.IsNullOrEmpty(accept)) Accept = accept;
            return this;
        }

        /// <summary>
        /// Specifies the request method/verb.
        /// </summary>
        public string Method { get { return WebReq.Method; } set { WebReq.Method = value; } }
        /// <summary>
        /// Specifies the request method/verb.
        /// </summary>
        /// <param name="method">The method value.</param>
        /// <returns>Itself.</returns>
        public Http SetMethod(string method)
        {
            if (!string.IsNullOrEmpty(method)) Method = method;
            return this;
        }

        private Encoding _requestEncoding;
        /// <summary>
        /// Specifies the request encoding.
        /// </summary>
        public Encoding RequestEncoding { get { return _requestEncoding ?? Encoding.UTF8; } set { _requestEncoding = value; } }
        /// <summary>
        /// Specifies the request encoding.
        /// </summary>
        /// <param name="requestEncoding">The request encoding value.</param>
        /// <returns>Itself.</returns>
        public Http SetRequestEncoding(Encoding requestEncoding)
        {
            if (requestEncoding != null) _requestEncoding = requestEncoding;
            return this;
        }

        /// <summary>
        /// Specifies the payload to use with the request. Automatically overrides the ContentSize property.
        /// </summary>
        public string Payload
        {
            set
            {
                var data = !String.IsNullOrEmpty(value) ? RequestEncoding.GetBytes(value) : RequestEncoding.GetBytes(String.Empty);
                WebReq.ContentLength = data.Length;
                if (WebReq.ContentLength <= 0) return;
                using (var dataStream = WebReq.GetRequestStream()) dataStream.Write(data, 0, data.Length);
            }
        }
        /// <summary>
        /// Specifies the payload to use with the request. Automatically overrides the ContentSize property.
        /// </summary>
        /// <param name="payload">The payload value.</param>
        /// <returns>Itself.</returns>
        public Http SetPayload(string payload)
        {
            return SetPayload(payload, RequestEncoding);
        }
        /// <summary>
        /// Specifies the payload to use with the request. 
        /// Automatically overrides the ContentSize property.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of payload.</typeparam>
        /// <param name="payload">The payload entity.</param>
        /// <returns>Itself.</returns>
        public Http SetPayload<TPayload>(TPayload payload)
        {
            return SetPayload(payload, RequestEncoding, Format.Xml, Serializer.DataContract);
        }
        /// <summary>
        /// Specifies the payload to use with the request. 
        /// Automatically overrides the ContentSize property.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of payload.</typeparam>
        /// <param name="payload">The payload entity.</param>
        /// <param name="requestEncoding">The value to encode the payload.</param>
        /// <returns>Itself.</returns>
        public Http SetPayload<TPayload>(TPayload payload, Encoding requestEncoding)
        {
            return SetPayload(payload, requestEncoding, Format.Xml, Serializer.DataContract);
        }
        /// <summary>
        /// Specifies the payload to use with the request. 
        /// Automatically overrides the ContentSize property.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of payload.</typeparam>
        /// <param name="payload">The payload entity.</param>
        /// <param name="format">The format of the payload (xml or json).</param>
        /// <returns>Itself.</returns>
        public Http SetPayload<TPayload>(TPayload payload, Format format)
        {
            return SetPayload(payload, RequestEncoding, format, Serializer.DataContract);
        }
        /// <summary>
        /// Specifies the payload to use with the request. 
        /// Automatically overrides the ContentSize property.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of payload.</typeparam>
        /// <param name="payload">The payload entity.</param>
        /// <param name="serializer">Specifies which serializer to use to serialize the payload. It will user DataContract as default.</param>
        /// <returns>Itself.</returns>
        public Http SetPayload<TPayload>(TPayload payload, Serializer serializer)
        {
            return SetPayload(payload, RequestEncoding, Format.Xml, serializer);
        }
        /// <summary>
        /// Specifies the payload to use with the request. 
        /// Automatically overrides the ContentSize property.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of payload.</typeparam>
        /// <param name="payload">The payload entity.</param>
        /// <param name="requestEncoding">The value to encode the payload.</param>
        /// <param name="format">The format of the payload (xml or json).</param>
        /// <returns>Itself.</returns>
        public Http SetPayload<TPayload>(TPayload payload, Encoding requestEncoding, Format format)
        {
            return SetPayload(payload, requestEncoding, format, Serializer.DataContract);
        }
        /// <summary>
        /// Specifies the payload to use with the request. 
        /// Automatically overrides the ContentSize property.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of payload.</typeparam>
        /// <param name="payload">The payload entity.</param>
        /// <param name="requestEncoding">The value to encode the payload.</param>
        /// <param name="serializer">Specifies which serializer to use to serialize the payload. It will user DataContract as default.</param>
        /// <returns>Itself.</returns>
        public Http SetPayload<TPayload>(TPayload payload, Encoding requestEncoding, Serializer serializer)
        {
            return SetPayload(payload, requestEncoding, Format.Xml, serializer);
        }
        /// <summary>
        /// Specifies the payload to use with the request. 
        /// Automatically overrides the ContentSize property.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of payload.</typeparam>
        /// <param name="payload">The payload entity.</param>
        /// <param name="requestEncoding">The value to encode the payload.</param>
        /// <param name="format">The format of the payload (xml or json).</param>
        /// <param name="serializer">Specifies which serializer to use to serialize the payload. It will user DataContract as default.</param>
        /// <returns>Itself.</returns>
        public Http SetPayload<TPayload>(TPayload payload, Encoding requestEncoding, Format format, Serializer serializer)
        {
            string p;
            if (serializer == Serializer.Xml)
            {
                p = XmlSerializerHelper.ToXmlString(payload);
            }
            else
            {
                p = format == Format.Xml
                        ? DataContractSerializerHelper.ToXmlString(payload, requestEncoding)
                        : DataContractSerializerHelper.ToJsonString(payload, requestEncoding);
            }
            return SetPayload(p);
        }
        /// <summary>
        /// Specifies the payload to use with the request. Automatically overrides the ContentSize property.
        /// </summary>
        /// <param name="payload">The payload value.</param>
        /// <param name="requestEncoding">The value to encode the payload.</param>
        /// <returns>Itself.</returns>
        public Http SetPayload(string payload, Encoding requestEncoding)
        {
            byte[] data = !string.IsNullOrEmpty(payload) ? requestEncoding.GetBytes(payload) : requestEncoding.GetBytes(String.Empty);
            WebReq.ContentLength = data.Length;
            if (WebReq.ContentLength > 0)
            {
                using (var dataStream = WebReq.GetRequestStream())
                    dataStream.Write(data, 0, data.Length);
            }
            return this;
        }

        /// <summary>
        /// Specifies the content type header.
        /// </summary>
        public string ContentType { get { return string.IsNullOrEmpty(WebReq.ContentType) ? "text/xml" : WebReq.ContentType; } set { WebReq.ContentType = value; } }
        /// <summary>
        /// Specifies the content type header.
        /// </summary>
        /// <param name="contentType">The content type header value.</param>
        /// <returns>Itself.</returns>
        public Http SetContentType(string contentType)
        {
            ContentType = !String.IsNullOrEmpty(contentType) ? contentType : "text/xml";
            return this;
        }

        /// <summary>
        /// Specifies the user agent header.
        /// </summary>
        public string UserAgent { get { return WebReq.UserAgent; } set { WebReq.UserAgent = value; } }
        /// <summary>
        /// Specifies the user agent header.
        /// </summary>
        /// <param name="userAgent">The user agent value.</param>
        /// <returns>Itself.</returns>
        public Http SetUserAgent(string userAgent)
        {
            if (!String.IsNullOrEmpty(userAgent)) UserAgent = userAgent;
            return this;
        }

        /// <summary>
        /// Specifies the keep alive header.
        /// </summary>
        public bool KeepAlive { get { return WebReq.KeepAlive; } set { WebReq.KeepAlive = value; } }
        /// <summary>
        /// Specifies the keep alive header.
        /// </summary>
        /// <param name="keepAlive">The specified keep alive.</param>
        /// <returns>Itself.</returns>
        public Http SetKeepAlive(bool keepAlive)
        {
            KeepAlive = keepAlive;
            return this;
        }

        /// <summary>
        /// Specifies the timeout header.
        /// </summary>
        public int Timeout { get { return WebReq.Timeout; } set { WebReq.Timeout = value; } }
        /// <summary>
        /// Specifies the timeout header.
        /// </summary>
        /// <param name="timeout">The specified timeout.</param>
        /// <returns>Itself.</returns>
        public Http SetTimeout(int timeout)
        {
            Timeout = timeout;
            return this;
        }

        /// <summary>
        /// Specifies a collection of header values to use with the request.
        /// </summary>
        public WebHeaderCollection Headers { get { return WebReq.Headers; } set { WebReq.Headers = value; } }
        /// <summary>
        /// Specifies a collection of header values to use with the request.
        /// </summary>
        /// <param name="headers">Adds custom headers to the request.</param>
        /// <returns>Itself.</returns>
        public Http SetHeaders(Func<Headers, Headers> headers)
        {
            Headers = headers(new Headers()).Collection;
            return this;
        }

        /// <summary>
        /// Specifies the credentials to use with the request.
        /// </summary>
        public ICredentials Credentials { get { return WebReq.Credentials; } set { WebReq.Credentials = value; } }
        /// <summary>
        /// Specifies the credentials to use with the request.
        /// </summary>
        /// <param name="credentials">The specified credentials</param>
        /// <returns>Itself.</returns>
        public Http SetCredentials(ICredentials credentials)
        {
            if (credentials != null) Credentials = credentials;
            return this;
        }
        /// <summary>
        /// Specifies the credentials to use with the request.
        /// </summary>
        /// <param name="username">The specfied username.</param>
        /// <param name="password">The specified password.</param>
        /// <returns>Itself.</returns>
        public Http SetCredentials(string username, string password)
        {
            Credentials = new NetworkCredential(username, password);
            return this;
        }
        /// <summary>
        /// Specifies the credentials to use with the request.
        /// </summary>
        /// <param name="username">The specified username.</param>
        /// <param name="password">The specifies password.</param>
        /// <param name="domain">The specified domain.</param>
        /// <returns>Itself.</returns>
        public Http SetCredentials(string username, string password, string domain)
        {
            Credentials = new NetworkCredential(username, password, domain);
            return this;
        }

        /// <summary>
        /// Specifies the proxy for the request.
        /// </summary>
        public IWebProxy Proxy { get { return WebReq.Proxy; } set { WebReq.Proxy = value; } }
        /// <summary>
        /// Specifies the proxy for the request.
        /// </summary>
        /// <param name="webProxy">Add proxy configuration.</param>
        /// <returns>Itself.</returns>
        public Http SetProxy(Func<Proxy, Proxy> webProxy)
        {
            Proxy = webProxy(new Proxy()).WebProxy;
            return this;
        }
        /// <summary>
        /// Specifies the proxy for the request.
        /// </summary>
        /// <param name="webProxy">The proxy configuration entity.</param>
        /// <returns>Itself.</returns>
        public Http SetProxy(IWebProxy webProxy)
        {
            if (webProxy != null) Proxy = webProxy;
            return this;
        }

        private Encoding _responseEncoding = Encoding.UTF8;
        /// <summary>
        /// Specifies what encoding to use for the response.
        /// </summary>
        public Encoding ResponseEncoding { get { return _responseEncoding; } set { _responseEncoding = value; } }
        /// <summary>
        /// Specifies what encoding to use for the response.
        /// </summary>
        /// <param name="responseEncoding">The specified encoding.</param>
        /// <returns>Itself.</returns>
        public Http SetResponseEncoding(Encoding responseEncoding)
        {
            _responseEncoding = responseEncoding;
            return this;
        }

        internal HttpWebRequest WebReq;
        /// <summary>
        /// Initializes the http helper class.
        /// </summary>
        /// <param name="url">The request url.</param>
        public Http(string url)
        {
            WebReq = (HttpWebRequest)WebRequest.Create(url);
            Initialize();
        }
        /// <summary>
        /// Initializes the Http class.
        /// </summary>
        /// <param name="builder">The function to build the url.</param>
        public Http(Func<UrlBuilder, UrlBuilder> builder)
        {
            WebReq = (HttpWebRequest)WebRequest.Create(builder(new UrlBuilder()).Build());
            Initialize();
        }

        /// <summary>
        /// Initializes some default configurations for this request. These can be changed using either the fluent api or the properties.
        /// </summary>
        private void Initialize()
        {
            SetContentType("text/xml");
        }

        /// <summary>
        /// Sends a specified request.
        /// </summary>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <returns>The response entity.</returns>
        public TReturn DoRequest<TReturn>()
        {
            return DoRequest<TReturn>(Format.Xml, Serializer.DataContract);
        }
        /// <summary>
        /// Sends a specified request.
        /// </summary>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <returns>The response entity.</returns>
        public TReturn DoRequest<TReturn>(Format format)
        {
            return DoRequest<TReturn>(format, Serializer.DataContract);
        }
        /// <summary>
        /// Sends a specified request.
        /// </summary>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="serializer">Specifies which serializer to use to serialize the payload. It will user DataContract as default.</param>
        /// <returns>The response entity.</returns>
        public TReturn DoRequest<TReturn>(Serializer serializer)
        {
            return DoRequest<TReturn>(Format.Xml, serializer);
        }
        /// <summary>
        /// Sends a specified request.
        /// </summary>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="serializer">Specifies which serializer to use to serialize the payload. It will user DataContract as default.</param>
        /// <returns>The response entity.</returns>
        public TReturn DoRequest<TReturn>(Format format, Serializer serializer)
        {
            var response = DoRequest();
            if (Response != null && response != null)
            {
                try
                {
                    var result = (serializer == Serializer.Xml)
                        ? XmlSerializerHelper.FromXmlString<TReturn>(response)
                        : format == Format.Xml
                            ? DataContractSerializerHelper.FromXmlString<TReturn>(response)
                            : DataContractSerializerHelper.FromJsonString<TReturn>(response, _responseEncoding);
                    return result;
                }
                catch { }
            }
            return default(TReturn);
        }
        /// <summary>
        /// Sends a specified request.
        /// </summary>
        /// <returns>A string containing the response.</returns>
        public string DoRequest()
        {
            HttpWebResponse webResp = null;
            try
            {
                //requests the data
                webResp = (HttpWebResponse)WebReq.GetResponse();
                // Get the stream associated with the response.
                var response = ReadResponse(webResp);
                Response = new HttpResponseMessage { StatusCode = webResp.StatusCode, ReasonPhrase = webResp.StatusDescription };
                return response;
            }
            catch (WebException webException)
            {
                if (webException.Status == WebExceptionStatus.ProtocolError)
                {
                    webResp = webException.Response as HttpWebResponse;
                    if (webResp != null)
                    {
                        Response = new HttpResponseMessage { StatusCode = webResp.StatusCode, ReasonPhrase = webException.Message };
                        return ReadResponse(webResp);
                    }
                }
                throw;
            }
            finally
            {
                if (webResp != null && Response != null)
                {
                    for (int i = 0; i < webResp.Headers.Count; ++i)
                    {
                        Response.Headers.TryAddWithoutValidation(webResp.Headers.Keys[i], webResp.Headers[i]);
                    }
                }
                if (webResp != null) webResp.Close();
            }
        }

        /// <summary>
        /// Reads the contents of the http response stream.
        /// </summary>
        /// <param name="webResponse">The specified http response stream to read the contents from.</param>
        /// <returns>A string containing the stream content.</returns>
        private string ReadResponse(HttpWebResponse webResponse)
        {
            using (var receiveStream = webResponse.GetResponseStream())
            {
                if (receiveStream == null) throw new HttpException("Response stream is null.");
                // Pipes the stream to a higher level stream reader with the required encoding format.
                using (var readStream = new StreamReader(receiveStream, (_responseEncoding ?? Encoding.UTF8)))
                    return readStream.ReadToEnd();
            }
        }
    }
}