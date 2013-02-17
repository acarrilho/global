using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Global
{
    /// <summary>
    /// Class Parameter used by class Res.cs
    /// </summary>
    [XmlRoot(Namespace = "", ElementName = "parameter")]
    [DataContract(Namespace = "", Name = "parameter")]
    public class Parameter
    {
        /// <summary>
        /// Data member Key.
        /// </summary>
        [XmlElement(ElementName = "key")]
        [DataMember(Name = "key")]
        public string Key { get; set; }

        /// <summary>
        /// Data member Value. 
        /// </summary>
        [XmlElement(ElementName = "value")]
        [DataMember(Name = "value")]
        public string Value { get; set; }
    } 

    /// <summary>
    /// The result of an http request. Generic for any type of request.
    /// </summary>
    /// <typeparam name="TEntity">The type of object returned by the request.</typeparam>
    public class Result<TEntity>
    {
        /// <summary>
        /// Represents the value of the response.
        /// </summary>
        public TEntity Value { get; protected set; }
        /// <summary>
        /// Represents the request result object.
        /// </summary>
        private static HttpResult Request { get; set; }
        /// <summary>
        /// If an exception is thrown it will be stored here.
        /// </summary>
        public Exception Exception { get; protected set; }
        /// <summary>
        /// Checks if the request was successful.
        /// </summary>
        /// <returns>A boolean indicating if the request was successful.</returns>
        public bool IsSuccessful()
        {
            return Request.Equals(HttpResult.Success);
        }
        /// <summary>
        /// Sets the Request parameter to Success..
        /// </summary>
        public static void Sucess()
        {
            Request = HttpResult.Success;
        }
        /// <summary>
        /// Sets the Request parameter to Failure.
        /// </summary>
        public static void Failure()
        {
            Request = HttpResult.Failure;
        }

    }
}