using dotnet_bot_accountant.Extensions;
using Serilog;
using System.Runtime.CompilerServices;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace dotnet_bot_accountant.Engine.Messenger.Models;

public class TelegramBot : IDisposable
{
    #region Fields

    private readonly string _token;

    private TelegramBotClient _client;

    public Telegram.Bot.Types.User BotInfo { get; private set; }

    private CancellationTokenSource _receiveTokenSource;

    #endregion

    #region Constructors
    public TelegramBot(string token)
    {
        _token = token;
        _client = new TelegramBotClient(token);
    }
    #endregion

    #region Methods

    public void StartReceive()
    {
        _receiveTokenSource = new CancellationTokenSource();

        ReceiverOptions options = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        Log.Information("starting bot");

        _client.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: options,
            cancellationToken: _receiveTokenSource.Token);
    }

    public void StopReceive()
    {
        if (!_receiveTokenSource.IsCancellationRequested)
        {
            _receiveTokenSource.Cancel();

            Log.Information("bot stopped");
        }
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Only process Message updates: https://core.telegram.org/bots/api#message
        if (update.Message is not { } message)
            return;
        // Only process text messages
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;

        Log.Logger.LogInfo($"Received a '{messageText}' message in chat {chatId}.");

        // Echo received message text
        Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "You said:\n" + messageText,
            cancellationToken: cancellationToken);
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }

    public async void UpdateBotInfo()
    {
        var botInfo = await _client.GetMeAsync();

        BotInfo = botInfo;
    }

    public void Dispose()
    {
        _client?.CloseAsync().Wait();
    }

    #endregion
}
