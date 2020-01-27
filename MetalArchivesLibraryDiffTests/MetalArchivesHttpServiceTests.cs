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
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRequestMayNotBeNull()
        {
            _metalArchivesHttpService.Submit(null);
        }
    }
}
