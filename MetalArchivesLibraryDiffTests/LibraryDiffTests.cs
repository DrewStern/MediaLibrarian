using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class LibraryDiffTests
    {
        [TestMethod]
        public void EmptyLibraryShouldHaveNoDiffs()
        {
            LibraryDiff libraryDiff = new LibraryDiff(LibraryTestData.EmptyLibrary, LibraryTestData.EmptyLibrary);
            Assert.AreEqual(libraryDiff.GetUnrecognizedArtists().Count, 0);
            Assert.AreEqual(libraryDiff.GetMissingAlbums().Count, 0);
        }

        [TestMethod]
        public void SameLibraryShouldHaveNoDiffs()
        {
            LibraryDiff libraryDiff = new LibraryDiff(LibraryTestData.ManyArtistsToManyRelasesLibrary, LibraryTestData.ManyArtistsToManyRelasesLibrary);
            Assert.AreEqual(libraryDiff.GetUnrecognizedArtists().Count, 0);
            Assert.AreEqual(libraryDiff.GetMissingAlbums().Count, 0);
        }

        [TestMethod]
        public void DifferentLibrariesShouldHaveDiffs()
        {
            LibraryDiff libraryDiff = new LibraryDiff(LibraryTestData.ManyArtistsToManyRelasesLibrary, LibraryTestData.DisjointSimpleLibrary);

            Assert.AreEqual(libraryDiff.GetUnrecognizedArtists().Count, 3);
            Assert.AreEqual(libraryDiff.GetMissingAlbums().Count, 3);
        }

        [TestMethod]
        public void WriteLibraryDiff()
        {
            List<ArtistReleaseData> ardList1 = new List<ArtistReleaseData>
            {
                new ArtistReleaseData("artist1", "release1"),
                new ArtistReleaseData("artist2", "release2"),
            };

            List<ArtistReleaseData> ardList2 = new List<ArtistReleaseData>
            {
                new ArtistReleaseData("artist1", "release1"),
                new ArtistReleaseData("artist1", "release2"),
                new ArtistReleaseData("artist2", "release1"),
                new ArtistReleaseData("artist2", "release2"),
            };

            LibraryData libraryData1 = new LibraryData(ardList1);

            LibraryData libraryData2 = new LibraryData(ardList2);

            LibraryDiff libraryDiff = new LibraryDiff(libraryData1, libraryData2);

            Assert.AreEqual(libraryDiff.GetUnrecognizedArtists().Count, 0);

            var missingAlbums = libraryDiff.GetMissingAlbums();
            Assert.AreEqual(missingAlbums.Count, 2);
            Assert.AreEqual(missingAlbums[0], new ArtistReleaseData("artist1", "release2"));
            Assert.AreEqual(missingAlbums[1], new ArtistReleaseData("artist2", "release1"));
        }
    }
}
