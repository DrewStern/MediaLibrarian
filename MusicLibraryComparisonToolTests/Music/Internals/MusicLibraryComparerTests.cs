using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaLibraryCompareTool.UnitTests
{
    [TestClass]
    public class MusicLibraryComparerTests
    {
        private static TestContext _testContext;
        private static MusicLibraryTestData _musicLibraryTestData;
        private static MusicLibraryComparer _musicLibraryCompareService;

        [ClassInitialize]
        public static void InitializeMusicLibraryCompareServiceTests(TestContext testContext)
        {
            _testContext = testContext;
            _musicLibraryTestData = new MusicLibraryTestData();
            _musicLibraryCompareService = new MusicLibraryComparer();
        }

        #region Sum tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenSummedWithAnEmptyLibrary_ThenResultsInAnEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();

            var expected = emptyLibrary;
            var actual = _musicLibraryCompareService.Compare(emptyLibrary, emptyLibrary).Sum;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenSummedWithANonEmptyLibrary_ThenResultsInTheSameNonEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = nonEmptyLibrary;
            var actual = _musicLibraryCompareService.Compare(emptyLibrary, nonEmptyLibrary).Sum;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenANonEmptyLibrary_WhenSummedWithAProperSubsetOfItself_ThenResultsInTheOriginalNonEmptyLibrary()
        {
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();
            var nonEmptyLibrarySubset = _musicLibraryTestData.GetRandomLibraryTwo();

            var expected = nonEmptyLibrary;
            var actual = _musicLibraryCompareService.Compare(nonEmptyLibrary, nonEmptyLibrarySubset).Sum;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Intersection tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenIntersectedWithAnEmptyLibrary_ThenTheResultIsAnEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();

            var expected = emptyLibrary;
            var actual = _musicLibraryCompareService.Compare(emptyLibrary, emptyLibrary).Intersection;

            Assert.AreEqual(expected, actual);
        }

        #endregion 

        #region LeftOutersection tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenLeftOutersectedWithANonEmptyLibrary_ThenResultsInAnEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = emptyLibrary;
            var actual = _musicLibraryCompareService.Compare(emptyLibrary, nonEmptyLibrary).LeftOutersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenANonEmptyLibrary_WhenLeftOutersectedWithAnEmptyLibrary_ThenResultsInTheOriginalNonEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = nonEmptyLibrary;
            var actual = _musicLibraryCompareService.Compare(nonEmptyLibrary, emptyLibrary).LeftOutersection;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region RightOutersection tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenRightOutersectedWithANonEmptyLibrary_ThenResultsInTheSameNonEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = nonEmptyLibrary;
            var actual = _musicLibraryCompareService.Compare(emptyLibrary, nonEmptyLibrary).RightOutersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenANonEmptyLibrary_WhenRightOutersectedWithAnEmptyLibrary_ThenResultsInAnEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = emptyLibrary;
            var actual = _musicLibraryCompareService.Compare(nonEmptyLibrary, emptyLibrary).RightOutersection;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region FullOutersection tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenFullOutersectedWithAnEmptyLibrary_ThenShouldFindNoDifferences()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();

            var expected = emptyLibrary;
            var actual = _musicLibraryCompareService.Compare(emptyLibrary, emptyLibrary).FullOutersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenSomeLibrary_WhenFullOutersectedWithAnIdenticalLibrary_ThenShouldFindNoDifferences()
        {
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = _musicLibraryTestData.GetEmptyLibrary();
            var actual = _musicLibraryCompareService.Compare(nonEmptyLibrary, nonEmptyLibrary).FullOutersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenTwoDisjointLibraries_WhenFullOutersected_ThenResultShouldEqualTheirSum()
        {
            var library1 = _musicLibraryTestData.GetManyToManyLibrary();
            var library2 = _musicLibraryTestData.GetDisjointSimpleLibrary();

            var expected = _musicLibraryTestData.GetSumOf_ManyToManyLibrary_AndDisjointSimpleLibrary();
            var actual = _musicLibraryCompareService.Compare(library1, library2).FullOutersection;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        [TestMethod]
        public void WriteLibraryDiff()
        {
            var releaseDiffs = _musicLibraryCompareService.Compare(
                _musicLibraryTestData.GetRandomLibraryOne(), 
                _musicLibraryTestData.GetRandomLibraryTwo());

            Assert.AreEqual(releaseDiffs.FullOutersection.Collection.Count, _musicLibraryTestData.GetRandomLibraryOneSubtractTwo().Releases.Count);
        }
    }
}
