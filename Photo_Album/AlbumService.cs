using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Photo_Album.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Photo_Album
{
    public interface IAlbumService
    {
        Task<List<Photo>> GetPhotosByAlbumId(int albumId);
    }

    public class AlbumService : IAlbumService
    {
        private readonly ILogger<AlbumService> _logger;
        private readonly HttpClient _httpClient;

        public AlbumService(ILogger<AlbumService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<List<Photo>> GetPhotosByAlbumId(int albumId)
        {
            var photos = new List<Photo>();
            var query = $"{Constants.PHOTOS_URL}{Constants.QUERY_BY_ALBUM_ID_SUFFIX}{albumId}";

            using (var response = await _httpClient.GetAsync(query))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    photos = JsonConvert.DeserializeObject<List<Photo>>(apiResponse);
                }
                else
                {
                    _logger.LogError(string.Format(Constants.ERROR_FAILED_CONNECTION, response.StatusCode, response.ReasonPhrase));
                }
            }
            return photos;
        }
    }
}
