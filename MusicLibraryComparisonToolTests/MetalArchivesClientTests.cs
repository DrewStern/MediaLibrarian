using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MusicLibraryCompareTool.UnitTests
{
    [TestClass]
    public class MetalArchivesClientTests
    {
        private MetalArchivesServiceClient _client;

        [TestInitialize]
        public void OnInitialize()
        {
            var maService = new MetalArchivesServiceProvider();
            var maResponseParser = new MetalArchivesResponseParser();
            _client = new MetalArchivesServiceClient(maService, maResponseParser);
        }

        [TestMethod]
        public void TestFindByArtistMayNotBeNullOrEmptyOrWhitespace()
        {
            Assert.ThrowsException<ArgumentException>(() => _client.FindByArtist(null));
            Assert.ThrowsException<ArgumentException>(() => _client.FindByArtist(String.Empty));
            Assert.ThrowsException<ArgumentException>(() => _client.FindByArtist("        "));
        }
    }
}
