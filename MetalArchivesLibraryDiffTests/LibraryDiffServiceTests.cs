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
            var artistReleaseDiffs = _libraryDiffService.GetArtistReleaseDiffs(LibraryTestData.EmptyLibrary, LibraryTestData.EmptyLibrary);

            Assert.AreEqual(artistDiffs.Count, 0);
            Assert.AreEqual(artistReleaseDiffs.Count, 0);
        }

        [TestMethod]
        public void SameLibraryShouldHaveNoDiffs()
        {
            var artistDiffs = _libraryDiffService.GetArtistDiffs(LibraryTestData.ManyArtistsToManyRelasesLibrary, LibraryTestData.ManyArtistsToManyRelasesLibrary);
            var artistReleaseDiffs = _libraryDiffService.GetArtistReleaseDiffs(LibraryTestData.ManyArtistsToManyRelasesLibrary, LibraryTestData.ManyArtistsToManyRelasesLibrary);

            Assert.AreEqual(artistDiffs.Count, 0);
            Assert.AreEqual(artistReleaseDiffs.Count, 0);
        }

        [TestMethod]
        public void DifferentLibrariesShouldHaveDiffs()
        {
            var artistDiffs = _libraryDiffService.GetArtistDiffs(LibraryTestData.ManyArtistsToManyRelasesLibrary, LibraryTestData.DisjointSimpleLibrary);
            var artistReleaseDiffs = _libraryDiffService.GetArtistReleaseDiffs(LibraryTestData.ManyArtistsToManyRelasesLibrary, LibraryTestData.DisjointSimpleLibrary);

            Assert.AreEqual(artistDiffs.Count, 3);
            Assert.AreEqual(artistReleaseDiffs.Count, 3);
        }

        [TestMethod]
        public void WriteLibraryDiff()
        {
            var artistDiffs = _libraryDiffService.GetArtistDiffs(LibraryTestData.RandomLibraryOne, LibraryTestData.RandomLibraryTwo);
            var artistReleaseDiffs = _libraryDiffService.GetArtistReleaseDiffs(LibraryTestData.RandomLibraryOne, LibraryTestData.RandomLibraryTwo);

            Assert.AreEqual(LibraryTestData.RandomLibraryOneSubtractTwo.EntireCollection.Count, 3);
            Assert.AreEqual(LibraryTestData.RandomLibraryOneSubtractTwo.EntireCollection.Count, 3);
        }
    }
}
