using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class MetalArchivesHttpRequestTests
    {
        [TestMethod]
        public void TestHttpRequestAccountsForSpacesInArtistName()
        {
            var request = new MetalArchivesHttpRequest(new ArtistData("a test artist name"));

            var expected = "\"a test artist name\"";
            var actual = request.ArtistName;

            Assert.AreEqual(expected, actual);
        }
    }
}
