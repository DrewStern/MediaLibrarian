using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaLibraryCompareTool.UnitTests
{
    [TestClass]
    public class MusicLibraryCompareServiceTests
    {
        private static TestContext _testContext;
        private static MusicLibraryTestData _musicLibraryTestData;
        private static MusicLibraryCompareService _musicLibraryCompareService;

        [ClassInitialize]
        public static void InitializeMusicLibraryCompareServiceTests(TestContext testContext)
        {
            _testContext = testContext;
            _musicLibraryTestData = new MusicLibraryTestData();
            _musicLibraryCompareService = new MusicLibraryCompareService();
        }

        #region GetReleaseDiffs tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenComparedWithAnEmptyLibrary_ThenShouldFindNoDifferentReleases()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();

            var expected = 0;
            var actual = _musicLibraryCompareService.GetReleaseDiffs(emptyLibrary, emptyLibrary).Collection.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenSomeLibrary_WhenComparedWithAnIdenticalLibrary_ThenShouldFindNoDifferentReleases()
        {
            var nonEmptyLibrary = _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary();

            var expected = 0;
            var actual = _musicLibraryCompareService.GetReleaseDiffs(nonEmptyLibrary, nonEmptyLibrary).Collection.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenTwoDifferentLibraries_WhenReleasesCompared_ThenShouldFindDifferences()
        {
            var releaseDiffs = _musicLibraryCompareService.GetReleaseDiffs(
                _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary(),
                _musicLibraryTestData.GetDisjointSimpleLibrary());

            Assert.AreEqual(releaseDiffs.Collection.Count, 3);
        }

        #endregion

        #region GetSum tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenSummedWithANonEmptyLibrary_ThenResultsInTheSameNonEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary();

            var expected = nonEmptyLibrary;
            var actual = _musicLibraryCompareService.GetSum(emptyLibrary, nonEmptyLibrary);

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region GetLeftOutersection tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenLeftOutersectedWithANonEmptyLibrary_ThenResultsInAnEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary();

            var expected = emptyLibrary;
            var actual = _musicLibraryCompareService.GetLeftOutersection(emptyLibrary, nonEmptyLibrary);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenANonEmptyLibrary_WhenLeftOutersectedWithAnEmptyLibrary_ThenResultsInTheOriginalNonEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary();

            var expected = nonEmptyLibrary;
            var actual = _musicLibraryCompareService.GetLeftOutersection(nonEmptyLibrary, emptyLibrary);

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region GetRightOutersection tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenRightOutersectedWithANonEmptyLibrary_ThenResultsInTheSameNonEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary();

            var expected = nonEmptyLibrary;
            var actual = _musicLibraryCompareService.GetRightOutersection(emptyLibrary, nonEmptyLibrary);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenANonEmptyLibrary_WhenRightOutersectedWithAnEmptyLibrary_ThenResultsInAnEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary();

            var expected = emptyLibrary;
            var actual = _musicLibraryCompareService.GetRightOutersection(nonEmptyLibrary, emptyLibrary);

            Assert.AreEqual(expected, actual);
        }

        #endregion

        [TestMethod]
        public void WriteLibraryDiff()
        {
            var releaseDiffs = _musicLibraryCompareService.GetReleaseDiffs(
                _musicLibraryTestData.GetRandomLibraryOne(), 
                _musicLibraryTestData.GetRandomLibraryTwo());

            Assert.AreEqual(releaseDiffs.Collection.Count, _musicLibraryTestData.GetRandomLibraryOneSubtractTwo().Releases.Count);
        }
    }
}
