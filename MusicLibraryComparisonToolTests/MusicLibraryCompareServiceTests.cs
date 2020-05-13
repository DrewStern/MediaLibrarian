using MusicLibraryCompareTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MusicLibraryCompareTool.UnitTests
{
    [TestClass]
    public class MusicLibraryCompareServiceTests
    {
        private static TestContext _testContext;
        private static LibraryComparisonService _libraryDiffService;

        [ClassInitialize]
        public static void MusicLibraryCompareServiceClassInitialize(TestContext testContext)
        {
            _testContext = testContext;
            _libraryDiffService = new LibraryComparisonService();
        }

        [TestMethod]
        public void GivenTwoEmptyLibraries_WhenCompared_ThenShouldFindNoDifferentArtists()
        {
            var artistDiffs = _libraryDiffService.GetArtistDiffs(LibraryTestData.EmptyLibrary, LibraryTestData.EmptyLibrary);
            Assert.AreEqual(artistDiffs.Count, 0);
        }

        [TestMethod]
        public void GivenTwoEmptyLibraries_WhenCompared_ThenShouldFindNoDifferentReleases()
        {
            var releaseDiffs = _libraryDiffService.GetReleaseDiffs(LibraryTestData.EmptyLibrary, LibraryTestData.EmptyLibrary);
            Assert.AreEqual(releaseDiffs.Count, 0);
        }

        [TestMethod]
        public void GivenTwoIdenticalLibraries_WhenCompared_ThenShouldFindNoDifferentArtists()
        {
            var artistDiffs = _libraryDiffService.GetArtistDiffs(LibraryTestData.ManyArtistsToManyReleasesLibrary, LibraryTestData.ManyArtistsToManyReleasesLibrary);
            Assert.AreEqual(artistDiffs.Count, 0);
        }

        [TestMethod]
        public void GivenTwoIdenticalLibraries_WhenCompared_ThenShouldFindNoDifferentReleases()
        {
            var releaseDiffs = _libraryDiffService.GetReleaseDiffs(LibraryTestData.ManyArtistsToManyReleasesLibrary, LibraryTestData.ManyArtistsToManyReleasesLibrary);
            Assert.AreEqual(releaseDiffs.Count, 0);
        }

        [TestMethod]
        public void GivenTwoDifferentLibraries_WhenCompared_ShouldFindDifferences()
        {
            var artistDiffs = _libraryDiffService.GetArtistDiffs(LibraryTestData.ManyArtistsToManyReleasesLibrary, LibraryTestData.DisjointSimpleLibrary);
            var releaseDiffs = _libraryDiffService.GetReleaseDiffs(LibraryTestData.ManyArtistsToManyReleasesLibrary, LibraryTestData.DisjointSimpleLibrary);

            Assert.AreEqual(artistDiffs.Count, 3);
            Assert.AreEqual(releaseDiffs.Count, 3);
        }

        [TestMethod]
        public void GivenTwoArtistsWithSameNameButDifferentCountries_WhenCompared_ShouldFindDifferences()
        {
            var artistDiffs1 = _libraryDiffService.GetArtistDiffs(LibraryTestData.MultipleArtistsWithSameNameDifferentCountry1Library, LibraryTestData.MultipleArtistsWithSameNameDifferentCountry2Library);
            var artistDiffs2 = _libraryDiffService.GetArtistDiffs(LibraryTestData.MultipleArtistsWithSameNameDifferentCountry2Library, LibraryTestData.MultipleArtistsWithSameNameDifferentCountry1Library);

            Assert.AreEqual(artistDiffs1.Count, 2);
            Assert.AreEqual(artistDiffs2.Count, 2);
        }

        [TestMethod]
        public void GivenAnEmptyLibraryAndANonEmptyLibrary_WhenSummed_ShouldResultInTheOriginalNonEmptyLibrary()
        {
            var expected = LibraryTestData.ManyArtistsToManyReleasesLibrary;
            var actual = _libraryDiffService.GetSum(LibraryTestData.EmptyLibrary, LibraryTestData.ManyArtistsToManyReleasesLibrary);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAnEmptyLibraryAndANonEmptyLibrary_WhenLeftOutersected_ShouldResultInAnEmptyLibrary()
        {
            var expected = LibraryTestData.EmptyLibrary;
            var actual = _libraryDiffService.GetLeftOutersection(LibraryTestData.EmptyLibrary, LibraryTestData.ManyArtistsToManyReleasesLibrary);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAnEmptyLibraryAndANonEmptyLibrary_WhenRightOutersected_ShouldResultInTheOriginalNonEmptyLibrary()
        {
            var expected = LibraryTestData.ManyArtistsToManyReleasesLibrary;
            var actual = _libraryDiffService.GetRightOutersection(LibraryTestData.EmptyLibrary, LibraryTestData.ManyArtistsToManyReleasesLibrary);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WriteLibraryDiff()
        {
            var artistDiffs = _libraryDiffService.GetArtistDiffs(LibraryTestData.RandomLibraryOne, LibraryTestData.RandomLibraryTwo);
            var releaseDiffs = _libraryDiffService.GetReleaseDiffs(LibraryTestData.RandomLibraryOne, LibraryTestData.RandomLibraryTwo);

            Assert.AreEqual(artistDiffs.Count, 0);
            Assert.AreEqual(releaseDiffs.Count, LibraryTestData.RandomLibraryOneSubtractTwo.Releases.Count);
        }
    }
}
