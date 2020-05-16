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

        #region TODO: turn these into integration tests

        [TestMethod]
        public void GivenALocationOnDiskWhichDoesNotExist_WhenAttemptToParseIntoMusicLibrary_ThenExceptionIsThrown()
        {
            // use a new directory on disk that has no content
            var libraryPath = new DirectoryInfo("C:\\iDontExist");

            Assert.ThrowsException<ArgumentException>(() => new MusicLibrary(libraryPath));
        }

        [TestMethod]
        public void GivenAnEmptyDirectoryOnDisk_WhenParsedIntoAMusicLibrary_ThenMusicLibraryShouldBeEmpty()
        {
            // use a new directory on disk that has no content
            var libraryPath = new DirectoryInfo("C:\\iExistButAmEmpty");

            // TODO: I guess this could fail if the folder already exists on disk...
            libraryPath.Create();

            var l = new MusicLibrary(libraryPath);

            Assert.AreEqual(l.Collection.Count, 0);
            Assert.AreEqual(l.Artists.Count, 0);
            Assert.AreEqual(l.Releases.Count, 0);

            // TODO: I guess this could leave the folder on disk if the Assert above fails...
            libraryPath.Delete();
        }

        [TestMethod]
        public void GivenANonEmptyDirectoryOnDisk_WhenParsedIntoAMusicLibrary_ThenMusicLibraryShouldBePopulated()
        {
            // TODO: I guess this could fail if the folders already exist on disk...
            var rootPath = new DirectoryInfo("C:\\iExist");
            rootPath.Create();

            var a1path = new DirectoryInfo(rootPath + "\\artist1");
            a1path.Create();
            var a2path = new DirectoryInfo(rootPath + "\\artist2");
            a1path.Create();

            var a1r1path = new DirectoryInfo(a1path + "\\release1");
            a1r1path.Create();
            var a1r2path = new DirectoryInfo(a1path + "\\release2");
            a1r2path.Create();
            var a2r1path = new DirectoryInfo(a2path + "\\release1");
            a2r1path.Create();
            var a2r2path = new DirectoryInfo(a2path + "\\release2");
            a2r2path.Create();

            var l = new MusicLibrary(rootPath);

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

        #endregion

        #region Equals tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenComparedWithNull_ThenShouldNotBeEqual()
        {
            var l = _musicLibraryTestData.GetEmptyLibrary();

            var expected = false;
            var actual = l.Equals(null);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenComparedWithNonEmptyLibrary_ThenShouldNotBeEqual()
        {
            var l1 = _musicLibraryTestData.GetEmptyLibrary();
            var l2 = _musicLibraryTestData.GetOneArtistToManyReleasesLibrary();

            var expected = false;
            var actual = l1.Equals(l2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenANonEmptyMusicLibrary_WhenComparedWithEmptyLibrary_ThenShouldNotBeEqual()
        {
            var l1 = _musicLibraryTestData.GetOneArtistToManyReleasesLibrary();
            var l2 = _musicLibraryTestData.GetEmptyLibrary();

            var expected = false;
            var actual = l1.Equals(l2);

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ToString tests

        [TestMethod]
        public void GivenANonEmptyMusicLibrary_WhenPrintedToString_ThenMatchesExpectedFormat()
        {
            var l = _musicLibraryTestData.GetOneArtistToManyReleasesLibrary();

            var expected =
                "artist1 - release1" + Environment.NewLine +
                "artist1 - release2";
            var actual = l.ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        [TestMethod]
        public void GivenLibraryWithMultipleReleasesByTheSameArtist_WhenArtistCollectionAccessed_ThenNoDuplicateIsReturned()
        {
            var l = _musicLibraryTestData.GetOneArtistToManyReleasesLibrary();
            Assert.AreEqual(2, l.Collection.Count);
            Assert.AreEqual(1, l.Artists.Count);
            Assert.AreEqual(2, l.Releases.Count);
        }

        [TestMethod]
        public void GivenLibraryWithDuplicateItems_WhenAccessed_ThenDuplicateItemsIgnored()
        {
            var l = _musicLibraryTestData.GetDuplicatedDataLibrary();
            Assert.AreEqual(1, l.Collection.Count);
            Assert.AreEqual(1, l.Artists.Count);
            Assert.AreEqual(1, l.Releases.Count);
        }

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenNonEmptyLibraryAddedToCollection_ThenCollectionIsNonEmpty()
        {
            var empty = _musicLibraryTestData.GetEmptyLibrary();
            empty.AddToCollection(_musicLibraryTestData.GetOneArtistToManyReleasesLibrary());

            var nonEmptyNow = empty;
            Assert.AreEqual(2, nonEmptyNow.Collection.Count);
            Assert.AreEqual(1, nonEmptyNow.Artists.Count);
            Assert.AreEqual(2, nonEmptyNow.Releases.Count);
        }
    }
}
