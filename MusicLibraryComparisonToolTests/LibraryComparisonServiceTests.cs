using MusicLibraryCompareTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MusicLibraryCompareToolTests
{
    [TestClass]
    public class LibraryComparisonServiceTests
    {
        private static TestContext _testContext;
        private static LibraryComparisonService _libraryDiffService;

        [ClassInitialize]
        public static void LibraryDiffServiceClassInitialize(TestContext testContext)
        {
            _testContext = testContext;
            _libraryDiffService = new LibraryComparisonService();
        }

        [TestMethod]
        public void EmptyLibraryShouldHaveNoArtists()
        {
            var artistDiffs = _libraryDiffService.GetArtistDiffs(LibraryTestData.EmptyLibrary, LibraryTestData.EmptyLibrary);
            Assert.AreEqual(artistDiffs.Count, 0);
        }

        [TestMethod]
        public void EmptyLibraryShouldHaveNoReleases()
        {
            var releaseDiffs = _libraryDiffService.GetReleaseDiffs(LibraryTestData.EmptyLibrary, LibraryTestData.EmptyLibrary);
            Assert.AreEqual(releaseDiffs.Count, 0);
        }

        [TestMethod]
        public void SameLibraryShouldHaveNoDiffArtists()
        {
            var artistDiffs = _libraryDiffService.GetArtistDiffs(LibraryTestData.ManyArtistsToManyRelasesLibrary, LibraryTestData.ManyArtistsToManyRelasesLibrary);
            Assert.AreEqual(artistDiffs.Count, 0);
        }

        [TestMethod]
        public void SameLibraryShouldHaveNoDiffReleases()
        {
            var releaseDiffs = _libraryDiffService.GetReleaseDiffs(LibraryTestData.ManyArtistsToManyRelasesLibrary, LibraryTestData.ManyArtistsToManyRelasesLibrary);
            Assert.AreEqual(releaseDiffs.Count, 0);
        }

        [TestMethod]
        public void DifferentLibrariesShouldHaveDiffs()
        {
            var artistDiffs = _libraryDiffService.GetArtistDiffs(LibraryTestData.ManyArtistsToManyRelasesLibrary, LibraryTestData.DisjointSimpleLibrary);
            var releaseDiffs = _libraryDiffService.GetReleaseDiffs(LibraryTestData.ManyArtistsToManyRelasesLibrary, LibraryTestData.DisjointSimpleLibrary);

            Assert.AreEqual(artistDiffs.Count, 3);
            Assert.AreEqual(releaseDiffs.Count, 3);
        }

        [TestMethod]
        public void TwoArtistsWithSameNameDifferentCountryShouldDiff()
        {
            var artistDiffs1 = _libraryDiffService.GetArtistDiffs(LibraryTestData.MultipleArtistsWithSameNameDifferentCountry1Library, LibraryTestData.MultipleArtistsWithSameNameDifferentCountry2Library);
            var artistDiffs2 = _libraryDiffService.GetArtistDiffs(LibraryTestData.MultipleArtistsWithSameNameDifferentCountry2Library, LibraryTestData.MultipleArtistsWithSameNameDifferentCountry1Library);

            Assert.AreEqual(artistDiffs1.Count, 2);
            Assert.AreEqual(artistDiffs2.Count, 2);
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
