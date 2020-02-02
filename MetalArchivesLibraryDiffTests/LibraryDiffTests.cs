using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class LibraryDiffTests
    {
        [TestMethod]
        public void TestIntersection_EmptyLibraryAndEmptyLibrary()
        {
            var l1 = new Library(new List<LibraryItem>());
            var l2 = new Library(new List<LibraryItem>());
            var ld = new LibraryDiff(l1, l2);

            var expected = new Library(new List<LibraryItem>());
            var actual = ld.Intersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestIntersect_LeftCollectionLargerThanRightCollection()
        {
            var l1 = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName1", "releaseName1"),
                new LibraryItem("artistName2", "releaseName2"),
                new LibraryItem("artistName3", "releaseName3"),
            });
            var l2 = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName1", "releaseName1"),
                new LibraryItem("artistName2", "releaseName2"),
            });

            var ld = new LibraryDiff(l1, l2);

            var expected = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName1", "releaseName1"),
                new LibraryItem("artistName2", "releaseName2"),
            });
            var actual = ld.Intersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestIntersect_RightCollectionLargerThanLeftCollection()
        {
            var l1 = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName5", "releaseName5"),
                new LibraryItem("artistName6", "releaseName6"),
            });
            var l2 = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName4", "releaseName4"),
                new LibraryItem("artistName5", "releaseName5"),
                new LibraryItem("artistName7", "releaseName7"),
            });

            var ld = new LibraryDiff(l1, l2);

            var expected = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName5", "releaseName5"),
            });
            var actual = ld.Intersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSum_EmptyLibraryAndEmptyLibrary()
        {
            var l1 = new Library(new List<LibraryItem>());
            var l2 = new Library(new List<LibraryItem>());
            var ld = new LibraryDiff(l1, l2);

            var expected = new Library(new List<LibraryItem>());
            var actual = ld.Sum;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLeftOutersection()
        {
            var l1 = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName1", "releaseName1"),
                new LibraryItem("artistName2", "releaseName2"),
                new LibraryItem("artistName3", "releaseName3"),
            });
            var l2 = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName3", "releaseName3"),
                new LibraryItem("artistName4", "releaseName4"),
            });
            var ld = new LibraryDiff(l1, l2);

            var expected = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName1", "releaseName1"),
                new LibraryItem("artistName2", "releaseName2"),
            });
            var actual = ld.LeftOutersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestRightOutersection()
        {
            var l1 = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName1", "releaseName1"),
                new LibraryItem("artistName2", "releaseName2"),
            });
            var l2 = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName2", "releaseName2"),
                new LibraryItem("artistName3", "releaseName3"),
                new LibraryItem("artistName4", "releaseName4"),
            });
            var ld = new LibraryDiff(l1, l2);

            var expected = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName3", "releaseName3"),
                new LibraryItem("artistName4", "releaseName4"),
            });
            var actual = ld.RightOutersection;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFullOutersection()
        {
            var l1 = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName1", "releaseName1"),
                new LibraryItem("artistName2", "releaseName2"),
                new LibraryItem("artistName3", "releaseName3"),
            });
            var l2 = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName1", "releaseName1"),
                new LibraryItem("artistName4", "releaseName4"),
            });
            var ld = new LibraryDiff(l1, l2);

            var expected = new Library(new List<LibraryItem>
            {
                new LibraryItem("artistName2", "releaseName2"),
                new LibraryItem("artistName3", "releaseName3"),
                new LibraryItem("artistName4", "releaseName4"),
            });
            var actual = ld.FullOutersection;

            Assert.AreEqual(expected, actual);
        }
    }
}
