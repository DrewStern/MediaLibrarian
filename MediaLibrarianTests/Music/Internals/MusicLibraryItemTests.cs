using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaLibrarian.UnitTests
{
    [TestClass]
    public class MusicLibraryItemTests
    {
        [TestMethod]
        public void GivenTwoIdenticalItems_WhenCompared_ThenTheyAreEqual()
        {
            var libraryItem1 = new MusicLibraryItem(
                new ArtistData("artistName"),
                new ReleaseData("releaseName"));

            var libraryItem2 = new MusicLibraryItem(
                new ArtistData("artistName"),
                new ReleaseData("releaseName"));

            Assert.IsTrue(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void GivenTwoItemsWithDifferentArtistNameButSameReleaseName_WhenCompared_ThenTheyAreNotEqual()
        {
            var libraryItem1 = new MusicLibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName1"));

            var libraryItem2 = new MusicLibraryItem(
                new ArtistData("artistName2"),
                new ReleaseData("releaseName1"));

            Assert.IsFalse(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void GivenTwoItemsWithSameArtistNameButDifferentReleaseName_WhenCompared_ThenTheyAreNotEqual()
        {
            var libraryItem1 = new MusicLibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName1"));

            var libraryItem2 = new MusicLibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName2"));

            Assert.IsFalse(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void GivenTwoItemsWithDifferentArtistNameAndDifferentReleaseName_WhenCompared_ThenTheyAreNotEqual()
        {
            var libraryItem1 = new MusicLibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName1"));

            var libraryItem2 = new MusicLibraryItem(
                new ArtistData("artistName2"),
                new ReleaseData("releaseName2"));

            Assert.IsFalse(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void GivenAnItemWithFaultyArtistData_WhenComparedWithValidItem_ThenNotEqual()
        {
            var libraryItem1 = new MusicLibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName1"));

            var libraryItem2 = new MusicLibraryItem(
                null,
                new ReleaseData("releaseName1"));

            Assert.IsFalse(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void GivenAnItemWithFaultyReleaseData_WhenComparedWithValidItem_ThenNotEqual()
        {
            var libraryItem1 = new MusicLibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName1"));

            var libraryItem2 = new MusicLibraryItem(
                new ArtistData("artistName1"),
                null);

            Assert.IsFalse(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void GivenAnItem_WhenComparedWithNullableItem_ThenNotEqual()
        {
            var libraryItem1 = new MusicLibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName1"));

            MusicLibraryItem libraryItem2 = null;

            Assert.IsFalse(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void GivenAnItem_WhenPrintedToString_ThenMatchesExpectedFormat()
        {
            var libraryItem = new MusicLibraryItem(
                new ArtistData("artistName", "country"),
                new ReleaseData("releaseName", "demo"));

            var expected = "artistName (country) - releaseName (demo)";
            var actual = libraryItem.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAnItemWhoseReleaseHasNoType_WhenPrintedToString_ThenMatchesExpectedFormat()
        {
            var libraryItem = new MusicLibraryItem(
                new ArtistData("artistName", "country"),
                new ReleaseData("releaseName"));

            var expected = "artistName (country) - releaseName";
            var actual = libraryItem.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAnItemWhoseArtistHasNoCountry_WhenPrintedToString_ThenMatchesExpectedFormat()
        {
            var libraryItem = new MusicLibraryItem(
                new ArtistData("artistName"),
                new ReleaseData("releaseName", "split"));

            var expected = "artistName - releaseName (split)";
            var actual = libraryItem.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAnItemWithNoReleaseTypeAndNoArtistCountry_WhenPrintedToString_ThenMatchesExpectedFormat()
        {
            var libraryItem = new MusicLibraryItem(
                new ArtistData("artistName"),
                new ReleaseData("releaseName"));

            var expected = "artistName - releaseName";
            var actual = libraryItem.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
