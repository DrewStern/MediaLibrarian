using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class MetalArchivesLibraryDiffTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CanOnlyLoadDataFromExistantDiskLocations()
        {
            // use a new directory on disk that has no content
            DirectoryInfo libraryPath = new DirectoryInfo("C:\\todo");

            LibraryData l = new LibraryData(libraryPath);
        }

        [TestMethod]
        public void EmptyLibraryShouldHaveNoArtists()
        {
            // use a new directory on disk that has no content
            DirectoryInfo libraryPath = new DirectoryInfo("C:\\todo");

            libraryPath.Create();

            LibraryData l = new LibraryData(libraryPath);

            Assert.AreEqual(l.Artists.Count, 0);

            // TODO: I guess this could leave the folder on disk if the Assert above fails...
            libraryPath.Delete();
        }

        [TestMethod]
        public void LibraryDoesntHaveDuplicateArtists()
        {
            LibraryData l = MetalArchivesLibraryTestData.OneArtistToManyReleasesLibrary;
            Assert.AreEqual(l.Artists.Count, 1);
            Assert.AreEqual(l.Releases.Count, 2);
        }

        [TestMethod]
        public void LibraryDoesntHaveDuplicateArtistReleases()
        {
            LibraryData l = MetalArchivesLibraryTestData.DuplicatedDataLibrary;
            Assert.AreEqual(l.Artists.Count, 1);
            Assert.AreEqual(l.Releases.Count, 1);
        }

        [TestMethod]
        public void LibraryHasArtists()
        {
            LibraryData l = MetalArchivesLibraryTestData.ManyArtistsToManyRelasesLibrary;
            Assert.AreEqual(l.Artists.Count, 3);
            Assert.AreEqual(l.Releases.Count, 3);
        }

        [TestMethod]
        public void EmptyLibraryShouldHaveNoDiffs()
        {
            LibraryDiff libraryDiff = new LibraryDiff(MetalArchivesLibraryTestData.EmptyLibrary, MetalArchivesLibraryTestData.EmptyLibrary);
            Assert.AreEqual(libraryDiff.GetUnrecognizedArtists().Count, 0);
            Assert.AreEqual(libraryDiff.GetMissingAlbums().Count, 0);
        }

        [TestMethod]
        public void SameLibraryShouldHaveNoDiffs()
        {
            LibraryDiff libraryDiff = new LibraryDiff(MetalArchivesLibraryTestData.ManyArtistsToManyRelasesLibrary, MetalArchivesLibraryTestData.ManyArtistsToManyRelasesLibrary);
            Assert.AreEqual(libraryDiff.GetUnrecognizedArtists().Count, 0);
            Assert.AreEqual(libraryDiff.GetMissingAlbums().Count, 0);
        }

        [TestMethod]
        public void DifferentLibrariesShouldHaveDiffs()
        {
            LibraryDiff libraryDiff = new LibraryDiff(MetalArchivesLibraryTestData.ManyArtistsToManyRelasesLibrary, MetalArchivesLibraryTestData.DisjointSimpleLibrary);

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
