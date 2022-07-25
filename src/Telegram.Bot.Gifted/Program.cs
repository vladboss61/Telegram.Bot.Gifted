using Microsoft.Extensions.Configuration;

namespace Telegram.Bot.Gifted;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var startup = new Startup(configuration);

        await startup.ConfigureAsync();
        await startup.RunAsync();

        await WaitCtrlPlusC();
    }

    private static async Task WaitCtrlPlusC()
    {
        var waiter = new TaskCompletionSource();
        Console.CancelKeyPress += (obj, consoleArgs) =>
        {
            Console.WriteLine("Ctrl + C is called.");
            waiter.SetResult();
        };

        await waiter.Task;
        Console.WriteLine("Bot is stopped.");
    }
}
