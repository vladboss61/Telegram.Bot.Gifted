using Microsoft.Extensions.Configuration;
using Telegram.Bot.Gifted.Core;

namespace Telegram.Bot.Gifted;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var startUp = new Startup(configuration);
        await startUp.ConfigureAsync();
        await startUp.Run();
        var waiter = new TaskCompletionSource();
        Console.CancelKeyPress += (obj, consoleArgs) =>
        {
            waiter.SetResult();
        };

        await waiter.Task;
    }
}
