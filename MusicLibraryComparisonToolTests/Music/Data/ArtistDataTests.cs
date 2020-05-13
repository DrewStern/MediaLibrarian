using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaLibraryCompareTool.UnitTests
{
    [TestClass]
    public class ArtistDataTests
    {
        [TestMethod]
        public void GivenTwoArtistsWithSameNameButDifferentCountries_WhenCompared_ThenAreNotEqual()
        {
            ArtistData ad1 = new ArtistData("artistName", "country1");
            ArtistData ad2 = new ArtistData("artistName", "country2");

            // Q: Which of these is preferable here?
            Assert.AreNotEqual(ad1, ad2);
            Assert.IsFalse(ad1.Equals(ad2));
        }

        [TestMethod]
        public void GivenTwoArtistsWithDifferentNameButSameCountries_WhenCompared_ThenAreNotEqual()
        {
            ArtistData ad1 = new ArtistData("artistName1", "country");
            ArtistData ad2 = new ArtistData("artistName2", "country");

            // Q: Which of these is preferable here?
            Assert.AreNotEqual(ad1, ad2);
            Assert.IsFalse(ad1.Equals(ad2));
        }
    }
}
