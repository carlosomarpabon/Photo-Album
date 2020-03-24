using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Photo_Album;
using Photo_Album.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Photo_Album_Tests
{
    [TestClass]
    public class ProgramTests
    {
        private Program _program;
        private Mock<IAlbumService> _mockAlbumService;
        private Mock<IInputValidator> _mockInputValidator;
        private Mock<IConsoleService> _mockConsoleService;
        private List<Photo> _validPhotos;
        private const string VALID_INPUT = "123";
        private const string INVALID_INPUT = "INVALID";
        
        [TestInitialize]
        public void Setup()
        {
            _mockInputValidator = new Mock<IInputValidator>();
            _mockInputValidator.Setup(x => x.IsInt(VALID_INPUT))
                .Returns(new InputValResult()
                {
                    IsValid = true,
                    Error = string.Empty,
                    OutputNumber = int.Parse(VALID_INPUT)
                });
            _mockInputValidator.Setup(x => x.IsInt(INVALID_INPUT))
                .Returns(new InputValResult()
                {
                    IsValid = false,
                    Error = Constants.ERROR_IS_NOT_NUMBER
                });
            _validPhotos = GetValidPhotos();
            _mockAlbumService = new Mock<IAlbumService>();
            _mockAlbumService.Setup(x => x.GetPhotosByAlbumId(int.Parse(VALID_INPUT)))
                .Returns(Task.FromResult(_validPhotos));

            _mockConsoleService = new Mock<IConsoleService>();

            _program = new Program(_mockInputValidator.Object, _mockAlbumService.Object, _mockConsoleService.Object);
        }

        [TestMethod]
        public void Run_RunsAllTheWayToExit()
        {
            _mockConsoleService.SetupSequence(x => x.ReadLine())
                .Returns(VALID_INPUT)
                .Returns(Constants.NO);

            _program.Run();

            _mockConsoleService.Verify(x => x.WriteLine(Constants.WELCOME));
            _mockConsoleService.Verify(x => x.WriteLine(Constants.ASK_FOR_ID));
            _mockInputValidator.Verify(x => x.IsInt(VALID_INPUT));
            _mockAlbumService.Verify(x => x.GetPhotosByAlbumId(int.Parse(VALID_INPUT)));
            foreach (var p in _validPhotos)
            {
                _mockConsoleService.Verify(x => x.WriteLine(string.Format(Constants.PHOTO_RECORD, p.Id, p.Title)));
            }
            _mockConsoleService.Verify(x => x.WriteLine(Constants.ASK_IF_CONTINUE));
        }

        [TestMethod]
        public void Run_PrintsError_When_NonIntInput()
        {
            _mockConsoleService.SetupSequence(x => x.ReadLine())
                .Returns(INVALID_INPUT)
                .Returns(Constants.NO);

            _program.Run();

            _mockConsoleService.Verify(x => x.WriteLine(Constants.WELCOME));
            _mockConsoleService.Verify(x => x.WriteLine(Constants.ASK_FOR_ID));
            _mockInputValidator.Verify(x => x.IsInt(INVALID_INPUT));
            _mockConsoleService.Verify(x => x.WriteLine(Constants.ERROR_IS_NOT_NUMBER));
            _mockConsoleService.Verify(x => x.WriteLine(Constants.ASK_IF_CONTINUE));
        }

        private List<Photo> GetValidPhotos()
        {
            const string STRING = "STRING";
            return new List<Photo>()
            {
                new Photo()
                {
                    Id = 1,
                    AlbumId = int.Parse(VALID_INPUT),
                    ThumbnailUrl = STRING,
                    Title = STRING,
                    Url = STRING
                },
                new Photo()
                {
                    Id = 2,
                    AlbumId = int.Parse(VALID_INPUT),
                    ThumbnailUrl = STRING,
                    Title = STRING,
                    Url = STRING
                },
            };
        }
    }
}
