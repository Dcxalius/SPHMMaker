using System;
using System.IO;
using System.IO.Compression;

namespace SPHMMaker
{
    internal static class DatapackArchive
    {
        public static (string ExtractionRoot, string DatapackRoot) ExtractToTemporaryDirectory(string archivePath)
        {
            if (!File.Exists(archivePath))
            {
                throw new FileNotFoundException("Archive not found.", archivePath);
            }

            string extractionRoot = Path.Combine(Path.GetTempPath(), "SPHMMaker", "datapacks", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(extractionRoot);

            ZipFile.ExtractToDirectory(archivePath, extractionRoot);

            string? datapackRoot = FindDatapackRoot(extractionRoot);
            if (datapackRoot is null)
            {
                throw new InvalidDataException("The archive does not contain an Items directory.");
            }

            return (extractionRoot, datapackRoot);
        }

        public static void CreateArchive(string sourceDirectory, string destinationArchivePath)
        {
            if (!Directory.Exists(sourceDirectory))
            {
                throw new DirectoryNotFoundException($"Source directory not found: {sourceDirectory}");
            }

            if (File.Exists(destinationArchivePath))
            {
                File.Delete(destinationArchivePath);
            }

            ZipFile.CreateFromDirectory(sourceDirectory, destinationArchivePath, CompressionLevel.Optimal, includeBaseDirectory: false);
        }

        static string? FindDatapackRoot(string directory)
        {
            if (Directory.Exists(Path.Combine(directory, "Items")))
            {
                return directory;
            }

            foreach (string child in Directory.GetDirectories(directory))
            {
                string? candidate = FindDatapackRoot(child);
                if (candidate != null)
                {
                    return candidate;
                }
            }

            return null;
        }
    }
}
