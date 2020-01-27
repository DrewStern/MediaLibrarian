using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class MetalArchivesHttpServiceTests
    {
        private MetalArchivesHttpService _metalArchivesHttpService = new MetalArchivesHttpService();

        [TestMethod]
        public void TestRequestMayNotBeNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _metalArchivesHttpService.Submit(null));
        }
    }
}
