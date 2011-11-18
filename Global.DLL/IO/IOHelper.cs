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
            FileInfo fInfo = new FileInfo(filePath);
            return fInfo.LastWriteTime;
        }
        /// <summary>
        /// Get the last modified date of the specified folder.
        /// </summary>
        /// <param name="folderPath">The full folder path.</param>
        /// <returns>The last modified DateTime.</returns>
        public static DateTime GetFolderLastModified(string folderPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(folderPath);
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
                FileInfo fInfo = new FileInfo(fileToMove);
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
                DirectoryInfo dInfo = new DirectoryInfo(folderToMove);
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
                    var destFile = Path.Combine(folderToMove, fileName);
                    File.Copy(s, destFile, true);
                }
            }
            return true;
        }

        public static void CopyDirectory(String Source, String Destination, Boolean Overwrite)
        {
            // Hold directory information
            var SourceDirectory = new DirectoryInfo(Source);
            var DestinationDirectory = new DirectoryInfo(Destination);

            // Throw an error is the source directory does not exist
            if (SourceDirectory.Exists == false)
            {
                throw new DirectoryNotFoundException();
            }

            // Create the destination directory
            if (DestinationDirectory.Exists == false)
            {
                DestinationDirectory.Create();
            }

            // Loop through the files and copy them
            var SubFiles = SourceDirectory.GetFiles();
            for (int i = 0; i < SubFiles.Length; i++)
            {
                var NewFile = Path.Combine(
                        DestinationDirectory.FullName,
                        SubFiles[i].Name
                );
                SubFiles[i].CopyTo(NewFile, Overwrite);
            }

            // Loop through the directories and call this function
            var SubDirectories = SourceDirectory.GetDirectories();
            for (int i = 0; i < SubDirectories.Length; i++)
            {
                var NewDirectory = Path.Combine(
                        DestinationDirectory.FullName,
                        SubDirectories[i].Name
                );
                CopyDirectory(SubDirectories[i].FullName, NewDirectory, Overwrite);
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
                FileInfo fInfo = new FileInfo(fileToCopy);
                fInfo.CopyTo(PathHelper.ValidateEndOfPath(destinationFolder) + PathHelper.GetFileNameWithExtension(fileToCopy), overwrite);
            }
            return true;
        }
    }
}
