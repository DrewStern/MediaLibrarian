using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaLibrarian.UnitTests
{
    [TestClass]
    public class ArtistDataTests
    {
        [TestMethod]
        public void GivenAnArtist_WhenComparedToAnIdenticalArtist_ThenTheyAreEqual()
        {
            ArtistData ad1 = new ArtistData("artistName", "country");
            ArtistData ad2 = new ArtistData("artistName", "country");

            Assert.IsTrue(ad1.Equals(ad2));
        }

        [TestMethod]
        public void GivenAnArtist_WhenComparedToAnArtistWithTheSameNameButDifferentCountry_ThenTheyAreNotEqual()
        {
            ArtistData ad1 = new ArtistData("artistName", "country1");
            ArtistData ad2 = new ArtistData("artistName", "country2");

            Assert.IsFalse(ad1.Equals(ad2));
        }

        [TestMethod]
        public void GivenAnArtist_WhenComparedToAnArtistWithDifferentNameButSameCountry_ThenTheyAreNotEqual()
        {
            ArtistData ad1 = new ArtistData("artistName1", "country");
            ArtistData ad2 = new ArtistData("artistName2", "country");

            Assert.IsFalse(ad1.Equals(ad2));
        }
    }
}
