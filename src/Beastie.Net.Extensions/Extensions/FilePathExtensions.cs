using System.IO;
using System.Web.Hosting;

namespace Beastie.Net.Extensions.Extensions
{
    public static class FilePathExtensions
    {
        /// <summary>
        /// The delete directory.
        /// </summary>
        /// <param name="directoryPath">
        /// The directory path.
        /// </param>
        /// <param name="recursive">
        /// The deep delete.
        /// </param>
        public static void DeleteDirectory(this string directoryPath, bool recursive = true)
        {
            Directory.Delete(directoryPath, recursive);
        }

        /// <summary>
        /// Determines whether the specified folder exists.
        /// </summary>
        /// <param name="virtualPath">
        /// The virtual path (absolute or relative).
        /// </param>
        /// <returns>
        /// True if the path contains the name of an existing folder; otherwise, false.
        /// </returns>
        public static bool DirectoryExists(this string virtualPath)
        {
            return !string.IsNullOrWhiteSpace(virtualPath) && HostingEnvironment.VirtualPathProvider.DirectoryExists(virtualPath);
        }

        /// <summary>
        /// Determines whether the specified file exists.
        /// </summary>
        /// <param name="virtualPath">
        /// The virtual path (absolute or relative).
        /// </param>
        /// <returns>
        /// True if the path contains the name of an existing file; otherwise, false.
        /// </returns>
        public static bool FileExists(this string virtualPath)
        {
            return !string.IsNullOrWhiteSpace(virtualPath) && HostingEnvironment.VirtualPathProvider.FileExists(virtualPath);
        }

        /// <summary>
        /// Maps a virtual path to a physical path on the server.
        /// </summary>
        /// <param name="virtualPath">
        /// The virtual path (absolute or relative).
        /// </param>
        /// <returns>
        /// The physical path on the server specified by virtualPath.
        /// </returns>
        public static string MapPath(this string virtualPath)
        {
            return !string.IsNullOrWhiteSpace(virtualPath) ? HostingEnvironment.MapPath(virtualPath) : string.Empty;
        }
    }
}