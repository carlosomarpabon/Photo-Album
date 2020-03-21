using System;
using System.Threading.Tasks;

namespace Photo_Album
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new AppServiceProvider();
            var inputValidator = serviceProvider.GetService<IInputValidator>();
            var albumService = serviceProvider.GetService<IAlbumService>();

            Console.WriteLine("Welcome to Photo Album!");
            while (true)
            {
                Console.WriteLine("Please enter the ID of the album you want:");
                var input = Console.ReadLine();
                var inputResult = inputValidator.IsInt(input);
                if (inputResult.IsValid)
                {
                    var task = Task.Run(async ()=> await albumService.GetPhotosByAlbumId(inputResult.OutputNumber));
                    var photos = task.Result;
                    foreach (var p in photos)
                    {
                        Console.WriteLine($"[{p.Id}] {p.Title}");
                    }
                }
                else
                {
                    Console.WriteLine(inputResult.Error);
                }
            }
        }
    }
}
