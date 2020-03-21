using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

        public AlbumService(ILogger<AlbumService> logger)
        {
            _logger = logger;
        }

        public async Task<List<Photo>> GetPhotosByAlbumId(int albumId)
        {
            var photos = new List<Photo>();
            var query = $"{Constants.PHOTOS_URL}{Constants.QUERY_BY_ALBUM_ID_SUFFIX}{albumId}";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(query))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        photos = JsonConvert.DeserializeObject<List<Photo>>(apiResponse);
                    }
                    else
                    {
                        _logger.LogError($"Failed to connect to Album Service. ResponseStatusCode:{response.StatusCode}, ReasonPhrase:{response.ReasonPhrase}");
                    }
                }
            }
            return photos;
        }
    }
}
