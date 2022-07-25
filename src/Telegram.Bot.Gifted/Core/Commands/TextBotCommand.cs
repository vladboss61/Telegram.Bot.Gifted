namespace Telegram.Bot.Gifted.Core.Commands;

using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

internal sealed class TextBotCommand : IBotCommand
{
    private readonly ITelegramBotClient _botClient;

    public TextBotCommand(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public MessageType MessageType => MessageType.Text;

    public async Task ExecuteAsync(Update update, CancellationToken cancellationToken)
    {
        await using var stream = System.IO.File.OpenRead(@$"{Directory.GetCurrentDirectory()}/Resources/default-audio-response.ogg");

        await _botClient.SendVoiceAsync(
            chatId: new ChatId(update.Message!.Chat.Id),
            voice: stream,
            duration: 36,
            cancellationToken: cancellationToken);
    }
}
