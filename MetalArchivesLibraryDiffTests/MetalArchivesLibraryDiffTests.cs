using MetalArchivesLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class MetalArchivesLibraryDiffTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LibraryDataCanOnlyLoadFromExistantLocations()
        {
            // use a new directory on disk that has no content
            DirectoryInfo libraryPath = new DirectoryInfo("C:\\toDelete");

            LibraryData l = new LibraryData(libraryPath);
        }

        [TestMethod]
        public void EmptyLibraryShouldHaveNoArtists()
        {
            // use a new directory on disk that has no content
            DirectoryInfo libraryPath = new DirectoryInfo("C:\\todo");

            LibraryData l = new LibraryData(libraryPath);

            Assert.AreEqual(l.Artists.Count, 0);
        }

        //[TestMethod]
        //public void EmptyLibraryShouldHaveNoDiffs()
        //{
        //    // use a new directory on disk that has no content
        //    string libraryPath = "C:\todo";

        //    LibraryData l = new LibraryData(libraryPath);

        //    Assert.AreEqual(l.FindDiffs().Count, 0);
        //}
    }
}
