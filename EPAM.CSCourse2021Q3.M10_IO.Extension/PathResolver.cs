using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace EPAM.CSCourse2021Q3.M10_IO.Extension
{
    public static class PathResolver
    {
        public static string CurrentDirectory()
        {
            return Environment.CurrentDirectory;
        }
        public static string GetDirectory(this string filepath)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filepath);
            return fileInfo.Directory.FullName;
        }
        public static bool DirectoryExists(this String str)
        {
            return System.IO.Directory.Exists(str);
        }
        public static bool FileExists(this String str)
        {
            return System.IO.File.Exists(str);
        }
    }
}
