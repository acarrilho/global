using System.Net;

namespace Global.Http
{
    /// <summary>
    /// Sets the headers to be used in the request.
    /// </summary>
    public class Header
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
        public Header AddRequestHeader(string header)
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
        public Header AddRequestHeader(string name, string value)
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
        public Header AddRequestHeader(HttpRequestHeader header, string value)
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
        public Header AddResponseHeader(HttpResponseHeader header, string value)
        {
            Collection.Add(header, value);
            return this;
        }
    }
}