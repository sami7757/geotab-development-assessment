using System;
using System.Threading.Tasks;
using JokeGenerator.Controllers;
using JokeGenerator.Services;
using JokeGenerator.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace GeoTabConsoleApp
{
    class Program
    {
        static async Task Main()
        {
            var serviceProvider = new ServiceCollection()
           .AddSingleton<IJokesController, JokesController>()
           .AddSingleton<IJokeService, JokeService>()
           .AddSingleton<IConsoleIOService, ConsoleIOService>()
           .AddSingleton<IJsonFeedProvider, JsonFeedProvider>()
           .BuildServiceProvider();

            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            await serviceProvider.GetService<IJokesController>().StartJokes();
        }

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
