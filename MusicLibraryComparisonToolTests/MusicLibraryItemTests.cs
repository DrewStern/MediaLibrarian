using MusicLibraryCompareTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MusicLibraryCompareTool.UnitTests
{
    [TestClass]
    public class MusicLibraryItemTests
    {
        [TestMethod]
        public void TestEquality()
        {
            var libraryItem1 = new LibraryItem(
                new ArtistData("artistName"),
                new ReleaseData("releaseName"));

            var libraryItem2 = new LibraryItem(
                new ArtistData("artistName"),
                new ReleaseData("releaseName"));

            Assert.IsTrue(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void TestNonEquality_BothArtistsAndReleasesDiff()
        {
            var libraryItem1 = new LibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName1"));

            var libraryItem2 = new LibraryItem(
                new ArtistData("artistName2"),
                new ReleaseData("releaseName2"));

            Assert.IsFalse(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void TestNonEquality_ArtistsDiffOnly()
        {
            var libraryItem1 = new LibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName1"));

            var libraryItem2 = new LibraryItem(
                new ArtistData("artistName2"),
                new ReleaseData("releaseName1"));

            Assert.IsFalse(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void TestNonEquality_ReleasesDiffOnly()
        {
            var libraryItem1 = new LibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName1"));

            var libraryItem2 = new LibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName2"));

            Assert.IsFalse(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void TestNonEquality_ArtistDataNotSpecified()
        {
            var libraryItem1 = new LibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName1"));

            var libraryItem2 = new LibraryItem(
                null,
                new ReleaseData("releaseName1"));

            Assert.IsFalse(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void TestNonEquality_ReleaseDataNotSpecified()
        {
            var libraryItem1 = new LibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName1"));

            var libraryItem2 = new LibraryItem(
                new ArtistData("artistName1"),
                null);

            Assert.IsFalse(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void TestNonEqualityWithDifferentType()
        {
            var libraryItem1 = new LibraryItem(
                new ArtistData("artistName1"),
                new ReleaseData("releaseName1"));

            var libraryItem2 = new LibraryItem? { };

            Assert.IsFalse(libraryItem1.Equals(libraryItem2));
        }

        [TestMethod]
        public void TestLibraryItemToString_FullSpec()
        {
            var libraryItem = new LibraryItem(
                new ArtistData("artistName", "country"),
                new ReleaseData("releaseName", "demo"));

            var expected = "artistName (country) - releaseName (demo)";
            var actual = libraryItem.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLibraryItemToString_NoReleaseType()
        {
            var libraryItem = new LibraryItem(
                new ArtistData("artistName", "country"),
                new ReleaseData("releaseName"));

            var expected = "artistName (country) - releaseName";
            var actual = libraryItem.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLibraryItemToString_NoCountry()
        {
            var libraryItem = new LibraryItem(
                new ArtistData("artistName"),
                new ReleaseData("releaseName", "split"));

            var expected = "artistName - releaseName (split)";
            var actual = libraryItem.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLibraryItemToString_NoReleaseType_NoCountry()
        {
            var libraryItem = new LibraryItem(
                new ArtistData("artistName"),
                new ReleaseData("releaseName"));

            var expected = "artistName - releaseName";
            var actual = libraryItem.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
