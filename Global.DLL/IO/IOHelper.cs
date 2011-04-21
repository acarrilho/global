using System;
using System.IO;

namespace Global.IO
{
    ///<summary>
    /// Performs input/output actions.
    ///</summary>
    public class IOHelper
    {
        /// <summary>
        /// Get the last modified date of the specified file.
        /// </summary>
        /// <param name="filePath">The full file path.</param>
        /// <returns>The last modified DateTime.</returns>
        public static DateTime GetFileLastModified(string filePath)
        {
            var fInfo = new FileInfo(filePath);
            return fInfo.LastWriteTime;
        }

        /// <summary>
        /// Gets the file content.
        /// </summary>
        /// <param name="filePath">The full file path.</param>
        /// <returns>A string containing the file contents.</returns>
        public static string GetFileContent(string filePath)
        {
            using (TextReader txtReader = new StreamReader(filePath))
            {
                return txtReader.ReadToEnd();
            }
        }
    }
}
