using System;
using System.Collections.Generic;
using System.IO;
using SPHMMaker.Items;
using SPHMMaker.Tiles;

namespace SPHMMaker.Datapacks
{
    internal static class SaveManager
    {
        private static readonly IReadOnlyList<(string Subfolder, Func<string, bool> SaveFunc)> SaveTargets = new List<(string, Func<string, bool>)>
        {
            ("Items", ItemManager.Save),
            ("Tiles", TileManager.Save),
        };

        public static void SaveToDirectory(string destinationPath)
        {
            if (string.IsNullOrWhiteSpace(destinationPath))
            {
                throw new ArgumentException("Destination path cannot be null or whitespace.", nameof(destinationPath));
            }

            Directory.CreateDirectory(destinationPath);

            bool anySaved = false;
            foreach (var target in SaveTargets)
            {
                string subfolderPath = Path.Combine(destinationPath, target.Subfolder);
                RemoveExistingDirectory(subfolderPath);
                Directory.CreateDirectory(subfolderPath);

                bool saved = target.SaveFunc(subfolderPath);
                anySaved |= saved;
            }

            if (!anySaved)
            {
                throw new InvalidOperationException("There is no data to save.");
            }
        }

        public static void SaveToArchive(string destinationArchivePath)
        {
            if (string.IsNullOrWhiteSpace(destinationArchivePath))
            {
                throw new ArgumentException("Destination archive path cannot be null or whitespace.", nameof(destinationArchivePath));
            }

            string? archiveDirectory = Path.GetDirectoryName(destinationArchivePath);
            if (!string.IsNullOrEmpty(archiveDirectory))
            {
                Directory.CreateDirectory(archiveDirectory);
            }

            string tempRoot = Path.Combine(Path.GetTempPath(), "SPHMMaker", "export", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempRoot);

            bool anySaved = false;
            try
            {
                foreach (var target in SaveTargets)
                {
                    string subfolderPath = Path.Combine(tempRoot, target.Subfolder);
                    Directory.CreateDirectory(subfolderPath);

                    bool saved = target.SaveFunc(subfolderPath);
                    anySaved |= saved;
                }

                if (!anySaved)
                {
                    throw new InvalidOperationException("There is no data to save.");
                }

                DatapackArchive.CreateArchive(tempRoot, destinationArchivePath);
            }
            finally
            {
                TryDeleteDirectory(tempRoot);
            }
        }

        private static void RemoveExistingDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recursive: true);
            }
            else if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private static void TryDeleteDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, recursive: true);
                }
            }
            catch
            {
                // Ignore cleanup failures.
            }
        }
    }
}
