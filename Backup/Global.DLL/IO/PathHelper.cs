using System;
namespace Global.IO
{
    ///<summary>
    /// Performs folder and files actions
    ///</summary>
    public class PathHelper
    {
        /// <summary>
        /// Adds a "\" to the end of the directory if it does not exist.
        /// </summary>
        /// <param name="path">the directory path.</param>
        /// <returns></returns>
        public static string ValidateEndOfPath(string path)
        {
            if (!path.EndsWith(@"\"))
                return String.Concat(path, "\\");

            return path;
        }

        /// <summary>
        /// Gets the file name without extention.
        /// </summary>
        /// <param name="fullPath">The full file path.</param>
        /// <returns></returns>
        public static string GetFileNameOnly(string fullPath)
        {
            return System.IO.Path.GetFileNameWithoutExtension(fullPath);
        }

        /// <summary>
        /// Returns the file name with extension.
        /// </summary>
        /// <param name="fullPath">the full file path.</param>
        /// <returns></returns>
        public static string GetFileNameWithExtension(string fullPath)
        {
            return System.IO.Path.GetFileName(fullPath);
        }

        /// <summary>
        /// Changes the extension of the specified file.
        /// </summary>
        /// <param name="fullPath">the full file path.</param>
        /// <param name="extension">The new extension.</param>
        /// <returns></returns>
        public static string ChangeFileExtension(string fullPath, string extension)
        {
            return System.IO.Path.ChangeExtension(fullPath, extension);
        }

        /// <summary>
        /// Get the file extension (includes the dot).
        /// </summary>
        /// <param name="fullPath">The filename or full path.</param>
        /// <returns></returns>
        public static string GetFileNameExtension(string fullPath)
        {
            return System.IO.Path.GetExtension(fullPath);
        }

        /// <summary>
        /// Returns a valid extension with a dot in the begining.
        /// </summary>
        /// <param name="extension">Extension to be validated.</param>
        /// <returns></returns>
        public static string ValidateStartOfExtension(string extension)
        {
            if (!extension.StartsWith("."))
                extension = "." + extension;
            return extension;
        }

        /// <summary>
        /// Returns the path of the file.
        /// </summary>
        /// <param name="fullFilePath">The full path.</param>
        /// <returns></returns>
        public static string GetFilePath(string fullFilePath)
        {
            return ValidateEndOfPath(System.IO.Path.GetDirectoryName(fullFilePath));
        }
    }
}
