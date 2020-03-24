using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Photo_Album;
using Photo_Album.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Photo_Album_Tests
{
    [TestClass]
    public class AlbumServiceTests
    {
        private AlbumService _albumService;
        private const int VALID_ALBUM_ID = 4;
        private const int INVALID_ALBUM_ID = 123456;
        [TestInitialize]
        public void Setup()
        {
            var loggerMock = new Mock<ILogger<AlbumService>>();
            _albumService = new AlbumService(loggerMock.Object);
        }

        [TestMethod]
        public void GetPhotosByAlbumId_Returns_CorrectValues()
        {
            var task = Task.Run(async () => await _albumService.GetPhotosByAlbumId(VALID_ALBUM_ID));
            var result = task.Result;

            Assert.IsTrue(result.Count == 50);

            AssertPhoto(result[0],
                4,
                151,
                "possimus dolor minima provident ipsam",
                "https://via.placeholder.com/600/1d2ad4",
                "https://via.placeholder.com/150/1d2ad4");

            AssertPhoto(result[1],
                4,
                152,
                "et accusantium enim pariatur eum nihil fugit",
                "https://via.placeholder.com/600/a01c5b",
                "https://via.placeholder.com/150/a01c5b");

            AssertPhoto(result[45],
                4,
                196,
                "amet maiores ut",
                "https://via.placeholder.com/600/128151",
                "https://via.placeholder.com/150/128151");
        }

        [TestMethod]
        public void GetPhotosByAlbumId_Returns_EmptyList_WhenNoneExistForId()
        {
            var task = Task.Run(async () => await _albumService.GetPhotosByAlbumId(INVALID_ALBUM_ID));
            var result = task.Result;

            Assert.IsTrue(result.Count == 0);
        }

        private void AssertPhoto(Photo photo, int albumId, int id, string title, string url, string thumbnailUrl)
        {
            Assert.IsTrue(photo.AlbumId == albumId);
            Assert.IsTrue(photo.Id == id);
            Assert.IsTrue(photo.Title == title);
            Assert.IsTrue(photo.Url == url);
            Assert.IsTrue(photo.ThumbnailUrl == thumbnailUrl);
        }
    }
}
