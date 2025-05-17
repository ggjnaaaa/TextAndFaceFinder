namespace LR3.Services
{
    public static class TempFolderCopier
    {
        private static string _cachedTempPath;

        /// <summary>
        /// Копирует указанную папку из исходного каталога в уникальную папку в %TEMP%,
        /// если она еще не скопирована, и возвращает путь к ней.
        /// </summary>
        /// <param name="sourceFolderName">Имя папки в каталоге приложения</param>
        /// <returns>Путь к скопированной папке в %TEMP%</returns>
        public static string GetOrCopyToTemp(string sourceFolderName)
        {
            if (!string.IsNullOrEmpty(_cachedTempPath))
                return _cachedTempPath;

            string appBase = AppDomain.CurrentDomain.BaseDirectory;
            string sourcePath = Path.Combine(appBase, sourceFolderName);

            if (!Directory.Exists(sourcePath))
                throw new DirectoryNotFoundException($"Source folder '{sourcePath}' not found.");

            string tempBase = Path.Combine(Path.GetTempPath(), "MyApp", sourceFolderName);

            if (!Directory.Exists(tempBase) || IsFolderEmpty(tempBase))
            {
                CopyDirectory(sourcePath, tempBase);
            }

            _cachedTempPath = tempBase;
            return _cachedTempPath;
        }

        private static bool IsFolderEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        private static void CopyDirectory(string sourceDir, string destinationDir)
        {
            Directory.CreateDirectory(destinationDir);

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destinationDir, Path.GetFileName(dir));
                CopyDirectory(dir, destSubDir);
            }
        }
    }
}
