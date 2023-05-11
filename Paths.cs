using System.Reflection;

namespace dotnet_bot_accountant
{
    public static class Paths
    {
        public static string LogFolder { get; private set; }

        public static string CurrentPath { get; private set; }

        public static void MakePaths()
        {
            CurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            LogFolder = Path.Combine(CurrentPath, "logs");
        }

        public static string GetLogFilePath() => Path.Combine(LogFolder, "log-.txt");
    }
}
