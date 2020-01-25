using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class LibraryDiffServiceTests
    {
        private static LibraryDiffService _libraryDiffService;

        [TestInitialize]
        public void LibraryDiffTestInitialize()
        {
            _libraryDiffService = new LibraryDiffService();
        }

        [TestMethod]
        public void EmptyLibraryShouldHaveNoDiffs()
        {
            var artistDiffs = _libraryDiffService.GetArtistDiffs(LibraryTestData.EmptyLibrary, LibraryTestData.EmptyLibrary);
            var releaseDiffs = _libraryDiffService.GetReleaseDiffs(LibraryTestData.EmptyLibrary, LibraryTestData.EmptyLibrary);

            Assert.AreEqual(artistDiffs.Count, 0);
            Assert.AreEqual(releaseDiffs.Count, 0);
        }

        [TestMethod]
        public void SameLibraryShouldHaveNoDiffs()
        {
            var artistDiffs = _libraryDiffService.GetArtistDiffs(LibraryTestData.ManyArtistsToManyRelasesLibrary, LibraryTestData.ManyArtistsToManyRelasesLibrary);
            var releaseDiffs = _libraryDiffService.GetReleaseDiffs(LibraryTestData.ManyArtistsToManyRelasesLibrary, LibraryTestData.ManyArtistsToManyRelasesLibrary);

            Assert.AreEqual(artistDiffs.Count, 0);
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
        public void WriteLibraryDiff()
        {
            var artistDiffs = _libraryDiffService.GetArtistDiffs(LibraryTestData.RandomLibraryOne, LibraryTestData.RandomLibraryTwo);
            var releaseDiffs = _libraryDiffService.GetReleaseDiffs(LibraryTestData.RandomLibraryOne, LibraryTestData.RandomLibraryTwo);

            Assert.AreEqual(artistDiffs.Count, 0);
            Assert.AreEqual(releaseDiffs.Count, LibraryTestData.RandomLibraryOneSubtractTwo.Releases.Count);
        }
    }
}
