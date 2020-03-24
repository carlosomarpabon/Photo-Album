using System;

namespace Photo_Album
{
    public interface IConsoleService
    {
        string ReadLine();
        void WriteLine(string input);
    }
    public class ConsoleService : IConsoleService
    {
        
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string input)
        {
            Console.WriteLine(input);
        }
    }
}
