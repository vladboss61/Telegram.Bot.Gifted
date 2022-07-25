namespace Telegram.Bot.Gifted.Core;

using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Gifted.Core.Commands;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

public sealed class GiftedUpdateHandler : IUpdateHandler
{
    private readonly IServiceProvider _serviceProvider;

    public GiftedUpdateHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update?.Message is null)
        {
            return;
        }

        using var scope = _serviceProvider.CreateScope();
        var commands = scope.ServiceProvider.GetServices<IBotCommand>();

        IBotCommand command = commands.SingleOrDefault(x => x.MessageType == update.Message.Type);

        if (command is null)
        {
            Console.WriteLine($"Message: Bot can not process command {update.Message.Type}.");
            return;
        }

        await command.ExecuteAsync(update, cancellationToken);

        Console.WriteLine("Message: HandleUpdateAsync is executed.");
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine("Message: Something went wrong.");
        throw exception;
    }
}
