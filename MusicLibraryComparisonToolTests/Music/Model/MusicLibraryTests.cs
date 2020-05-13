using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace MediaLibraryCompareTool.UnitTests
{
    [TestClass]
    public class MusicLibraryTests
    {
        private static TestContext _testContext;
        private static MusicLibraryTestData _musicLibraryTestData;

        [ClassInitialize]
        public static void InitializeMusicLibraryTests(TestContext testContext)
        {
            _testContext = testContext;
            _musicLibraryTestData = new MusicLibraryTestData();
        }

        // TODO: Turn this into an integration test
        [TestMethod]
        public void GivenALocationOnDiskWhichDoesNotExist_WhenAttemptToParseIntoMusicLibrary_ThenExceptionIsThrown()
        {
            // use a new directory on disk that has no content
            DirectoryInfo libraryPath = new DirectoryInfo("C:\\iDontExist");

            Assert.ThrowsException<ArgumentException>(() => new MusicLibrary(libraryPath));
        }

        // TODO: Turn this into an integration test
        [TestMethod]
        public void GivenAnEmptyDirectoryOnDisk_WhenParsedIntoAMusicLibrary_ThenMusicLibraryShouldBeEmpty()
        {
            // use a new directory on disk that has no content
            DirectoryInfo libraryPath = new DirectoryInfo("C:\\iExistButAmEmpty");

            // TODO: I guess this could fail if the folder already exists on disk...
            libraryPath.Create();

            MusicLibrary l = new MusicLibrary(libraryPath);

            Assert.AreEqual(l.Collection.Count, 0);
            Assert.AreEqual(l.Artists.Count, 0);
            Assert.AreEqual(l.Releases.Count, 0);

            // TODO: I guess this could leave the folder on disk if the Assert above fails...
            libraryPath.Delete();
        }

        // TODO: Turn this into an integration test
        [TestMethod]
        public void GivenANonEmptyDirectoryOnDisk_WhenParsedIntoAMusicLibrary_ThenMusicLibraryShouldBePopulated()
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

            MusicLibrary l = new MusicLibrary(rootPath);

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
        public void GivenSomeMusicLibrary_WhenComparedWithNull_ThenShouldNotBeEqual()
        {
            MusicLibrary l = new MusicLibrary(new List<MusicLibraryItem>());

            var expected = false;
            var actual = l.Equals(null);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenEmptyMusicLibrary_WhenComparedWithNonEmptyLibrary_ThenShouldNotBeEqual()
        {
            MusicLibrary l1 = _musicLibraryTestData.GetEmptyLibrary();
            MusicLibrary l2 = _musicLibraryTestData.GetOneArtistToManyReleasesLibrary();

            var expected = false;
            var actual = l1.Equals(l2);

            Assert.AreEqual(expected, actual);
        }

        /*
         * This is the reverse of the test above. Perhaps they could be combined?
         */
        [TestMethod]
        public void GivenNonEmptyMusicLibrary_WhenComparedWithEmptyLibrary_ThenShouldNotBeEqual()
        {
            MusicLibrary l1 = _musicLibraryTestData.GetOneArtistToManyReleasesLibrary();
            MusicLibrary l2 = _musicLibraryTestData.GetEmptyLibrary();

            var expected = false;
            var actual = l1.Equals(l2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenNonEmptyMusicLibrary_WhenPrintedToString_ThenMatchesExpectedFormat()
        {
            MusicLibrary l = _musicLibraryTestData.GetOneArtistToManyReleasesLibrary();

            var expected =
                "artist1 - release1" + Environment.NewLine +
                "artist1 - release2";
            var actual = l.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenMusicLibraryWithMultipleReleasesByTheSameArtist_WhenArtistCollectionAccessed_ThenNoDuplicateIsReturned()
        {
            MusicLibrary l = _musicLibraryTestData.GetOneArtistToManyReleasesLibrary();
            Assert.AreEqual(l.Collection.Count, 2);
            Assert.AreEqual(l.Artists.Count, 1);
            Assert.AreEqual(l.Releases.Count, 2);
        }

        [TestMethod]
        public void GivenMusicLibraryWithDuplicateItems_WhenAccessed_ThenDuplicateItemsIgnored()
        {
            MusicLibrary l = _musicLibraryTestData.GetDuplicatedDataLibrary();
            Assert.AreEqual(l.Collection.Count, 1);
            Assert.AreEqual(l.Artists.Count, 1);
            Assert.AreEqual(l.Releases.Count, 1);
        }

        [TestMethod]
        public void GivenEmptyLibrary_WhenNonEmptyLibraryAddedToCollection_ThenCollectionIsNonEmpty()
        {
            MusicLibrary empty = _musicLibraryTestData.GetEmptyLibrary();
            empty.AddToCollection(_musicLibraryTestData.GetOneArtistToManyReleasesLibrary());
            Assert.AreEqual(empty.Collection.Count, 2);
            Assert.AreEqual(empty.Artists.Count, 1);
            Assert.AreEqual(empty.Releases.Count, 2);
        }
    }
}
