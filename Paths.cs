using System.Reflection;

namespace dotnet_bot_accountant
{
    public static class Paths
    {
        public static string LogFolder { get; private set; }

        public static string CurrentPath { get; private set; }

        public static string AppFolder { get; private set; }

        public static void MakePaths()
        {
            CurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            AppFolder = Path.Combine(CurrentPath, "app");
            LogFolder = Path.Combine(AppFolder, "logs");

            MakeFolders();
        }

        private static void MakeFolders()
        {
            if (!Directory.Exists(AppFolder))
                Directory.CreateDirectory(AppFolder);

            if (!Directory.Exists(LogFolder))
                Directory.CreateDirectory(LogFolder);
        }

        public static string GetSettingsFilePath() => Path.Combine(AppFolder, "Settings.xml");

        public static string GetLogFilePath() => Path.Combine(LogFolder, "log-.txt");
    }
}
