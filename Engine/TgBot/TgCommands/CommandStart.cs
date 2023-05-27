using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace dotnet_bot_accountant.Engine.TgBot.TgCommands;

public class CommandStart : TgCommand
{
    private static Serilog.ILogger _logger = Log.Logger;

    public CommandStart() : base("/start")
    {

    }

    public override async Task Handle(Message msg, ITelegramBotClient botClient, Update update)
    {
        if (msg == null)
            return;

        await botClient.SendTextMessageAsync(
            msg.Chat.Id,
            $"Hello! Enter your name"
            );
    }
}
