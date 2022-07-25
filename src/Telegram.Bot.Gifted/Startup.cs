using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Gifted.Core;
using Telegram.Bot.Gifted.Core.Commands;
using Telegram.Bot.Polling;

namespace Telegram.Bot.Gifted;
internal class Startup
{
    private readonly IServiceCollection _services;
    private readonly IConfiguration _configuration;

    private readonly BotConfiguration _botConfiguration;
    private readonly TelegramBotClient _telegramBot;


    public Startup(IConfiguration configuration)
    {
        _services = new ServiceCollection();
        _configuration = configuration;

        _botConfiguration = configuration.Get<BotConfiguration>();
        _telegramBot = new TelegramBotClient(_botConfiguration.BotToken);
    }

    public Task ConfigureAsync() => Task.Run(() =>
    {
        _services.AddSingleton(_configuration);
        _services.AddSingleton<ITelegramBotClient>(_telegramBot);
        _services.AddSingleton<IUpdateHandler, GiftedUpdateHandler>();

        _services.AddScoped<IBotCommand, TextBotCommand>();
        _services.AddScoped<IBotCommand, StickerBotCommand>();

        return Task.CompletedTask;
    });

    public Task Run() => Task.Run(() =>
    {

        _telegramBot.StartReceiving(_services
            .BuildServiceProvider()
            .GetRequiredService<IUpdateHandler>());
    });
}
