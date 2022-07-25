namespace Telegram.Bot.Gifted.Core.Commands;

using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

internal sealed class StickerBotCommand : IBotCommand
{
    private const string StickerUrl = "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-dali.webp";
    private readonly ITelegramBotClient _botClient;

    public StickerBotCommand(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public MessageType MessageType => MessageType.Sticker;

    public async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
    {
        await _botClient.SendStickerAsync(
            chatId: new ChatId(message.Chat.Id),
            StickerUrl,
            cancellationToken: cancellationToken);
    }
}
