using System.Reflection;

namespace LightRssReader.BusinessLayer.Helpers
{
    public static class FileHelper
    {
        public static string GetPath(string path)
        {
            var s = FindPath(path);
            if (s == null)
                throw new Exception("Can't find path:" + path);
            return s;
        }

        public static string? FindPath(string path, string? rootPath = null)
        {
            rootPath = rootPath ?? Assembly.GetExecutingAssembly().Location;
            var relativePath = FindRelativePath(path, rootPath);
            if (relativePath != null)
                return Path.Combine(rootPath, relativePath);
            return null;
        }

        public static string? FindRelativePath(string path, string? rootPath = null)
        {
            rootPath = rootPath ?? Assembly.GetExecutingAssembly().Location;
            int iteration = 0;
            var prefix = "." + Path.DirectorySeparatorChar;
            while (true)
            {
                iteration++;
                if (iteration > 40)
                {
                    return null;
                }

                var combined = Path.Combine(rootPath, prefix, path);
                if (Directory.Exists(combined))
                    return Path.Combine(prefix, path);
                if (File.Exists(combined))
                    return Path.Combine(prefix, path);
                prefix = ".." + Path.DirectorySeparatorChar + prefix;
                if (prefix.Length > 40)
                    break;
            }

            return null;
        }
    }
}