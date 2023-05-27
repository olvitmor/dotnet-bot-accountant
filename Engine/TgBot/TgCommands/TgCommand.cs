using Telegram.Bot;
using Telegram.Bot.Types;

namespace dotnet_bot_accountant.Engine.TgBot.TgCommands
{
    public class TgCommand
    {
        public string Name { get; set; }

        public TgCommand(string name)
        {
            Name = name;
        }

        public virtual async Task Handle(Message msg, ITelegramBotClient botClient, Update update)
        {

        }
    }
}
