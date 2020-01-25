using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class LibraryDataTests
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
            LibraryData l = LibraryTestData.OneArtistToManyReleasesLibrary;
            Assert.AreEqual(l.Artists.Count, 1);
            Assert.AreEqual(l.Releases.Count, 2);
        }

        [TestMethod]
        public void LibraryDoesntHaveDuplicateArtistReleases()
        {
            LibraryData l = LibraryTestData.DuplicatedDataLibrary;
            Assert.AreEqual(l.Artists.Count, 1);
            Assert.AreEqual(l.Releases.Count, 1);
            Assert.AreEqual(l.EntireCollection.Count, 1);
        }

        [TestMethod]
        public void LibraryHasArtists()
        {
            LibraryData l = LibraryTestData.ManyArtistsToManyRelasesLibrary;
            Assert.AreEqual(l.Artists.Count, 3);
            Assert.AreEqual(l.Releases.Count, 3);
            Assert.AreEqual(l.EntireCollection.Count, 9);
        }
    }
}
