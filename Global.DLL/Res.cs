//Called Res due to existance of a class named Result
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Global
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [XmlRoot(Namespace = "", ElementName = "result")]
    [DataContract(Namespace = "", Name = "result")]
    public class Res<T>
    {
        /// <summary>
        /// Data member Code.
        /// </summary>
        [XmlElement(ElementName = "code")]
        [DataMember(Name = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Data member Code.
        /// </summary>
        [XmlElement(ElementName = "successful")]
        [DataMember(Name = "successful")]
        public bool Successful { get; set; }

        /// <summary>
        /// Error Message.
        /// </summary>
        [XmlElement(ElementName = "message")]
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Detailed Error Message.
        /// </summary>
        [XmlElement(ElementName = "detailedmessage")]
        [DataMember(Name = "detailedmessage")]
        public string DetailedMessage { get; set; }

        /// <summary>
        /// Data member Parameters.
        /// </summary>
        [XmlElement(ElementName = "parameter")]
        [DataMember(Name = "parameters")]
        public List<Parameter> Parameters { get; set; }

        /// <summary>
        /// Generic element Value -->here is the data. 
        /// </summary>
        [XmlElement(ElementName = "value")]
        [DataMember(Name = "value")]
        public T Value { get; set; }
    }
}