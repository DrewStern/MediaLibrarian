using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaLibraryCompareTool.UnitTests
{
    [TestClass]
    public class MetalArchivesResponseParserTests
    {
        private MetalArchivesResponseParser _parser;

        [TestInitialize]
        public void MetalArchivesResponseParserTestInitialize()
        {
            _parser = new MetalArchivesResponseParser();
        }

        [TestMethod]
        public void GivenResponseToParse_WhenParsingArtistDataOutOfHtml_ThenHtmlStrippedOffCorrectly()
        {
            var artistResponseHtml = "<a href=\"https://www.metal-archives.com/bands/%21T.O.O.H.%21/16265\" title=\"!T.O.O.H.! (CZ)\">!T.O.O.H.!</a>";

            var expected = new ArtistData("!T.O.O.H.!", "CZ");
            var actual = _parser.GetArtistData(artistResponseHtml);

            Assert.AreEqual(expected, actual);
        }

        #region ExtractReleaseData tests

        [TestMethod]
        public void GivenResponseToParse_WhenParsingReleaseDataOutOfHtml_ThenHtmlStrippedOffCorrectly()
        {
            var releaseResponseHtml = "<a href=\"https://www.metal-archives.com/albums/%21T.O.O.H.%21/Democratic_Solution/384622\">Democratic Solution</a> <!-- 7.792132 -->";
            var releaseTypeHtml = "Full-Length";

            var expected = new ReleaseData("Democratic Solution", "Full-Length");
            var actual = _parser.GetReleaseData(releaseResponseHtml, releaseTypeHtml);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenResponseToParse_WhenParsingDemoReleaseDataOutOfHtml_ThenHtmlStrippedOffCorrectly()
        {
            var releaseResponseHtml = "<a href=\"https://www.metal-archives.com/albums/%21T.O.O.H.%21/Democratic_Solution/384622\">some wacky demo name (demo)</a> <!-- 7.792132 -->";
            var releaseTypeHtml = "demo";

            var expected = new ReleaseData("some wacky demo name", "demo");
            var actual = _parser.GetReleaseData(releaseResponseHtml, releaseTypeHtml);

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Parse tests

        [TestMethod]
        public void GivenAFaultyResponse_WhenParsed_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _parser.Parse(null));
        }

        [TestMethod]
        public void GivenResponseToParse_WhenMultipleItemsAreReturned_ThenTheyShouldBeParsedCorrectly()
        {
            var htmlResponse1 = new string[3];
            htmlResponse1[0] = "<a href=\"https://www.metal-archives.com/bands/%21T.O.O.H.%21/16265\" title=\"!T.O.O.H.! (CZ)\">!T.O.O.H.!</a>";
            htmlResponse1[1] = "<a href=\"https://www.metal-archives.com/albums/%21T.O.O.H.%21/Democratic_Solution/384622\">Democratic Solution</a> <!-- 7.792132 -->";
            htmlResponse1[2] = "Full-Length";

            var htmlResponse2 = new string[3];
            htmlResponse2[0] = "<a href=\"https://www.metal-archives.com/bands/%21T.O.O.H.%21/16265\" title=\"!T.O.O.H.! (CZ)\">!T.O.O.H.!</a>";
            htmlResponse2[1] = "<a href=\"https://www.metal-archives.com/albums/%21T.O.O.H.%21/Democratic_Solution/384622\">wacky demo name (demo)</a> <!-- 7.792132 -->";
            htmlResponse2[2] = "demo";

            var maResponse = new MetalArchivesResponse();
            maResponse.aaData = new string[2][];
            maResponse.aaData[0] = htmlResponse1;
            maResponse.aaData[1] = htmlResponse2;

            MusicLibrary l = _parser.Parse(maResponse);

            Assert.AreEqual(2, l.Collection.Count);

            Assert.AreEqual(1, l.Artists.Count);
            Assert.AreEqual("!T.O.O.H.!", l.Artists[0].ArtistName);
            Assert.AreEqual("CZ", l.Artists[0].Country);

            Assert.AreEqual(2, l.Releases.Count);
            Assert.AreEqual("Democratic Solution", l.Releases[0].ReleaseName);
            Assert.AreEqual(true, l.Releases[0].IsFullLength);

            Assert.AreEqual("wacky demo name", l.Releases[1].ReleaseName);
            Assert.AreEqual(false, l.Releases[1].IsFullLength);
        }

        #endregion
    }
}
