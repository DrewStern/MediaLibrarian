using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaLibraryCompareTool.UnitTests
{
    [TestClass]
    public class ReleaseDataTests
    {
        [TestMethod]
        public void GivenARelease_WhenComparedToAnIdenticalRelease_ThenTheyAreEqual()
        {
            ReleaseData ad1 = new ReleaseData("releaseName");
            ReleaseData ad2 = new ReleaseData("releaseName");

            Assert.IsTrue(ad1.Equals(ad2));
        }

        [TestMethod]
        public void GivenARelease_WhenComparedToAReleaseWithIdenticalNameButDifferentType_ThenTheyAreNotEqual()
        {
            ReleaseData ad1 = new ReleaseData("releaseName");
            ReleaseData ad2 = new ReleaseData("releaseName", "demo");

            Assert.IsFalse(ad1.Equals(ad2));
        }

        [TestMethod]
        public void GivenARelease_WhenComparedToAReleaseWithDifferentNameButIdenticalType_ThenTheyAreNotEqual()
        {
            ReleaseData ad1 = new ReleaseData("releaseName1", "demo");
            ReleaseData ad2 = new ReleaseData("releaseName2", "demo");

            Assert.IsFalse(ad1.Equals(ad2));
        }
    }
}
