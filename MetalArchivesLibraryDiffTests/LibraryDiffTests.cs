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
            LibraryDiff libraryDiff = new LibraryDiff(LibraryTestData.RandomLibraryOne, LibraryTestData.RandomLibraryTwo);

            Assert.AreEqual(libraryDiff.GetUnrecognizedArtists().Count, 0);

            var missingAlbums = libraryDiff.GetMissingAlbums();
            Assert.AreEqual(missingAlbums.Count, LibraryTestData.RandomLibraryOneSubtractTwo.EntireCollection.Count);



            //Assert.AreEqual(missingAlbums[0], new ArtistReleaseData("artist1", "release2"));
            //Assert.AreEqual(missingAlbums[1], new ArtistReleaseData("artist2", "release1"));

            //Assert.AreEqual(missingAlbums, LibraryTestData.RandomLibraryOneSubtractTwo);

            //Assert.AreEqual(missingAlbums, LibraryTestData.RandomLibraryOneSubtractTwo.EntireCollection);
        }
    }
}
