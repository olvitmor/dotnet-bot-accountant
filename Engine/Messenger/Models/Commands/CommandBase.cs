using Telegram.Bot;
using Telegram.Bot.Types;

namespace dotnet_bot_accountant.Engine.Messenger.Models.Commands;

public abstract class CommandBase
{
    public string CommandName { get; set; }

    public string Description { get; set; }

    public bool IsEnabled { get; set; }

    public abstract async Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

}
