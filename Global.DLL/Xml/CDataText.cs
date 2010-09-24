using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Global.Xml
{
    ///<summary>
    /// Class that converts the incoming text to CData.
    ///</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CDataText : Attribute, IXmlSerializable
    {
        ///<summary>
        /// The text that should be transformed to CData.
        ///</summary>
        public string Text { get; set; }

        ///<summary>
        /// Constructor.
        ///</summary>
        public CDataText()
        {
        }

        ///<summary>
        /// Constructor that specifies the current text string.
        ///</summary>
        ///<param name="text">The current text string.</param>
        public CDataText(string text)
        {
            Text = text;
        }

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader)
        {
            Text = reader.ReadString();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteCData(Text);
        }
    }
}
