using MusicLibraryCompareTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MusicLibraryCompareToolTests
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
        public void TestUnableToParseNullResponse()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _parser.Parse(null));
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
            var releaseTypeHtml = "Full-Length";

            var expected = new ReleaseData("Democratic Solution", "Full-Length");
            var actual = _parser.ExtractReleaseData(releaseResponseHtml, releaseTypeHtml);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestParse()
        {
            var htmlResponse = new string[3];
            htmlResponse[0] = "<a href=\"https://www.metal-archives.com/bands/%21T.O.O.H.%21/16265\" title=\"!T.O.O.H.! (CZ)\">!T.O.O.H.!</a>";
            htmlResponse[1] = "<a href=\"https://www.metal-archives.com/albums/%21T.O.O.H.%21/Democratic_Solution/384622\">Democratic Solution</a> <!-- 7.792132 -->";
            htmlResponse[2] = "Full-Length";

            var maResponse = new MetalArchivesResponse();
            maResponse.aaData = new string[1][];
            maResponse.aaData[0] = htmlResponse;

            Library l = _parser.Parse(maResponse);

            Assert.AreEqual(1, l.Collection.Count);
            Assert.AreEqual(1, l.Artists.Count);
            Assert.AreEqual(1, l.Releases.Count);
        }

        [TestMethod]
        public void TestParseMasksNonFullLengthReleases()
        {
            var htmlResponse = new string[3];
            htmlResponse[0] = "<a href=\"https://www.metal-archives.com/bands/%21T.O.O.H.%21/16265\" title=\"!T.O.O.H.! (CZ)\">!T.O.O.H.!</a>";
            htmlResponse[1] = "<a href=\"https://www.metal-archives.com/albums/%21T.O.O.H.%21/Democratic_Solution/384622\">Democratic Solution</a> <!-- 7.792132 -->";
            htmlResponse[2] = "demo";

            var maHttpResponse = new MetalArchivesResponse();
            maHttpResponse.aaData = new string[1][];
            maHttpResponse.aaData[0] = htmlResponse;

            Library l = _parser.Parse(maHttpResponse);

            Assert.AreEqual(0, l.Collection.Count);
            Assert.AreEqual(0, l.Artists.Count);
            Assert.AreEqual(0, l.Releases.Count);
        }
    }
}
