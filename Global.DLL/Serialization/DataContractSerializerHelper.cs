using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace Global.Serialization
{
    /// <summary>
    /// Class to handle serialization of objects using DataContractSerializtion.
    /// </summary>
    public class DataContractSerializerHelper
    {
        /// <summary>
        /// Deserializes the file to the specified object.
        /// </summary>
        /// <param name="filePath">The path of the file to be serialized.</param>
        /// <typeparam name="TType">The object to be handled.</typeparam>
        /// <returns>The deserialized object.</returns>
        public static TType FromXmlFile<TType>(string filePath)
        {
            XmlReader reader = null;
            try
            {
                // Write the income object to a file
                var serializer = new DataContractSerializer(typeof(TType));
                reader = new XmlTextReader(filePath);
                return (TType)serializer.ReadObject(reader);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
        /// <summary>
        /// Deserializes the file to the specified object.
        /// </summary>
        /// <param name="filePath">The path of the file to be serialized.</param>
        /// <param name="encoding">The encoding used to deserialize the string object.</param>
        /// <typeparam name="TType">The object to be handled.</typeparam>
        /// <returns>The deserialized object.</returns>
        public static TType FromJsonFile<TType>(string filePath, Encoding encoding)
        {
            using (var textReader = new StreamReader(filePath, encoding))
            {
                var serializer = new DataContractJsonSerializer(typeof(TType));
                using (var memoryStream = new MemoryStream(encoding.GetBytes(textReader.ReadToEnd())))
                {
                    return (TType)serializer.ReadObject(memoryStream);
                }
            }
        }


        /// <summary>
        /// Deserializes the string to a specified object.
        /// </summary>
        /// <param name="objectString">The object string.</param>
        /// <typeparam name="TType">The object to be handled.</typeparam>
        /// <returns>The deserialized object.</returns>
        public static TType FromXmlString<TType>(string objectString)
        {
            using (var stringReader = new StringReader(objectString))
            {
                // Write the income object to a file
                var serializer = new DataContractSerializer(typeof(TType));
                var xmlReader = XmlReader.Create(stringReader);
                return (TType)serializer.ReadObject(xmlReader);
            }
        }
        /// <summary>
        /// Deserializes a json string to the specified object.
        /// </summary>
        /// <param name="objectString">The object as a json string.</param>
        /// <typeparam name="TType">The object to be handled.</typeparam>
        /// <returns>The deserialized object.</returns>
        public static TType FromJsonString<TType>(string objectString)
        {
            return FromJsonString<TType>(objectString, Encoding.UTF8);
        }
        /// <summary>
        /// Deserializes a json string to the specified object.
        /// </summary>
        /// <param name="objectString">The object as a json string.</param>
        /// <param name="encoding">The encoding used to deserialize the string to the specified object type.</param>
        /// <typeparam name="TType">The object to be handled.</typeparam>
        /// <returns>The deserialized object.</returns>
        public static TType FromJsonString<TType>(string objectString, Encoding encoding)
        {
            using (var memoryStream = new MemoryStream(encoding.GetBytes(objectString)))
            {
                // Write the income object to a file
                var jsonSerializer = new DataContractJsonSerializer(typeof(TType));
                return (TType)jsonSerializer.ReadObject(memoryStream);
            }
        }




        /// <summary>
        /// Serialized a specified object to a file.
        /// </summary>
        /// <param name="filePath">The path where the serialized object must be stored.</param>
        /// <typeparam name="TType">The object to be handled.</typeparam>
        /// <param name="entity">The entity to be serialized.</param>
        public static void ToXmlFile<TType>(string filePath, TType entity)
        {
            ToXmlFile(filePath, entity, Encoding.UTF8);
        }
        /// <summary>
        /// Serialized a specified object to a file.
        /// </summary>
        /// <param name="filePath">The path where the serialized object must be stored.</param>
        /// <param name="entity">The entity to be serialized.</param>
        /// <typeparam name="TType">The object to be handled.</typeparam>
        /// <param name="encoding">The encoding to used in the serialization precess.</param>
        public static void ToXmlFile<TType>(string filePath, TType entity, Encoding encoding)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Write the income object to a file
                var serializer = new DataContractSerializer(typeof(TType));
                serializer.WriteObject(memoryStream, entity);
                using (var textWriter = new StreamWriter(filePath))
                {
                    memoryStream.Flush();
                    memoryStream.Position = 0;
                    using (var streamReader = new StreamReader(memoryStream, encoding))
                    {
                        textWriter.Write(streamReader.ReadToEnd());
                    }
                }
            }
        }
        /// <summary>
        /// Serialized a specified object to a file.
        /// </summary>
        /// <param name="filePath">The path where the serialized object must be stored.</param>
        /// <param name="entity">The entity to be serialized.</param>
        /// <typeparam name="TType">The object to be handled.</typeparam>
        /// <param name="encoding">The encoding to used in the serialization precess.</param>
        public static void ToJsonFile<TType>(string filePath, TType entity, Encoding encoding)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Write the income object to a file
                var serializer = new DataContractJsonSerializer(typeof(TType));
                serializer.WriteObject(memoryStream, entity);
                using (var textWriter = new StreamWriter(filePath))
                {
                    memoryStream.Flush();
                    memoryStream.Position = 0;
                    using (var streamReader = new StreamReader(memoryStream, encoding))
                    {
                        textWriter.Write(streamReader.ReadToEnd());
                    }
                }
            }
        }


        /// <summary>
        /// Serializes the specified object to a string.
        /// </summary>
        /// <param name="entity">The object to be serialized.</param>
        /// <typeparam name="TType">The object to be handled.</typeparam>
        /// <returns>The serialized string.</returns>
        public static string ToXmlString<TType>(TType entity)
        {
            return ToXmlString(entity, Encoding.UTF8);
        }
        /// <summary>
        /// Serializes the specified object to a string.
        /// </summary>
        /// <param name="entity">The object to be serialized.</param>
        /// <param name="encoding">The encoding to be used in the serialization preocess.</param>
        /// <typeparam name="TType">The object to be handled.</typeparam>
        /// <returns>The serialized string.</returns>
        public static string ToXmlString<TType>(TType entity, Encoding encoding)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Write the income object to a strem
                var serializer = new DataContractSerializer(typeof(TType));
                serializer.WriteObject(memoryStream, entity);
                memoryStream.Flush();
                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream, encoding))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// Serializes the specified object to a json string.
        /// </summary>
        /// <param name="entity">The object to be serialized.</param>
        /// <typeparam name="TType">The object to be handled.</typeparam>
        /// <returns>The serialized object as a string.</returns>
        public static string ToJsonString<TType>(TType entity)
        {
            return ToJsonString(entity, Encoding.UTF8);
        }
        /// <summary>
        /// Serializes the specified object to a json string.
        /// </summary>
        /// <param name="entity">The object to be serialized.</param>
        /// <param name="encoding">The specified encoding to be used when serializing.</param>
        /// <typeparam name="TType">The object to be handled.</typeparam>
        /// <returns>The serialized object as a string.</returns>
        public static string ToJsonString<TType>(TType entity, Encoding encoding)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Write the income object to a strem
                var jsonSerializer = new DataContractJsonSerializer(typeof(TType));
                jsonSerializer.WriteObject(memoryStream, entity);
                memoryStream.Flush();
                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream, encoding))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}