using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class MetalArchivesClientTests
    {
        private MetalArchivesClient _client;

        [TestInitialize]
        public void OnInitialize()
        {
            var maService = new MetalArchivesService();
            var maResponseParser = new MetalArchivesResponseParser();
            _client = new MetalArchivesClient(maService, maResponseParser);
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
