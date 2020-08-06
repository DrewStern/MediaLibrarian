using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaLibrarian.UnitTests
{
    [TestClass]
    public class MetalArchivesRequestTests
    {
        [TestMethod]
        public void GivenAnArtistNameContainingSpaces_WhenCreatingRequest_ThenArtistNameIsWrappedInQuotes()
        {
            var request = new MetalArchivesRequest(new ArtistData("a test artist name"));

            var expected = "\"a test artist name\"";
            var actual = request.ArtistName;

            Assert.AreEqual(expected, actual);
        }
    }
}
