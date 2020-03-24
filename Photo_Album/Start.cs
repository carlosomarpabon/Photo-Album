using System;
using System.Threading.Tasks;

namespace Photo_Album
{
    class Start
    {
        static void Main(string[] args)
        {
            var serviceProvider = new AppServiceProvider();
            var program = serviceProvider.GetService<IProgram>();
            program.Run();
        }
    }
}
