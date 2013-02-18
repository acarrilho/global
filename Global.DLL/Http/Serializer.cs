namespace Global.Http
{
    /// <summary>
    /// Specifies the serializer to use when serializing objects (eg: payload).
    /// </summary>
    public enum Serializer
    {
        /// <summary>
        /// Uses the DataContractSerializer.
        /// </summary>
        DataContract,
        /// <summary>
        /// User XmlSerializer
        /// </summary>
        Xml
    }
}