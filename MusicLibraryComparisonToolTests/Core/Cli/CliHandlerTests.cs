using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MediaLibraryCompareTool.UnitTests
{
    [TestClass]
    public class CliHandlerTests
    {
        private enum FakeCliArgKey
        {
            FOO,
            BAR,
            BAZ
        }

        [TestMethod]
        public void GivenNoArgValues_WhenRegisteredAtRuntime_ThenAppRunsWithDefaultValues()
        {
            var noArgs = new string[] { string.Empty };

            var _cliHandler = new CliHandler();
            var _argRegistrar = new CliArgRegistrar<FakeCliArgKey>();

            var expected = new Dictionary<FakeCliArgKey, string>();
            expected.Add(FakeCliArgKey.FOO, string.Empty);
            expected.Add(FakeCliArgKey.BAR, string.Empty);
            expected.Add(FakeCliArgKey.BAZ, string.Empty);

            var actual = _cliHandler.GetCliArgRegistrar<FakeCliArgKey>(noArgs).Registry;

            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[FakeCliArgKey.FOO], actual[FakeCliArgKey.FOO]);
            Assert.AreEqual(expected[FakeCliArgKey.BAR], actual[FakeCliArgKey.BAR]);
            Assert.AreEqual(expected[FakeCliArgKey.BAZ], actual[FakeCliArgKey.BAZ]);
        }

        [TestMethod]
        public void GivenArgValues_WhenRegisteredAtRuntime_ThenAppRunsWithGivenValues()
        {
            var validArgs = new string[] 
            {
                "/baz:baz",
                "-foo=foo",
                "--bar~bar"
            };

            var _cliHandler = new CliHandler();
            var _argRegistrar = new CliArgRegistrar<FakeCliArgKey>();

            var expected = new Dictionary<FakeCliArgKey, string>();
            expected.Add(FakeCliArgKey.FOO, "foo");
            expected.Add(FakeCliArgKey.BAR, "bar");
            expected.Add(FakeCliArgKey.BAZ, "baz");

            var actual = _cliHandler.GetCliArgRegistrar<FakeCliArgKey>(validArgs).Registry;

            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[FakeCliArgKey.FOO], actual[FakeCliArgKey.FOO]);
            Assert.AreEqual(expected[FakeCliArgKey.BAR], actual[FakeCliArgKey.BAR]);
            Assert.AreEqual(expected[FakeCliArgKey.BAZ], actual[FakeCliArgKey.BAZ]);
        }
    }
}