using System;
using System.Text;
using System.Xml.Schema;
using System.Xml;
using System.IO;
using System.Data;

namespace Global.Xml
{
    ///<summary>
    /// Xml validation class.
    ///</summary>
    public class ValidationHelper
    {
        /// <summary>
        /// Gets the number of errors (if any) while validating the xml file.
        /// </summary>
        static int _errorsCount;

        /// <summary>
        /// Gets the error messages (if any) while validating the xml file.
        /// </summary>
        static string _errorMessage = "";

        /// <summary>
        /// Validates a DataSet using a xsd file.
        /// </summary>
        /// <param name="ds">The DataSet to be validated.</param>
        /// <param name="xsdFilePath">The xsd file path.</param>
        public bool ValidateXmlWithXsd(DataSet ds, string xsdFilePath)
        {
            using (var ms = new MemoryStream())
            {
                ds.WriteXml(ms);
                return ValidateXmlWithXsd(ms, xsdFilePath);
            }
        }

        /// <summary>
        /// Validates an xml content using a xsd file.
        /// </summary>
        /// <param name="xmlContent">The xml content to be validated.</param>
        /// <param name="encoding">The encoding of the xml content.</param>
        /// <param name="xsdFilePath">The xsd file path.</param>
        public bool ValidateXmlWithXsd(string xmlContent, Encoding encoding, string xsdFilePath)
        {
            using (var ms = new MemoryStream(encoding.GetBytes(xmlContent)))
            {
                return ValidateXmlWithXsd(ms, xsdFilePath);
            }
        }

        /// <summary>
        /// Validates an xml stream using a xsd file.
        /// </summary>
        /// <param name="xmlStream">The xml stream to be validated.</param>
        /// <param name="xsdFilePath">The xsd file path.</param>
        /// <returns>True if the validation was successful.</returns>
        public bool ValidateXmlWithXsd(Stream xmlStream, string xsdFilePath)
        {
            // Create the XmlSchemaSet class.
            var sc = new XmlSchemaSet();

            // Add the schema to the collection.
            sc.Add(null, xsdFilePath);

            // Set the validation settings.
            var settings = new XmlReaderSettings {ValidationType = ValidationType.Schema, Schemas = sc};
            settings.ValidationEventHandler += ValidationCallBack;

            // Create the XmlReader object.
            xmlStream.Seek(0, SeekOrigin.Begin);
            XmlReader reader = XmlReader.Create(xmlStream, settings);

            // Parse the file. 
            while (reader.Read())
            {
            }

            // Raise exception, if XML validation fails
            if (_errorsCount > 0)
            {
                throw new Exception(_errorMessage);
            }

            return true;
        }

        /// <summary>
        /// Displays any validation errors.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The arguments.</param>
        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            _errorMessage = _errorMessage + e.Message + "\r\n";
            _errorsCount++;
        }
    }
}
