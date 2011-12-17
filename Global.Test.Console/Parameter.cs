using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Global.Test.Console
{
    [XmlRoot(Namespace = "", ElementName = "parameter")]
    [DataContract(Namespace = "", Name = "parameter")]
    public class Parameter
    {
        [XmlElement(ElementName = "key")]
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [XmlElement(ElementName = "value")]
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}