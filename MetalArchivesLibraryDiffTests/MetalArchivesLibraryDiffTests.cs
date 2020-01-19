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
            List<ArtistReleaseData> ardList = new List<ArtistReleaseData>
            {
                new ArtistReleaseData("artist1", "release1"),
                new ArtistReleaseData("artist1", "release2"),
            };

            LibraryData l = new LibraryData(ardList);

            Assert.AreEqual(l.Artists.Count, 1);
            Assert.AreEqual(l.Releases.Count, 2);
        }

        [TestMethod]
        public void LibraryDoesntHaveDuplicateArtistReleases()
        {
            List<ArtistReleaseData> ardList = new List<ArtistReleaseData>
            {
                new ArtistReleaseData("artist1", "release1"),
                new ArtistReleaseData("artist1", "release1"),
            };

            LibraryData l = new LibraryData(ardList);

            Assert.AreEqual(l.Artists.Count, 1);
            Assert.AreEqual(l.Releases.Count, 1);
        }

        [TestMethod]
        public void LibraryHasArtists()
        {
            List<ArtistReleaseData> ardList = new List<ArtistReleaseData>
            {
                new ArtistReleaseData("artist1", "release1"),
                new ArtistReleaseData("artist2", "release2"),
                new ArtistReleaseData("artist3", "release3")
            };

            LibraryData l = new LibraryData(ardList);

            Assert.AreEqual(l.Artists.Count, 3);
            Assert.AreEqual(l.Releases.Count, 3);
        }

        [TestMethod]
        public void EmptyLibraryShouldHaveNoDiffs()
        {
            List<ArtistReleaseData> ardList1 = new List<ArtistReleaseData>
            {

            };

            LibraryData libraryData1 = new LibraryData(ardList1);

            LibraryData libraryData2 = new LibraryData(ardList1);

            LibraryDiff libraryDiff = new LibraryDiff(libraryData1, libraryData2);

            Assert.AreEqual(libraryDiff.GetMissingAlbums().Count, 0);
        }

        [TestMethod]
        public void SameLibraryShouldHaveNoDiffs()
        {
            List<ArtistReleaseData> ardList1 = new List<ArtistReleaseData>
            {
                new ArtistReleaseData("artist1", "release1"),
                new ArtistReleaseData("artist2", "release2"),
                new ArtistReleaseData("artist3", "release3")
            };

            List<ArtistReleaseData> ardList2 = new List<ArtistReleaseData>
            {
                new ArtistReleaseData("artist1", "release1"),
                new ArtistReleaseData("artist2", "release2"),
                new ArtistReleaseData("artist3", "release3")
            };

            LibraryData libraryData1 = new LibraryData(ardList1);

            LibraryData libraryData2 = new LibraryData(ardList2);

            LibraryDiff libraryDiff = new LibraryDiff(libraryData1, libraryData2);

            Assert.AreEqual(libraryDiff.GetMissingAlbums().Count, 0);
        }

        [TestMethod]
        public void DifferentLibrariesShouldHaveDiffs()
        {
            List<ArtistReleaseData> ardList1 = new List<ArtistReleaseData>
            {
                new ArtistReleaseData("artist1", "release1"),
                new ArtistReleaseData("artist2", "release2"),
                new ArtistReleaseData("artist3", "release3")
            };

            List<ArtistReleaseData> ardList2 = new List<ArtistReleaseData>
            {
                new ArtistReleaseData("artist4", "release5"),
                new ArtistReleaseData("artist5", "release6"),
                new ArtistReleaseData("artist6", "release7")
            };

            LibraryData libraryData1 = new LibraryData(ardList1);

            LibraryData libraryData2 = new LibraryData(ardList2);

            LibraryDiff libraryDiff = new LibraryDiff(libraryData1, libraryData2);

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

            var missingAlbums = libraryDiff.GetMissingAlbums();
            Assert.AreEqual(missingAlbums.Count, 2);
            Assert.AreEqual(missingAlbums[0], new ArtistReleaseData("artist1", "release2"));
            Assert.AreEqual(missingAlbums[1], new ArtistReleaseData("artist2", "release1"));
        }
    }
}
