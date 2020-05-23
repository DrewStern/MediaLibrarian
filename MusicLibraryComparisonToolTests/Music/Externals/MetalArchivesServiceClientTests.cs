using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaLibraryCompareTool.UnitTests
{
    [TestClass]
    public class MetalArchivesClientTests
    {
        private MetalArchivesServiceClient _client;

        [TestInitialize]
        public void OnInitialize()
        {
            var maService = new MetalArchivesServiceProvider();
            var maResponseParser = new MetalArchivesResponseParser();
            _client = new MetalArchivesServiceClient(maService, maResponseParser);
        }

        [TestMethod]
        public void GivenAFaultyRequest_WhenSubmitted_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _client.Submit(null));
        }
    }
}
