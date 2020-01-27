using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnableToParseNullResponse()
        {
            _parser.Parse(null);
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

        //[TestMethod]
        //public void TestParse()
        //{
        //    var maHttpResponse = new MetalArchivesHttpResponse
        //    {
        //        aaData = new string[2, 2]
        //        {
        //            {
        //                { "<a href=\"https://www.metal-archives.com/bands/%21T.O.O.H.%21/16265\" title=\"!T.O.O.H.! (CZ)\">!T.O.O.H.!</a>" },
        //                { "<a href=\"https://www.metal-archives.com/albums/%21T.O.O.H.%21/Democratic_Solution/384622\">Democratic Solution</a> <!-- 7.792132 -->"}
        //            },
        //            {
        //                { "<a href=\"https://www.metal-archives.com/bands/%21T.O.O.H.%21/16265\" title=\"!T.O.O.H.! (CZ)\">!T.O.O.H.!</a>" },
        //                { "<a href=\"https://www.metal-archives.com/albums/%21T.O.O.H.%21/iDontExist/384622\">iDontExist</a> <!-- 7.792132 -->" }
        //            }
        //        }
        //    };
        //}

        //[TestMethod]
        //public void TestIsFilteringOutNonFullLengthReleases()
        //{
        //    // TODO: construct a MetalArchivesHttpResponse to give to Parse

        //    var expected = null; // TODO
        //    var actual = _parser.Parse();

        //    Assert.AreEqual(expected, actual);
        //}
    }
}
