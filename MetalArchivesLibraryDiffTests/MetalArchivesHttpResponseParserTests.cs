using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class MetalArchivesHttpResponseParserTests
    {
        private MetalArchivesHttpResponseParser _parser;

        [TestInitialize]
        public void MetalArchivesHttpResponseParserTestInitialize()
        {
            _parser = new MetalArchivesHttpResponseParser();
        }


        [TestMethod]
        public void TestArtistDataHtmlParsing()
        {
            var artistResponseHtml = "<a href=\"https://www.metal-archives.com/bands/%21T.O.O.H.%21/16265\" title=\"!T.O.O.H.! (CZ)\">!T.O.O.H.!</a>";

            var expected = new ArtistData("!T.O.O.H.!", "CZ");
            var actual = _parser.ExtractArtistData(artistResponseHtml);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestReleaseDataHtmlParsing()
        {
            var releaseResponseHtml = "<a href=\"https://www.metal-archives.com/albums/%21T.O.O.H.%21/Democratic_Solution/384622\">Democratic Solution</a> <!-- 7.792132 -->";

            var expected = new ReleaseData("Democratic Solution");
            var actual = _parser.ExtractReleaseData(releaseResponseHtml);

            Assert.AreEqual(expected, actual);
        }
    }
}
