using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Header;
using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Header
{
    [TestClass]
    public class AppParamObexHeaderConverterTests
    {
        private static readonly List<ObexAppParameter> _testHeaderValue = new List<ObexAppParameter>
        {
            ObexAppParameter.Create(0x01, 0xFF, 0xFF),
            ObexAppParameter.Create(0x02, 0x00, 0x00)
        };
        private static readonly byte[] _testHeaderData = new byte[]
        {
            0x4C, 0x00, 0x0B, 0x01, 0x02, 0xFF, 0xFF, 0x02,
            0x02, 0x00, 0x00
        };
        private static readonly ObexHeader _testHeader = AppParamObexHeader.Create(11, _testHeaderValue);

        private readonly AppParamObexHeaderConverter _converter = new AppParamObexHeaderConverter();

        [TestMethod]
        public void FromBytes_GivenHeaderByteData_ReturnsHeaderObject()
        {
            var actual = _converter.FromBytes(_testHeaderData);

            Assert.AreEqual(_testHeader, actual);
        }

        [TestMethod]
        public void ToBytes_GivenHeaderObject_ReturnsHeaderByteData()
        {
            var actual = _converter.ToBytes(_testHeader);

            Assert.IsTrue(_testHeaderData.SequenceEqual(actual));
        }
    }
}
