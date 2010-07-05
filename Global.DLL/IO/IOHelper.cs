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
        /// Get the last modified date of the specified folder.
        /// </summary>
        /// <param name="folderPath">The full folder path.</param>
        /// <returns>The last modified DateTime.</returns>
        public static DateTime GetFolderLastModified(string folderPath)
        {
            var dInfo = new DirectoryInfo(folderPath);
            return dInfo.LastWriteTime;
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
        /// <summary>
        /// Moves a file to another destination.
        /// </summary>
        /// <param name="fileToMove">The file to be moved.</param>
        /// <param name="destinationFolder">The destination folder to where the file should be moved.</param>
        /// <param name="createDestinationFolder">Defines if the destination folder must be created if it doesn't exist.</param>
        /// <returns>A boolean indicating that the move was successful.</returns>
        public static bool MoveFile(string fileToMove, string destinationFolder, bool createDestinationFolder)
        {
            // Verify if icome file still exists
            if (File.Exists(fileToMove))
            {
                if (!Directory.Exists(destinationFolder) && createDestinationFolder)
                    Directory.CreateDirectory(destinationFolder);

                var fInfo = new FileInfo(fileToMove);
                // Move from income to success
                fInfo.MoveTo(PathHelper.ValidateEndOfPath(destinationFolder) + PathHelper.GetFileNameWithExtension(fileToMove));
            }
            return true;
        }
        /// <summary>
        /// Moves a folder to another destination.
        /// </summary>
        /// <param name="folderToMove">The folder to be moved.</param>
        /// <param name="destinationFolder">The destination folder to where the folder should be moved.</param>
        /// <param name="createDestinationFolder">Defines if the destination folder must be created if it doesn't exist.</param>
        /// <returns>A boolean indicating that the move was successful.</returns>
        public static bool MoveFolder(string folderToMove, string destinationFolder, bool createDestinationFolder)
        {
            // Verify if icome file still exists
            if (Directory.Exists(folderToMove))
            {
                //if (!Directory.Exists(destinationFolder) && createDestinationFolder)
                //    Directory.CreateDirectory(destinationFolder);

                var dInfo = new DirectoryInfo(folderToMove);
                // Move from income to success
                dInfo.MoveTo(PathHelper.ValidateEndOfPath(destinationFolder));
            }
            return true;
        }
        /// <summary>
        /// Copies a folder to another destination.
        /// </summary>
        /// <param name="fileToCopy">The folder to be copied.</param>
        /// <param name="destinationFolder">The destination folder to where the folder should be copied to.</param>
        /// <param name="createDestinationFolder">Defines if the destination folder must be created if it doesn't exist.</param>
        /// <param name="overwrite">Defines if the copy will overwrite existing files in the destination folder.</param>
        /// <returns>A boolean indicating that the copy was successful.</returns>
        public static bool CopyFile(string fileToCopy, string destinationFolder, bool createDestinationFolder, bool overwrite)
        {
            // Verify if icome file still exists
            if (File.Exists(fileToCopy))
            {
                if (!Directory.Exists(destinationFolder) && createDestinationFolder)
                    Directory.CreateDirectory(destinationFolder);

                var fInfo = new FileInfo(fileToCopy);
                // Copy from income to success
                fInfo.CopyTo(PathHelper.ValidateEndOfPath(destinationFolder) + PathHelper.GetFileNameWithExtension(fileToCopy), overwrite);
            }
            return true;
        }
    }
}
