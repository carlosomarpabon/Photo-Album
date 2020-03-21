using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Photo_Album;

namespace Photo_Album_Tests
{
    [TestClass]
    public class AppServiceProviderTests
    {
        private AppServiceProvider _appServiceProvider;

        [TestInitialize]
        public void Setup()
        {
            _appServiceProvider = new AppServiceProvider();
        }

        [TestMethod]
        public void Returns_InputValidator()
        {
            var service = _appServiceProvider.GetService<IInputValidator>();

            Assert.IsInstanceOfType(service, typeof(InputValidator));
        }

        [TestMethod]
        public void Returns_AlbumService()
        {
            var service = _appServiceProvider.GetService<IAlbumService>();

            Assert.IsInstanceOfType(service, typeof(AlbumService));
        }

        [TestMethod]
        public void Returns_Logger()
        {
            var service = _appServiceProvider.GetService<ILogger<AlbumService>>();

            Assert.IsInstanceOfType(service, typeof(Logger<AlbumService>));
        }
    }
}
