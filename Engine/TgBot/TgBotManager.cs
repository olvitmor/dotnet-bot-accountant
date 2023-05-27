using dotnet_bot_accountant.Engine.Enums;
using dotnet_bot_accountant.Engine.Managers;
using dotnet_bot_accountant.Engine.TgBot.TgModels;
using dotnet_bot_accountant.Extensions;
using Serilog;
using Telegram.Bot;

namespace dotnet_bot_accountant.Engine.TgBot;

public class TgBotManager
{
    #region Fields

    private static Serilog.ILogger _logger = Log.Logger;

    private static TelegramBot _tgBot;

    #endregion

    #region Methods

    public static void Init()
    {
        ApplySettings("on startup");
    }

    public static void ApplySettings(string reason)
    {
        var botSettings = Shared.Settings.TgBot;

        if (_tgBot == null)
        {
            if (string.IsNullOrEmpty(botSettings.Token))
            {
                _logger.LogError($"bot enabled but token not provided");
                return;
            }

            _tgBot = new TelegramBot(botSettings.Token);
        }
        else
        {
            if (_tgBot.Token != botSettings.Token)
            {
                _tgBot.Dispose();
                _tgBot = new TelegramBot(botSettings.Token);
            }
        }

        _logger.LogInfo($"settings applied [{reason}]");

        if (!botSettings.Enabled && _tgBot.State != Enums.BotState.Stopped)
            StopTgBot();

        if (botSettings.Enabled && _tgBot.State != BotState.Running)
            StartTgBot();
    }

    public static void StartTgBot()
    {
        _tgBot.StartReceive();
    }

    public static void StopTgBot()
    {
        _tgBot.StopReceive();
    }

    #endregion
}
