using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaLibrarian.UnitTests
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
            var maResponseFilterer = new MetalArchivesResponseFilterer();
            _client = new MetalArchivesServiceClient(maService, maResponseParser, maResponseFilterer);
        }

        [TestMethod]
        public void GivenAFaultyRequest_WhenSubmitted_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _client.Submit(null));
        }
    }
}
