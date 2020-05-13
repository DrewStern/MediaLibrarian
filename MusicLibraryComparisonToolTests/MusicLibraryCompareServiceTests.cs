using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MusicLibraryCompareTool.UnitTests
{
    [TestClass]
    public class MusicLibraryCompareServiceTests
    {
        private static TestContext _testContext;
        private static MusicLibraryCompareService _musicLibraryCompareService;

        [ClassInitialize]
        public static void MusicLibraryCompareServiceClassInitialize(TestContext testContext)
        {
            _testContext = testContext;
            _musicLibraryCompareService = new MusicLibraryCompareService();
        }

        [TestMethod]
        public void GivenTwoEmptyLibraries_WhenCompared_ThenShouldFindNoDifferentArtists()
        {
            var artistDiffs = _musicLibraryCompareService.GetArtistDiffs(LibraryTestData.EmptyLibrary, LibraryTestData.EmptyLibrary);
            Assert.AreEqual(artistDiffs.Count, 0);
        }

        [TestMethod]
        public void GivenTwoEmptyLibraries_WhenCompared_ThenShouldFindNoDifferentReleases()
        {
            var releaseDiffs = _musicLibraryCompareService.GetReleaseDiffs(LibraryTestData.EmptyLibrary, LibraryTestData.EmptyLibrary);
            Assert.AreEqual(releaseDiffs.Count, 0);
        }

        [TestMethod]
        public void GivenTwoIdenticalLibraries_WhenCompared_ThenShouldFindNoDifferentArtists()
        {
            var artistDiffs = _musicLibraryCompareService.GetArtistDiffs(LibraryTestData.ManyArtistsToManyReleasesLibrary, LibraryTestData.ManyArtistsToManyReleasesLibrary);
            Assert.AreEqual(artistDiffs.Count, 0);
        }

        [TestMethod]
        public void GivenTwoIdenticalLibraries_WhenCompared_ThenShouldFindNoDifferentReleases()
        {
            var releaseDiffs = _musicLibraryCompareService.GetReleaseDiffs(LibraryTestData.ManyArtistsToManyReleasesLibrary, LibraryTestData.ManyArtistsToManyReleasesLibrary);
            Assert.AreEqual(releaseDiffs.Count, 0);
        }

        [TestMethod]
        public void GivenTwoDifferentLibraries_WhenCompared_ShouldFindDifferences()
        {
            var artistDiffs = _musicLibraryCompareService.GetArtistDiffs(LibraryTestData.ManyArtistsToManyReleasesLibrary, LibraryTestData.DisjointSimpleLibrary);
            var releaseDiffs = _musicLibraryCompareService.GetReleaseDiffs(LibraryTestData.ManyArtistsToManyReleasesLibrary, LibraryTestData.DisjointSimpleLibrary);

            Assert.AreEqual(artistDiffs.Count, 3);
            Assert.AreEqual(releaseDiffs.Count, 3);
        }

        [TestMethod]
        public void GivenTwoArtistsWithSameNameButDifferentCountries_WhenCompared_ShouldFindDifferences()
        {
            var artistDiffs1 = _musicLibraryCompareService.GetArtistDiffs(LibraryTestData.MultipleArtistsWithSameNameDifferentCountry1Library, LibraryTestData.MultipleArtistsWithSameNameDifferentCountry2Library);
            var artistDiffs2 = _musicLibraryCompareService.GetArtistDiffs(LibraryTestData.MultipleArtistsWithSameNameDifferentCountry2Library, LibraryTestData.MultipleArtistsWithSameNameDifferentCountry1Library);

            Assert.AreEqual(artistDiffs1.Count, 2);
            Assert.AreEqual(artistDiffs2.Count, 2);
        }

        [TestMethod]
        public void GivenAnEmptyLibraryAndANonEmptyLibrary_WhenSummed_ShouldResultInTheOriginalNonEmptyLibrary()
        {
            var expected = LibraryTestData.ManyArtistsToManyReleasesLibrary;
            var actual = _musicLibraryCompareService.GetSum(LibraryTestData.EmptyLibrary, LibraryTestData.ManyArtistsToManyReleasesLibrary);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAnEmptyLibraryAndANonEmptyLibrary_WhenLeftOutersected_ShouldResultInAnEmptyLibrary()
        {
            var expected = LibraryTestData.EmptyLibrary;
            var actual = _musicLibraryCompareService.GetLeftOutersection(LibraryTestData.EmptyLibrary, LibraryTestData.ManyArtistsToManyReleasesLibrary);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAnEmptyLibraryAndANonEmptyLibrary_WhenRightOutersected_ShouldResultInTheOriginalNonEmptyLibrary()
        {
            var expected = LibraryTestData.ManyArtistsToManyReleasesLibrary;
            var actual = _musicLibraryCompareService.GetRightOutersection(LibraryTestData.EmptyLibrary, LibraryTestData.ManyArtistsToManyReleasesLibrary);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WriteLibraryDiff()
        {
            var artistDiffs = _musicLibraryCompareService.GetArtistDiffs(LibraryTestData.RandomLibraryOne, LibraryTestData.RandomLibraryTwo);
            var releaseDiffs = _musicLibraryCompareService.GetReleaseDiffs(LibraryTestData.RandomLibraryOne, LibraryTestData.RandomLibraryTwo);

            Assert.AreEqual(artistDiffs.Count, 0);
            Assert.AreEqual(releaseDiffs.Count, LibraryTestData.RandomLibraryOneSubtractTwo.Releases.Count);
        }
    }
}
