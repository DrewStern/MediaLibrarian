using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class MetalArchivesHttpClientTests
    {
        [TestMethod]
        public void TestFindByArtistMayNotBeNullOrEmptyOrWhitespace()
        {
            var maHttpService = new MetalArchivesHttpService();
            var maHttpResponseParser = new MetalArchivesHttpResponseParser();
            var maHttpClient = new MetalArchivesHttpClient(maHttpService, maHttpResponseParser);

            Assert.ThrowsException<ArgumentException>(() => maHttpClient.FindByArtist(null));
            Assert.ThrowsException<ArgumentException>(() => maHttpClient.FindByArtist(String.Empty));
            Assert.ThrowsException<ArgumentException>(() => maHttpClient.FindByArtist("        "));
        }
    }
}
