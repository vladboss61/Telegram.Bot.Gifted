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

    public async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
    {
        await using var audioStream = System.IO.File.OpenRead(@$"{Directory.GetCurrentDirectory()}/Resources/default-audio-response.ogg");

        await _botClient.SendVoiceAsync(
            chatId: new ChatId(message.Chat.Id),
            voice: audioStream,
            cancellationToken: cancellationToken);
    }
}
