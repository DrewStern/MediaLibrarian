using MetalArchivesLibraryDiffTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetalArchivesLibraryDiffTests
{
    [TestClass]
    public class MetalArchivesHttpRequestTests
    {
        [TestMethod]
        [Ignore("Test not finished")]
        public void TestHttpRequest()
        {
            var request = new MetalArchivesHttpRequest("!T.O.O.H.!");

            // TODO: make request - probably need to fake out the async call to the MetalArchivesService
        }
    }
}
