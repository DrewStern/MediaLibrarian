using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaLibrarian.UnitTests
{
    [TestClass]
    public class MusicLibraryComparerTests
    {
        private static TestContext _testContext;
        private static MusicLibraryTestDataFactory _musicLibraryTestData;
        private static MusicLibraryComparer _musicLibraryCompareService;

        [ClassInitialize]
        public static void InitializeMusicLibraryCompareServiceTests(TestContext testContext)
        {
            _testContext = testContext;
            _musicLibraryTestData = new MusicLibraryTestDataFactory();
            _musicLibraryCompareService = new MusicLibraryComparer();
        }

        #region Sum tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenSummedWithAnEmptyLibrary_ThenResultIsAnEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();

            var expected = emptyLibrary;
            var actual = _musicLibraryCompareService.Compare(emptyLibrary, emptyLibrary).Sum;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenSummedWithANonEmptyLibrary_ThenResultIsTheSameNonEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = nonEmptyLibrary;
            var actual = _musicLibraryCompareService.Compare(emptyLibrary, nonEmptyLibrary).Sum;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenANonEmptyLibrary_WhenSummedWithAProperSubsetOfItself_ThenResultIsTheOriginalNonEmptyLibrary()
        {
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();
            var nonEmptyLibrarySubset = _musicLibraryTestData.GetRandomLibraryTwo();

            var expected = nonEmptyLibrary;
            var actual = _musicLibraryCompareService.Compare(nonEmptyLibrary, nonEmptyLibrarySubset).Sum;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenANonEmptyLibrary_WhenSummedWithAProperSupersetOfItself_ThenResultIsTheSameSuperset()
        {
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();
            var nonEmptyLibrarySuperset = _musicLibraryTestData.GetSumOf_ManyToManyLibrary_AndDisjointSimpleLibrary();

            var expected = nonEmptyLibrarySuperset;
            var actual = _musicLibraryCompareService.Compare(nonEmptyLibrary, nonEmptyLibrarySuperset).Sum;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Intersection tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenIntersectedWithAnEmptyLibrary_ThenResultIsAnEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();

            var expected = emptyLibrary;
            var actual = _musicLibraryCompareService.Compare(emptyLibrary, emptyLibrary).Intersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenIntersectedWithANonEmptyLibrary_ThenResultIsAnEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = emptyLibrary;
            var actual = _musicLibraryCompareService.Compare(emptyLibrary, nonEmptyLibrary).Intersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenANonEmptyLibrary_WhenIntersectedWithAProperSubsetOfItself_ThenResultIsTheSameSubset()
        {
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();
            var nonEmptyLibrarySubset = _musicLibraryTestData.GetRandomLibraryTwo();

            var expected = nonEmptyLibrarySubset;
            var actual = _musicLibraryCompareService.Compare(nonEmptyLibrary, nonEmptyLibrarySubset).Intersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenANonEmptyLibrary_WhenIntersectedWithAProperSupersetOfItself_ThenResultIsTheOriginalNonEmptyLibrary()
        {
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();
            var nonEmptyLibrarySuperset = _musicLibraryTestData.GetSumOf_ManyToManyLibrary_AndDisjointSimpleLibrary();

            var expected = nonEmptyLibrary;
            var actual = _musicLibraryCompareService.Compare(nonEmptyLibrary, nonEmptyLibrarySuperset).Intersection;

            Assert.AreEqual(expected, actual);
        }

        #endregion 

        #region LeftOutersection tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenLeftOutersectedWithANonEmptyLibrary_ThenResultIsAnEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = emptyLibrary;
            var actual = _musicLibraryCompareService.Compare(emptyLibrary, nonEmptyLibrary).LeftOutersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenANonEmptyLibrary_WhenLeftOutersectedWithAnEmptyLibrary_ThenResultIsTheOriginalNonEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = nonEmptyLibrary;
            var actual = _musicLibraryCompareService.Compare(nonEmptyLibrary, emptyLibrary).LeftOutersection;

            Assert.AreEqual(expected, actual);
        }

        // TODO: create test which does LeftOutersect vs two non-empty datasets

        #endregion

        #region RightOutersection tests

        [TestMethod]
        public void GivenAnEmptyLibrary_WhenRightOutersectedWithANonEmptyLibrary_ThenResultIsTheSameNonEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = nonEmptyLibrary;
            var actual = _musicLibraryCompareService.Compare(emptyLibrary, nonEmptyLibrary).RightOutersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenANonEmptyLibrary_WhenRightOutersectedWithAnEmptyLibrary_ThenResultIsAnEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = emptyLibrary;
            var actual = _musicLibraryCompareService.Compare(nonEmptyLibrary, emptyLibrary).RightOutersection;

            Assert.AreEqual(expected, actual);
        }

        // TODO: create test which does RightOutersect vs two non-empty datasets

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
        public void GivenAnEmptyLibrary_WhenFullOutersectedWithANonEmptyLibrary_ThenResultIsTheSameNonEmptyLibrary()
        {
            var emptyLibrary = _musicLibraryTestData.GetEmptyLibrary();
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = nonEmptyLibrary;
            var actual = _musicLibraryCompareService.Compare(emptyLibrary, nonEmptyLibrary).FullOutersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenANonEmptyLibrary_WhenFullOutersectedWithAnIdenticalLibrary_ThenShouldFindNoDifferences()
        {
            var nonEmptyLibrary = _musicLibraryTestData.GetManyToManyLibrary();

            var expected = _musicLibraryTestData.GetEmptyLibrary();
            var actual = _musicLibraryCompareService.Compare(nonEmptyLibrary, nonEmptyLibrary).FullOutersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenTwoDisjointNonEmptyLibraries_WhenFullOutersected_ThenResultShouldEqualTheirSum()
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
