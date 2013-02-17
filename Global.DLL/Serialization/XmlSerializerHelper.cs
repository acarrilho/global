using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Global.Serialization
{
    ///<summary>
    /// Class to be used when performing serialization tasks.
    ///</summary>
    public class XmlSerializerHelper
    {
        /// <summary>
        /// Deserializes a file into an object.
        /// </summary>
        /// <param name="xmlFilePath">The content to be serialized.</param>
        ///<typeparam name="T">The object type to be serialized/deserialized.</typeparam>
        /// <returns>The object serialized from the file.</returns>
        public static T FromXmlFile<T>(string xmlFilePath)
        {
            T entity;
            using (var reader = new StreamReader(xmlFilePath))
            {
                var serializer = new XmlSerializer(typeof(T));
                entity = (T)serializer.Deserialize(reader);
            }

            return entity;
        }

        /// <summary>
        /// Deserializes a string into an object.
        /// </summary>
        /// <param name="xmlString">The content to be serialized.</param>
        ///<typeparam name="T">The object type to be serialized/deserialized.</typeparam>
        /// <returns>The object serialized from the file.</returns>
        public static T FromXmlString<T>(string xmlString)
        {
            T entity;
            using (var reader = new StringReader(xmlString))
            {
                var serializer = new XmlSerializer(typeof(T));
                entity = (T)serializer.Deserialize(reader);
            }

            return entity;
        }

        /// <summary>
        /// Serializes ans object to a file.
        /// </summary>
        /// <param name="xmlFilePath">The file where the serialized object should go to.</param>
        ///<typeparam name="T">The object type to be serialized/deserialized.</typeparam>
        /// <param name="entity">The objkect to be serialized.</param>
        public static void ToXmlFile<T>(string xmlFilePath, T entity)
        {
            using (var writer = new XmlTextWriter(xmlFilePath, Encoding.UTF8))
            {
                writer.WriteStartDocument();
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, entity, GetEmptyNamespace());
            }
        }

        /// <summary>
        /// Serializes an object to a file.
        /// </summary>
        /// <param name="xmlFilePath">The file where the serialized object should go to.</param>
        /// <param name="entity">The object to be serialized.</param>
        /// <param name="docTypeName"></param>
        /// <param name="docTypePubId"></param>
        /// <param name="docTypeSysId"></param>
        /// <param name="docTypeSubSet"></param>
        ///<typeparam name="T">The object type to be serialized/deserialized.</typeparam>
        public static void ToXmlFile<T>(string xmlFilePath, T entity, string docTypeName, string docTypePubId,
                                        string docTypeSysId, string docTypeSubSet)
        {
            using (var writer = new XmlTextWriter(xmlFilePath, Encoding.UTF8))
            {
                writer.WriteStartDocument();
                writer.WriteDocType(docTypeName, docTypePubId, docTypeSysId, docTypeSubSet);
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, entity, GetEmptyNamespace());
            }
        }

        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <param name="entity">The object to be serialized.</param>
        ///<typeparam name="T">The object type to be serialized/deserialized.</typeparam>
        /// <returns>The serialized object string.</returns>
        public static string ToXmlString<T>(T entity)
        {
            string xml;
            var ms = new MemoryStream();
            using (var writer = new XmlTextWriter(ms, Encoding.UTF8))
            {
                writer.WriteStartDocument();
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, entity, GetEmptyNamespace());
                xml = ByteArrayToString(Encoding.UTF8, ms.ToArray());
            }

            return xml;
        }

        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <param name="entity">The object to be serialized.</param>
        /// <param name="docTypeName"></param>
        /// <param name="docTypePubId"></param>
        /// <param name="docTypeSysId"></param>
        /// <param name="docTypeSubSet"></param>
        ///<typeparam name="T">The object type to be serialized/deserialized.</typeparam>
        /// <returns>The serialized object string.</returns>
        public static string ToXmlString<T>(T entity, string docTypeName, string docTypePubId,
                                            string docTypeSysId, string docTypeSubSet)
        {
            string xml;
            var ms = new MemoryStream();
            using (var writer = new XmlTextWriter(ms, Encoding.UTF8))
            {
                writer.WriteStartDocument();
                writer.WriteDocType(docTypeName, docTypePubId, docTypeSysId, docTypeSubSet);
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(ms, entity, GetEmptyNamespace());
                xml = ByteArrayToString(Encoding.UTF8, ms.ToArray());
            }

            return xml;
        }

        ///<summary>
        /// Gets the namespaces from a dictionary.
        ///</summary>
        ///<param name="prefixNs">A collection of namespace prefixes to be added.</param>
        ///<returns>Xml serializer namespaces.</returns>
        public static XmlSerializerNamespaces GetNamespace(Dictionary<string, string> prefixNs)
        {
            var namespaces = new XmlSerializerNamespaces();
            foreach (var keyValue in prefixNs)
            {
                namespaces.Add(keyValue.Key, keyValue.Value);
            }

            return namespaces;
        }

        ///<summary>
        /// Gets a collection of empty namespaces.
        ///</summary>
        ///<returns>Xml serializer namespaces.</returns>
        public static XmlSerializerNamespaces GetEmptyNamespace()
        {
            var dic = new Dictionary<string, string>
                          {
                              {
                                  String.Empty,
                                  String.Empty
                                  }
                          };
            return GetNamespace(dic);
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values to a complete String.
        /// </summary>
        /// <param name="encoding">The encoding to be applied in the convertion.</param>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private static String ByteArrayToString(Encoding encoding, Byte[] characters)
        {
            var constructedString = encoding.GetString(characters);
            return (constructedString);
        }
    }
}