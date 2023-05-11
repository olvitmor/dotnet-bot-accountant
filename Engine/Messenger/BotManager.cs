using dotnet_bot_accountant.Engine.Messenger.Models;
using dotnet_bot_accountant.Extensions;
using Serilog;
using Telegram.Bot;

namespace dotnet_bot_accountant.Engine.Messenger;

public class BotManager
{
    #region Fields

    private static TelegramBot _bot;

    private const string _botToken = "6015831086:AAH8cZJzwiCJUcmEyc_tOyI5EilqFdKrJ1s";

    #endregion

    #region Methods

    public static void Init()
    {
        _bot = new TelegramBot(_botToken);

        Task.Run(() => StartBot());
    }

    public static void StartBot()
    {
        _bot.StartReceive();
    }

    public static void StopBot()
    {
        _bot.StopReceive();
    }

    #endregion
}
