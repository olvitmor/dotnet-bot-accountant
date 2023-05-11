using System.Runtime.CompilerServices;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace dotnet_bot_accountant.Extensions;

public static class LogExtensions
{
    public const string GeneralLoggingTemplate = "[{Timestamp:HH:mm:ss:fff} {Level:u3}] at {SourceContext} {Message}{NewLine}{Exception}";

    public static void SetupLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug, outputTemplate: GeneralLoggingTemplate)
            .WriteTo.File(Paths.GetLogFilePath(), rollingInterval: RollingInterval.Day, outputTemplate: GeneralLoggingTemplate)
            .CreateLogger();
    }


    public static void LogError(
        this Serilog.ILogger logger,
        string msg,
        Exception ex = null,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int sourceLineNumber = 0,
        [CallerFilePath] string sourceFilePath = "")
    {
        var file = Path.GetFileName(sourceFilePath);

        logger.ForContext("SourceContext", $"[{file}]:[{memberName}]:[{sourceLineNumber}]")
            .Error(ex,"{Message}", msg);

    }

    public static void LogInfo(
        this Serilog.ILogger logger,
        string msg,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int sourceLineNumber = 0,
        [CallerFilePath] string sourceFilePath = "")
    {
        var file = Path.GetFileName(sourceFilePath);

        logger.ForContext("SourceContext", $"[{file}]:[{memberName}]:[{sourceLineNumber}]")
            .Information("{Message}", msg);
    }

    public static void LogWarn(
        this Serilog.ILogger logger,
        string msg,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int sourceLineNumber = 0,
        [CallerFilePath] string sourceFilePath = "")
    {
        var file = Path.GetFileName(sourceFilePath);

        logger.ForContext("SourceContext", $"[{file}]:[{memberName}]:[{sourceLineNumber}]")
            .Warning("{Message}", msg);
    }
}
