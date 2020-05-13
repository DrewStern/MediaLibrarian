using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MusicLibraryCompareTool.UnitTests
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

        [TestMethod]
        public void GivenTwoEmptyLibraries_WhenCompared_ThenShouldFindNoDifferentArtists()
        {
            var artistDiffs = _musicLibraryCompareService.GetArtistDiffs(_musicLibraryTestData.GetEmptyLibrary(), _musicLibraryTestData.GetEmptyLibrary());
            Assert.AreEqual(artistDiffs.Count, 0);
        }

        [TestMethod]
        public void GivenTwoEmptyLibraries_WhenCompared_ThenShouldFindNoDifferentReleases()
        {
            var releaseDiffs = _musicLibraryCompareService.GetReleaseDiffs(_musicLibraryTestData.GetEmptyLibrary(), _musicLibraryTestData.GetEmptyLibrary());
            Assert.AreEqual(releaseDiffs.Count, 0);
        }

        [TestMethod]
        public void GivenTwoIdenticalLibraries_WhenCompared_ThenShouldFindNoDifferentArtists()
        {
            var artistDiffs = _musicLibraryCompareService.GetArtistDiffs(_musicLibraryTestData.GetManyArtistsToManyReleasesLibrary(), _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary());
            Assert.AreEqual(artistDiffs.Count, 0);
        }

        [TestMethod]
        public void GivenTwoIdenticalLibraries_WhenCompared_ThenShouldFindNoDifferentReleases()
        {
            var releaseDiffs = _musicLibraryCompareService.GetReleaseDiffs(_musicLibraryTestData.GetManyArtistsToManyReleasesLibrary(), _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary());
            Assert.AreEqual(releaseDiffs.Count, 0);
        }

        [TestMethod]
        public void GivenTwoDifferentLibraries_WhenCompared_ShouldFindDifferences()
        {
            var artistDiffs = _musicLibraryCompareService.GetArtistDiffs(_musicLibraryTestData.GetManyArtistsToManyReleasesLibrary(), _musicLibraryTestData.GetDisjointSimpleLibrary());
            var releaseDiffs = _musicLibraryCompareService.GetReleaseDiffs(_musicLibraryTestData.GetManyArtistsToManyReleasesLibrary(), _musicLibraryTestData.GetDisjointSimpleLibrary());

            Assert.AreEqual(artistDiffs.Count, 3);
            Assert.AreEqual(releaseDiffs.Count, 3);
        }

        [TestMethod]
        public void GivenTwoArtistsWithSameNameButDifferentCountries_WhenCompared_ShouldFindDifferences()
        {
            var artistDiffs1 = _musicLibraryCompareService.GetArtistDiffs(_musicLibraryTestData.GetMultipleArtistsWithSameNameDifferentCountry1Library(), _musicLibraryTestData.GetMultipleArtistsWithSameNameDifferentCountry2Library());
            var artistDiffs2 = _musicLibraryCompareService.GetArtistDiffs(_musicLibraryTestData.GetMultipleArtistsWithSameNameDifferentCountry2Library(), _musicLibraryTestData.GetMultipleArtistsWithSameNameDifferentCountry1Library());

            Assert.AreEqual(artistDiffs1.Count, 2);
            Assert.AreEqual(artistDiffs2.Count, 2);
        }

        [TestMethod]
        public void GivenAnEmptyLibraryAndANonEmptyLibrary_WhenSummed_ShouldResultInTheOriginalNonEmptyLibrary()
        {
            var expected = _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary();
            var actual = _musicLibraryCompareService.GetSum(_musicLibraryTestData.GetEmptyLibrary(), _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary());

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAnEmptyLibraryAndANonEmptyLibrary_WhenLeftOutersected_ShouldResultInAnEmptyLibrary()
        {
            var expected = _musicLibraryTestData.GetEmptyLibrary();
            var actual = _musicLibraryCompareService.GetLeftOutersection(_musicLibraryTestData.GetEmptyLibrary(), _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary());

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenAnEmptyLibraryAndANonEmptyLibrary_WhenRightOutersected_ShouldResultInTheOriginalNonEmptyLibrary()
        {
            var expected = _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary();
            var actual = _musicLibraryCompareService.GetRightOutersection(_musicLibraryTestData.GetEmptyLibrary(), _musicLibraryTestData.GetManyArtistsToManyReleasesLibrary());

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WriteLibraryDiff()
        {
            var artistDiffs = _musicLibraryCompareService.GetArtistDiffs(_musicLibraryTestData.GetRandomLibraryOne(), _musicLibraryTestData.GetRandomLibraryTwo());
            var releaseDiffs = _musicLibraryCompareService.GetReleaseDiffs(_musicLibraryTestData.GetRandomLibraryOne(), _musicLibraryTestData.GetRandomLibraryTwo());

            Assert.AreEqual(artistDiffs.Count, 0);
            Assert.AreEqual(releaseDiffs.Count, _musicLibraryTestData.GetRandomLibraryOneSubtractTwo().Releases.Count);
        }
    }
}
