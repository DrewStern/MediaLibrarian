using MusicLibraryCompareTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MusicLibraryCompareToolTests
{
    [TestClass]
    public class MetalArchivesRequestTests
    {
        [TestMethod]
        public void TestRequestAccountsForSpacesInArtistName()
        {
            var request = new MetalArchivesRequest(new ArtistData("a test artist name"));

            var expected = "\"a test artist name\"";
            var actual = request.ArtistName;

            Assert.AreEqual(expected, actual);
        }
    }
}
