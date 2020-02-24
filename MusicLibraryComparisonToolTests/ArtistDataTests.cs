using MusicLibraryCompareTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MusicLibraryCompareToolTests
{
    [TestClass]
    public class ArtistDataTests
    {
        [TestMethod]
        public void TestArtists_SameName_DiffCountry_AreNotEqual()
        {
            ArtistData ad1 = new ArtistData("artistName", "country1");
            ArtistData ad2 = new ArtistData("artistName", "country2");

            // Q: Which of these is preferable here?
            Assert.AreNotEqual(ad1, ad2);
            Assert.IsFalse(ad1.Equals(ad2));
        }
    }
}
