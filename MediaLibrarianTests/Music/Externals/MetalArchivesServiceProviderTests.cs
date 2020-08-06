using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaLibrarian.UnitTests
{
    [TestClass]
    public class MetalArchivesServiceProviderTests
    {
        private MetalArchivesServiceProvider _metalArchivesServiceProvider = new MetalArchivesServiceProvider();

        [TestMethod]
        public void GivenAServiceProvider_WhenFaultyRequestIsProcessed_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _metalArchivesServiceProvider.Process(null));
        }
    }
}
