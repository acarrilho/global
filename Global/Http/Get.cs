using System;

namespace Global.Http
{
    /// <summary>
    /// Helper to handle get request. For more fine grained requests use the Http class.
    /// </summary>
    public class Get : Http
    {
        /// <summary>
        /// Initializes the http helper class.
        /// </summary>
        /// <param name="url">The request url.</param>
        public Get(string url)
            : base(url)
        {
            SetMethod("GET");
        }
        /// <summary>
        /// Initializes the Http class.
        /// </summary>
        /// <param name="builder">The function to build the url.</param>
        public Get(Func<UrlBuilder, UrlBuilder> builder)
            : base(builder)
        {
            SetMethod("GET");
        }

        /// <summary>
        /// Does a get request.
        /// </summary>
        /// <returns>A string with the response.</returns>
        public string Do()
        {
            return DoRequest();
        }
        /// <summary>
        /// Does a get request.
        /// </summary>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <returns>The response.</returns>
        public TReturn Do<TReturn>(Format format)
        {
            return DoRequest<TReturn>(format);
        }
        /// <summary>
        /// Does a get request.
        /// </summary>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="serializer">Specifies which serializer to use to serialize the payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TReturn>(Serializer serializer)
        {
            return DoRequest<TReturn>(serializer);
        }
        /// <summary>
        /// Does a get request.
        /// </summary>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="serializer">Specifies which serializer to use to serialize the payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TReturn>(Format format, Serializer serializer)
        {
            return DoRequest<TReturn>(format, serializer);
        }
        /// <summary>
        /// Does a get request.
        /// </summary>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <returns>The response.</returns>
        public TReturn Do<TReturn>()
        {
            return DoRequest<TReturn>();
        }
    }
}