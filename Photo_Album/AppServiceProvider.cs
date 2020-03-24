using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Photo_Album
{
    public class AppServiceProvider
    {
        private readonly ServiceProvider _serviceProvider;
        public AppServiceProvider()
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging(c =>c.AddConsole())
                .AddSingleton<IAlbumService, AlbumService>()
                .AddSingleton<IInputValidator, InputValidator>()
                .AddSingleton<IConsoleService, ConsoleService>()
                .AddSingleton<IProgram, Program>()
                .BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
