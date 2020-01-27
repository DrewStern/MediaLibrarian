using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class MetalArchivesHttpClientTests
    {
        [TestMethod]
        [Ignore("Test not implemented")]
        public void TestArtistDataHtmlParsing()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFindByArtistMayNotBeNullOrEmptyOrWhitespace()
        {
            var maHttpService = new MetalArchivesHttpService();
            var maHttpResponseParser = new MetalArchivesHttpResponseParser();
            var maHttpClient = new MetalArchivesHttpClient(maHttpService, maHttpResponseParser);

            maHttpClient.FindByArtist(null);
            maHttpClient.FindByArtist(String.Empty);
            maHttpClient.FindByArtist("        ");
        }
    }
}
