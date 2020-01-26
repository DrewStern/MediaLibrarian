using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class LibraryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CanOnlyLoadDataFromExistantDiskLocations()
        {
            // use a new directory on disk that has no content
            DirectoryInfo libraryPath = new DirectoryInfo("C:\\iDontExist");

            Library l = new Library(libraryPath);
        }

        [TestMethod]
        public void EmptyLibraryShouldHaveNoArtistReleaseData()
        {
            // use a new directory on disk that has no content
            DirectoryInfo libraryPath = new DirectoryInfo("C:\\iExistButAmEmpty");

            libraryPath.Create();

            Library l = new Library(libraryPath);

            Assert.AreEqual(l.Collection.Count, 0);
            Assert.AreEqual(l.Artists.Count, 0);
            Assert.AreEqual(l.Releases.Count, 0);

            // TODO: I guess this could leave the folder on disk if the Assert above fails...
            libraryPath.Delete();
        }

        [TestMethod]
        public void LibraryDoesntHaveDuplicateArtists()
        {
            Library l = LibraryTestData.OneArtistToManyReleasesLibrary;
            Assert.AreEqual(l.Collection.Count, 2);
            Assert.AreEqual(l.Artists.Count, 1);
            Assert.AreEqual(l.Releases.Count, 2);
        }

        [TestMethod]
        public void LibraryDoesntHaveDuplicateArtistReleases()
        {
            Library l = LibraryTestData.DuplicatedDataLibrary;
            Assert.AreEqual(l.Collection.Count, 1);
            Assert.AreEqual(l.Artists.Count, 1);
            Assert.AreEqual(l.Releases.Count, 1);
        }
    }
}
