using System;
using System.Threading.Tasks;

namespace Photo_Album
{
    public interface IProgram
    {
        void Run();
    }
    public class Program : IProgram
    {
        private readonly IInputValidator _inputValidator;
        private readonly IAlbumService _albumService;
        private readonly IConsoleService _consoleService;

        public Program(IInputValidator inputValidator, IAlbumService albumService, IConsoleService consoleService)
        {
            _inputValidator = inputValidator;
            _albumService = albumService;
            _consoleService = consoleService;
        }

        public void Run()
        {
            _consoleService.WriteLine(Constants.WELCOME);
            var isDone = false;
            while (!isDone)
            {
                _consoleService.WriteLine(Constants.ASK_FOR_ID);
                var input = _consoleService.ReadLine();
                var inputResult = _inputValidator.IsInt(input);
                if (inputResult.IsValid)
                {
                    var task = Task.Run(async () => await _albumService.GetPhotosByAlbumId(inputResult.OutputNumber));
                    var photos = task.Result;
                    foreach (var p in photos)
                    {
                        _consoleService.WriteLine(string.Format(Constants.PHOTO_RECORD, p.Id, p.Title));
                    }
                }
                else
                {
                    _consoleService.WriteLine(inputResult.Error);
                }
                _consoleService.WriteLine(Constants.ASK_IF_CONTINUE);
                input = _consoleService.ReadLine();
                if (input.ToUpper() == Constants.NO)
                {
                    isDone = true;
                }
            }
        }
    }
}
