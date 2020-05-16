using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaLibraryCompareTool.UnitTests
{
    [TestClass]
    public class MetalArchivesServiceProviderTests
    {
        private MetalArchivesServiceProvider _metalArchivesServiceProvider = new MetalArchivesServiceProvider();

        [TestMethod]
        public void GivenAServiceProvider_WhenFaultyRequestIsSubmitted_ThenAnExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _metalArchivesServiceProvider.Submit(null));
        }
    }
}
