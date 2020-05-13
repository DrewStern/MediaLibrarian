using MusicLibraryCompareTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MusicLibraryCompareTool.UnitTests
{
    [TestClass]
    public class MetalArchivesServiceTests
    {
        private MetalArchivesService _metalArchivesService = new MetalArchivesService();

        [TestMethod]
        public void TestRequestMayNotBeNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _metalArchivesService.Submit(null));
        }
    }
}
