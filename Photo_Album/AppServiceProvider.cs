using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Photo_Album
{
    public class AppServiceProvider
    {
        private readonly ServiceProvider _serviceProvider;
        public AppServiceProvider()
        {
            var serviceCollection = new ServiceCollection()
                .AddLogging(c => c.AddConsole())
                .AddSingleton<IAlbumService, AlbumService>()
                .AddSingleton<IInputValidator, InputValidator>()
                .AddSingleton<IConsoleService, ConsoleService>()
                .AddSingleton<IProgram, Program>();
            serviceCollection
                .AddHttpClient<IAlbumService, AlbumService>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
