using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PrepRelease
{
    internal class Program
    {
        static int Main(string[] args)
        {
            // Parse the desired version
            if (args == null || args.Length != 1 || !Version.TryParse(args[0], out Version version))
            {
                Console.WriteLine("Expected exactly one argument in version format, e.g. \"1.2.34.5\".");
                return 1;
            }

            // Find the root of the project
            string root = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
            Console.WriteLine($"Detected project root to be \"{root}\"");

            // Update the version in AssemblyInfo.cs
            ReplaceVersionInFiles(root, "AssemblyInfo.cs", "Version\\(\"\\d+?\\.\\d+?\\.\\d+?\\.\\d+?\"\\)", $"Version(\"{version.ToString()}\")");

            // Update the version in Setup.vdproj
            ReplaceVersionInFiles(root, "Setup.vdproj", "Setup_\\d+?_\\d+?_\\d+?_\\d+?.msi", $"Setup_{version.ToString().Replace('.', '_')}.msi");
            ReplaceVersionInFiles(root, "Setup.vdproj", "\"ProductVersion\" = \"8:\\d+?\\.\\d+?\\.\\d+?\"", $"\"ProductVersion\" = \"8:{version.ToString(3)}\"");

            return 0;
        }

        private static void ReplaceVersionInFiles(string root, string fileNameMask, string regex, string version)
        {
            string[] files = Directory.GetFiles(root, fileNameMask, SearchOption.AllDirectories);
            foreach (string filePath in files)
            {
                string content = File.ReadAllText(filePath);
                Regex.Replace(content, regex, version.ToString());
                File.WriteAllText(filePath, content);
                Console.WriteLine($"Patched version to {version.ToString()} in \"{filePath}\"");
            }
        }
    }
}
