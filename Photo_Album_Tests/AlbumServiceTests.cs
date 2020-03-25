using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Photo_Album;
using Photo_Album.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Moq.Protected;
using System.Threading;
using System.Net;
using System;
using Newtonsoft.Json;

namespace Photo_Album_Tests
{
    [TestClass]
    public class AlbumServiceTests
    {
        private Mock<ILogger<AlbumService>> _mockLogger;

        private const int VALID_ALBUM_ID = 4;
        private const int INVALID_ALBUM_ID = 123456;
        private const string VALID_REASON_PHRASE = "ALL GOOD!";
        private const string INVALID_REASON_PHRASE = "ERROR";

        [TestInitialize]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<AlbumService>>();
        }

        [TestMethod]
        public void GetPhotosByAlbumId_Returns_CorrectValues()
        {
            var httpClient = GetHttpClient(HttpStatusCode.OK, GetValidResponseContent(), VALID_REASON_PHRASE);
            var albumService = new AlbumService(_mockLogger.Object, httpClient);

            var task = Task.Run(async () => await albumService.GetPhotosByAlbumId(VALID_ALBUM_ID));
            var result = task.Result;

            Assert.IsTrue(result.Count == 3);

            AssertPhoto(result[0], ExpectedPhotos[0]);
            AssertPhoto(result[1], ExpectedPhotos[1]);
            AssertPhoto(result[2], ExpectedPhotos[2]);
        }

        [TestMethod]
        public void GetPhotosByAlbumId_Returns_EmptyList_WhenNoneExistForId()
        {
            var httpClient = GetHttpClient(HttpStatusCode.OK, GetEmptyResponseContent(), VALID_REASON_PHRASE);
            var albumService = new AlbumService(_mockLogger.Object, httpClient);

            var task = Task.Run(async () => await albumService.GetPhotosByAlbumId(INVALID_ALBUM_ID));
            var result = task.Result;

            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GetPhotosByAlbumId_Returns_EmptyList_WhenApiNotFound()
        {
            var httpClient = GetHttpClient(HttpStatusCode.NotFound, GetEmptyResponseContent(), INVALID_REASON_PHRASE);
            var albumService = new AlbumService(_mockLogger.Object, httpClient);

            var task = Task.Run(async () => await albumService.GetPhotosByAlbumId(INVALID_ALBUM_ID));
            var result = task.Result;

            Assert.IsTrue(result.Count == 0);
        }

        private HttpClient GetHttpClient(HttpStatusCode responseHttpStatusCode, string responseContent, string responsePhrase)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = responseHttpStatusCode,
                    Content = new StringContent(responseContent),
                    ReasonPhrase = responsePhrase
                })
                .Verifiable();

            return new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://fake.com")
            };
        }
        private void AssertPhoto(Photo photo, Photo expectedPhoto)
        {
            Assert.IsTrue(photo.AlbumId == expectedPhoto.AlbumId);
            Assert.IsTrue(photo.Id == expectedPhoto.Id);
            Assert.IsTrue(photo.Title == expectedPhoto.Title);
            Assert.IsTrue(photo.Url == expectedPhoto.Url);
            Assert.IsTrue(photo.ThumbnailUrl == expectedPhoto.ThumbnailUrl);
        }

        private List<Photo> ExpectedPhotos
        {
            get
            {
                return new List<Photo>
                {
                    new Photo()
                    {
                        AlbumId = 4,
                        Id = 151,
                        Title = "possimus dolor minima provident ipsam",
                        Url = "https://via.placeholder.com/600/1d2ad4",
                        ThumbnailUrl = "https://via.placeholder.com/150/1d2ad4"
                    },
                    new Photo()
                    {
                        AlbumId = 4,
                        Id = 152,
                        Title = "et accusantium enim pariatur eum nihil fugit",
                        Url = "https://via.placeholder.com/600/a01c5b",
                        ThumbnailUrl = "https://via.placeholder.com/150/1d2ad4"
                    },
                    new Photo()
                    {
                        AlbumId = 4,
                        Id = 196,
                        Title = "amet maiores ut",
                        Url = "https://via.placeholder.com/600/128151",
                        ThumbnailUrl = "https://via.placeholder.com/150/128151"
                    },
                };
            }
        }

        private string GetValidResponseContent()
        {
            return JsonConvert.SerializeObject(ExpectedPhotos);
        }

        private string GetEmptyResponseContent()
        {
            return JsonConvert.SerializeObject(new List<Photo>());
        }
    }
}
