using System;
using System.IO;
using System.Reflection;

namespace AudioScript.Helpers
{
    public static class Folders
    {
        private static string GetAssemblyPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().Location;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path)!;
        }

        public static string GetScriptFolder()
        {
            var path = GetAssemblyPath();
            return Path.Combine(path, "Scripts");
        }
    }
}
