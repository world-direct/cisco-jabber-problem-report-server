using System;
using CiscoJabberProblemReportServer.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NConfig;

namespace CiscoJabberProblemReportServerTests.Unit
{
    [TestClass]
    public class ProblemRptControllerTests
    {
        private static bool _setNconfigAsSystemDefault = false;
        private ProblemRPTController _controller;

        public ProblemRptControllerTests()
        {
            var nconfiguration = NConfigurator.UsingFiles(@".\Config\Operations.config");
            if (!_setNconfigAsSystemDefault)
            {
                nconfiguration.SetAsSystemDefault();
                _setNconfigAsSystemDefault = true;
            }
        }

        [TestInitialize]
        public void SetUp()
        {
            _controller = new ProblemRPTController();
        }

        [DataTestMethod]
        [DataRow("Jabber-iOS-12.0.0.261399-20180522_224538.zip.esc")]
        [DataRow("Jabber-iOS-12.0.0.261399-20180522_224538.zip.esk")]
        [DataRow("Jabber-Win-12.0.1.63173-20180522_235428-Windows_10_Enterprise.zip.enc")]
        [DataRow("Jabber-Win-12.0.1.63173-20180522_235428-Windows_10_Enterprise.zip.esk")]
        [DataRow("Jabber-Win-12.0.1.63173-20180601_140310-Windows_10_Enterprise.zip.enc")]
        [DataRow("Jabber-Win-12.0.1.63173-20180601_140310-Windows_10_Enterprise.zip.esk")]
        public void ValidFileNames_pass(string fileName)
        {
            _controller.ValidateFileName(fileName);
        }

        [DataTestMethod]
        [DataRow("Jabber-Win-12.0.1.63173-20180522_235428-Windows_10_Enterprise.zip")]
        [DataRow("tmp.txt")]
        [DataRow("test.txt")]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidFileNames_throw(string fileName)
        {
            _controller.ValidateFileName(fileName);
        }
    }
}
