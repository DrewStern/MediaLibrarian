using MusicLibraryCompareTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace MusicLibraryCompareTool.UnitTests
{
    [TestClass]
    public class LibraryTests
    {
        // TODO: Turn this into an integration test
        [TestMethod]
        public void CanOnlyLoadDataFromExistantDiskLocations()
        {
            // use a new directory on disk that has no content
            DirectoryInfo libraryPath = new DirectoryInfo("C:\\iDontExist");

            Assert.ThrowsException<ArgumentException>(() => new Library(libraryPath));
        }

        // TODO: Turn this into an integration test
        [TestMethod]
        public void EmptyLibraryShouldHaveNothing()
        {
            // use a new directory on disk that has no content
            DirectoryInfo libraryPath = new DirectoryInfo("C:\\iExistButAmEmpty");

            // TODO: I guess this could fail if the folder already exists on disk...
            libraryPath.Create();

            Library l = new Library(libraryPath);

            Assert.AreEqual(l.Collection.Count, 0);
            Assert.AreEqual(l.Artists.Count, 0);
            Assert.AreEqual(l.Releases.Count, 0);

            // TODO: I guess this could leave the folder on disk if the Assert above fails...
            libraryPath.Delete();
        }

        // TODO: Turn this into an integration test
        [TestMethod]
        public void ConstructFromDisk()
        {
            // TODO: I guess this could fail if the folders already exist on disk...
            DirectoryInfo rootPath = new DirectoryInfo("C:\\iExist");
            rootPath.Create();

            DirectoryInfo a1path = new DirectoryInfo(rootPath + "\\artist1");
            a1path.Create();
            DirectoryInfo a2path = new DirectoryInfo(rootPath + "\\artist2");
            a1path.Create();

            DirectoryInfo a1r1path = new DirectoryInfo(a1path + "\\release1");
            a1r1path.Create();
            DirectoryInfo a1r2path = new DirectoryInfo(a1path + "\\release2");
            a1r2path.Create();
            DirectoryInfo a2r1path = new DirectoryInfo(a2path + "\\release1");
            a2r1path.Create();
            DirectoryInfo a2r2path = new DirectoryInfo(a2path + "\\release2");
            a2r2path.Create();

            Library l = new Library(rootPath);

            Assert.AreEqual(l.Collection.Count, 4);
            Assert.AreEqual(l.Artists.Count, 2);
            Assert.AreEqual(l.Releases.Count, 4);

            // TODO: I guess this could leave the folders on disk if the Assert above fails...
            a1r1path.Delete();
            a1r2path.Delete();
            a2r1path.Delete();
            a2r2path.Delete();
            a1path.Delete();
            a2path.Delete();
            rootPath.Delete();
        }

        [TestMethod]
        public void LibraryNotEqualWithNonLibrary()
        {
            Library l = new Library(new List<LibraryItem>());

            var expected = false;
            var actual = l.Equals(null);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestEmptyLibraryNotEqualWithNonEmptyLibrary()
        {
            Library l1 = LibraryTestData.EmptyLibrary;
            Library l2 = LibraryTestData.OneArtistToManyReleasesLibrary;

            var expected = false;
            var actual = l1.Equals(l2);

            Assert.AreEqual(expected, actual);
        }

        /*
         * This is the reverse of the test above. Perhaps they could be combined?
         */
        [TestMethod]
        public void TestNonEmptyLibraryNotEqualWithEmptyLibrary()
        {
            Library l1 = LibraryTestData.OneArtistToManyReleasesLibrary;
            Library l2 = LibraryTestData.EmptyLibrary;

            var expected = false;
            var actual = l1.Equals(l2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLibraryToString()
        {
            Library l = LibraryTestData.OneArtistToManyReleasesLibrary;

            var expected =
                "artist1 - release1" + Environment.NewLine +
                "artist1 - release2";
            var actual = l.ToString();

            Assert.AreEqual(expected, actual);
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
        public void LibraryDoesntHaveDuplicateItems()
        {
            Library l = LibraryTestData.DuplicatedDataLibrary;
            Assert.AreEqual(l.Collection.Count, 1);
            Assert.AreEqual(l.Artists.Count, 1);
            Assert.AreEqual(l.Releases.Count, 1);
        }

        [TestMethod]
        public void TestEmptyLibraryPlusNonEmptyLibraryEqualsNonEmptyLibrary()
        {
            Library empty = LibraryTestData.EmptyLibrary;
            empty.AddToCollection(LibraryTestData.OneArtistToManyReleasesLibrary);
            Assert.AreEqual(empty.Collection.Count, 2);
            Assert.AreEqual(empty.Artists.Count, 1);
            Assert.AreEqual(empty.Releases.Count, 2);
        }
    }
}
