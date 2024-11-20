using ConsoleRpg.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRpg;

public static class Program
{
    private static void Main(string[] args)
    {
        // Setup DI container
        var serviceCollection = new ServiceCollection();
        Startup.ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Get the GameEngine and run it
        var gameEngine = serviceProvider.GetService<GameEngine>();
        gameEngine?.Run();
    }
}

