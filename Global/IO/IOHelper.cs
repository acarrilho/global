using System;
using System.IO;

namespace Global.IO
{
    /// <summary>
    ///  Performs input/output actions.
    /// </summary>
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
            string result;
            using (TextReader txtReader = new StreamReader(filePath))
            {
                result = txtReader.ReadToEnd();
            }
            return result;
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
            if (File.Exists(fileToMove))
            {
                if (!Directory.Exists(destinationFolder) && createDestinationFolder)
                {
                    Directory.CreateDirectory(destinationFolder);
                }
                var fInfo = new FileInfo(fileToMove);
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
            if (Directory.Exists(folderToMove))
            {
                var dInfo = new DirectoryInfo(folderToMove);
                dInfo.MoveTo(PathHelper.ValidateEndOfPath(destinationFolder));
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
        public static bool CopyFolder(string folderToMove, string destinationFolder, bool createDestinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            if (Directory.Exists(folderToMove))
            {
                var files = Directory.GetFiles(folderToMove);

                // Copy the files and overwrite destination files if they already exist.
                foreach (var s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    var fileName = Path.GetFileName(s);
                    if (fileName != null)
                    {
                        var destFile = Path.Combine(folderToMove, fileName);
                        File.Copy(s, destFile, true);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Copies a directory from one place to another.
        /// </summary>
        /// <param name="source">The source folder.</param>
        /// <param name="destination">The destination folder.</param>
        /// <param name="overwrite">A flag indicating if it is to overwrite the folder if it exists.</param>
        /// <exception cref="DirectoryNotFoundException">Throws an exception if the copy is not successful.</exception>
        public static void CopyDirectory(string source, string destination, bool overwrite)
        {
            // Hold directory information
            var sourceDirectory = new DirectoryInfo(source);
            var destinationDirectory = new DirectoryInfo(destination);

            // Throw an error is the source directory does not exist
            if (sourceDirectory.Exists == false)
            {
                throw new DirectoryNotFoundException();
            }

            // Create the destination directory
            if (!destinationDirectory.Exists)
            {
                destinationDirectory.Create();
            }

            // Loop through the files and copy them
            var subFiles = sourceDirectory.GetFiles();
            foreach (var t in subFiles)
            {
                var newFile = Path.Combine(destinationDirectory.FullName, t.Name);
                t.CopyTo(newFile, overwrite);
            }

            // Loop through the directories and call this function
            var subDirectories = sourceDirectory.GetDirectories();
            foreach (DirectoryInfo t in subDirectories)
            {
                var newDirectory = Path.Combine(destinationDirectory.FullName, t.Name);
                CopyDirectory(t.FullName, newDirectory, overwrite);
            }
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
            if (File.Exists(fileToCopy))
            {
                if (!Directory.Exists(destinationFolder) && createDestinationFolder)
                {
                    Directory.CreateDirectory(destinationFolder);
                }
                var fInfo = new FileInfo(fileToCopy);
                fInfo.CopyTo(PathHelper.ValidateEndOfPath(destinationFolder) + PathHelper.GetFileNameWithExtension(fileToCopy), overwrite);
            }
            return true;
        }
    }
}
