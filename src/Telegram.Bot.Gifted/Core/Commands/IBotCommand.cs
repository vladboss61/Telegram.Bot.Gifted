namespace Telegram.Bot.Gifted.Core.Commands;

using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public interface IBotCommand
{
    public MessageType MessageType { get; }

    public Task ExecuteAsync(Message message, CancellationToken cancellationToken);
}
