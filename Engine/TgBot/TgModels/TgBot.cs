using dotnet_bot_accountant.Engine.Enums;
using dotnet_bot_accountant.Engine.TgBot.TgCommands;
using dotnet_bot_accountant.Extensions;
using Serilog;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace dotnet_bot_accountant.Engine.TgBot.TgModels;

public class TelegramBot : IDisposable
{
    #region Properties
    public string Token { get; private set; }
    public Telegram.Bot.Types.User BotInfo { get; private set; }

    public BotState State { get; private set; }
    #endregion

    #region Fields
    private CancellationTokenSource _receiveTokenSource;
    private readonly Serilog.ILogger _logger = Log.Logger;
    private TelegramBotClient _client;
    private ConcurrentDictionary<string, TgCommand> _commands = new ConcurrentDictionary<string, TgCommand>();

    #endregion

    #region Constructors
    public TelegramBot(string token)
    {
        Token = token;
        _client = new TelegramBotClient(token);
        State = BotState.Initialized;
        MakeCommands();
    }
    #endregion

    #region Methods

    private void MakeCommands()
    {
        var commands = new List<TgCommand>()
        {
            new CommandStart()
        };

        foreach(var cmd in commands)
        {
            _commands[cmd.Name] = cmd;
        }
    }

    public void StartReceive()
    {
        _receiveTokenSource = new CancellationTokenSource();

        ReceiverOptions options = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        _logger.LogInfo("starting telegram bot ...");

        _client.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: options,
            cancellationToken: _receiveTokenSource.Token);

        State = BotState.Running;
    }

    public void StopReceive()
    {
        if (!_receiveTokenSource.IsCancellationRequested)
        {
            _receiveTokenSource.Cancel();

            _logger.Information("bot stopped");

            State = BotState.Stopped;
        }
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return;

        if (update.Message is not { } message)
            return;

        if (message.Text is not { } messageText)
            return;

        if (_commands.TryGetValue(messageText, out var command))
        {
            await command.Handle(message, botClient, update);
        }
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
        State = BotState.Disposed;
    }

    #endregion
}
