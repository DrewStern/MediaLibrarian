using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MediaLibraryCompareTool.UnitTests
{
    [TestClass]
    public class CliHandlerTests
    {
        [TestMethod]
        public void GivenNoArgs_WhenParsed_ThenNothingAddedToMap()
        {
            var noArgs = new string[] { string.Empty };

            var _argRegistrar = new ArgRegistrar<MediaLibraryArgRegistry>();
            var _cliHandler = new MediaLibraryCompareToolCliHandler(_argRegistrar);
            var expected = new Dictionary<MediaLibraryArgRegistry, string>();
            expected.Add(MediaLibraryArgRegistry.INPUT, string.Empty);
            expected.Add(MediaLibraryArgRegistry.OUTPUT, string.Empty);
            expected.Add(MediaLibraryArgRegistry.ARTIST_NAME, string.Empty);
            expected.Add(MediaLibraryArgRegistry.ARTIST_COUNTRY, string.Empty);
            expected.Add(MediaLibraryArgRegistry.RELEASE_NAME, string.Empty);
            var actual = _cliHandler.ParseArgs(noArgs);

            Assert.AreEqual(expected, actual);
        }
    }
}
