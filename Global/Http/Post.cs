using System;
using System.Text;

namespace Global.Http
{
    /// <summary>
    /// Helper to handle post request. For more fine grained requests use the Http class.
    /// </summary>
    public class Post : Http
    {
        /// <summary>
        /// Initializes the http helper class.
        /// </summary>
        /// <param name="url">The request url.</param>
        public Post(string url) : base(url) { }

        /// <summary>
        /// Initializes the Http class.
        /// </summary>
        /// <param name="builder">The function to build the url.</param>
        public Post(Func<UrlBuilder, UrlBuilder> builder) : base(builder) { }

        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TReturn>(Format format, string payload)
        {
            SetMethod("POST");
            SetPayload(payload);
            return DoRequest<TReturn>(format);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, TPayload payload)
        {
            SetMethod("POST");
            SetPayload(payload);
            return DoRequest<TReturn>(format);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, TPayload payload, Encoding payloadEncoding)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding);
            return DoRequest<TReturn>(format);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, TPayload payload, Format payloadFormat)
        {
            SetMethod("POST");
            SetPayload(payload, payloadFormat);
            return DoRequest<TReturn>(format);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, TPayload payload, Encoding payloadEncoding, Format payloadFormat)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadFormat);
            return DoRequest<TReturn>(format);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, TPayload payload, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadSerializer);
            return DoRequest<TReturn>(format);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, TPayload payload, Encoding payloadEncoding, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadSerializer);
            return DoRequest<TReturn>(format);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, TPayload payload, Encoding payloadEncoding, Format payloadFormat, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadFormat, payloadSerializer);
            return DoRequest<TReturn>(format);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TReturn>(Serializer serializer, string payload)
        {
            SetMethod("POST");
            SetPayload(payload);
            return DoRequest<TReturn>(serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Serializer serializer, TPayload payload)
        {
            SetMethod("POST");
            SetPayload(payload);
            return DoRequest<TReturn>(serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Serializer serializer, TPayload payload, Encoding payloadEncoding)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding);
            return DoRequest<TReturn>(serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Serializer serializer, TPayload payload, Format payloadFormat)
        {
            SetMethod("POST");
            SetPayload(payload, payloadFormat);
            return DoRequest<TReturn>(serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Serializer serializer, TPayload payload, Encoding payloadEncoding, Format payloadFormat)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadFormat);
            return DoRequest<TReturn>(serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Serializer serializer, TPayload payload, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadSerializer);
            return DoRequest<TReturn>(serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Serializer serializer, TPayload payload, Encoding payloadEncoding, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadSerializer);
            return DoRequest<TReturn>(serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Serializer serializer, TPayload payload, Encoding payloadEncoding, Format payloadFormat, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadFormat, payloadSerializer);
            return DoRequest<TReturn>(serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TReturn>(Format format, Serializer serializer, string payload)
        {
            SetMethod("POST");
            SetPayload(payload);
            return DoRequest<TReturn>(format, serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, Serializer serializer, TPayload payload)
        {
            SetMethod("POST");
            SetPayload(payload);
            return DoRequest<TReturn>(format, serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, Serializer serializer, TPayload payload, Encoding payloadEncoding)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding);
            return DoRequest<TReturn>(format, serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, Serializer serializer, TPayload payload, Format payloadFormat)
        {
            SetMethod("POST");
            SetPayload(payload, payloadFormat);
            return DoRequest<TReturn>(format, serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, Serializer serializer, TPayload payload, Encoding payloadEncoding, Format payloadFormat)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadFormat);
            return DoRequest<TReturn>(format, serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, Serializer serializer, TPayload payload, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadSerializer);
            return DoRequest<TReturn>(format, serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, Serializer serializer, TPayload payload, Encoding payloadEncoding, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadSerializer);
            return DoRequest<TReturn>(format, serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="format">The format of the response (xml or json).</param>
        /// <param name="serializer">Specifies which serializer to use to serialize the response payload. It will user DataContract as default.</param>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(Format format, Serializer serializer, TPayload payload, Encoding payloadEncoding, Format payloadFormat, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadFormat, payloadSerializer);
            return DoRequest<TReturn>(format, serializer);
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TReturn>(string payload)
        {
            SetMethod("POST");
            SetPayload(payload);
            return DoRequest<TReturn>();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(TPayload payload)
        {
            SetMethod("POST");
            SetPayload(payload);
            return DoRequest<TReturn>();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(TPayload payload, Encoding payloadEncoding)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding);
            return DoRequest<TReturn>();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(TPayload payload, Format payloadFormat)
        {
            SetMethod("POST");
            SetPayload(payload, payloadFormat);
            return DoRequest<TReturn>();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(TPayload payload, Encoding payloadEncoding, Format payloadFormat)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadFormat);
            return DoRequest<TReturn>();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(TPayload payload, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadSerializer);
            return DoRequest<TReturn>();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(TPayload payload, Encoding payloadEncoding, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadSerializer);
            return DoRequest<TReturn>();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <typeparam name="TReturn">The entity type of the response.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public TReturn Do<TPayload, TReturn>(TPayload payload, Encoding payloadEncoding, Format payloadFormat, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadFormat, payloadSerializer);
            return DoRequest<TReturn>();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <returns>The response.</returns>
        public string Do(string payload)
        {
            SetMethod("POST");
            SetPayload(payload);
            return DoRequest();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <returns>The response.</returns>
        public string Do<TPayload>(TPayload payload)
        {
            SetMethod("POST");
            SetPayload(payload);
            return DoRequest();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <returns>The response.</returns>
        public string Do<TPayload>(TPayload payload, Encoding payloadEncoding)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding);
            return DoRequest();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <returns>The response.</returns>
        public string Do<TPayload>(TPayload payload, Format payloadFormat)
        {
            SetMethod("POST");
            SetPayload(payload, payloadFormat);
            return DoRequest();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <returns>The response.</returns>
        public string Do<TPayload>(TPayload payload, Encoding payloadEncoding, Format payloadFormat)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadFormat);
            return DoRequest();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public string Do<TPayload>(TPayload payload, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadSerializer);
            return DoRequest();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public string Do<TPayload>(TPayload payload, Encoding payloadEncoding, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadSerializer);
            return DoRequest();
        }
        /// <summary>
        /// Does a post request.
        /// </summary>
        /// <typeparam name="TPayload">The entity type of the payload.</typeparam>
        /// <param name="payload">The payload to be sent with the request.</param>
        /// <param name="payloadEncoding">The encoding to be used when serializing the payload.</param>
        /// <param name="payloadFormat">The format of the request (xml or json).</param>
        /// <param name="payloadSerializer">Specifies which serializer to use to serialize the request payload. It will user DataContract as default.</param>
        /// <returns>The response.</returns>
        public string Do<TPayload>(TPayload payload, Encoding payloadEncoding, Format payloadFormat, Serializer payloadSerializer)
        {
            SetMethod("POST");
            SetPayload(payload, payloadEncoding, payloadFormat, payloadSerializer);
            return DoRequest();
        }
    }
}