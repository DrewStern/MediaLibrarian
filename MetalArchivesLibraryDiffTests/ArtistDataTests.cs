using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetalArchivesLibraryDiffTests
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

        [TestMethod]
        [Ignore]
        public void TestArtists_IgnoresCountryFlag()
        {
            // NOTE: mutating ArtistData.Equals such that the Country property is not compared between the two does NOT cause any tests to fail. 
            // not sure if that's what I want to do or not, or if masking the Country prop should instead happen in the ResponseParser
        }
    }
}
