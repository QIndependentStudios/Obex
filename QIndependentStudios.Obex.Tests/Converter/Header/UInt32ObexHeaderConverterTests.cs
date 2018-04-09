using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Header;
using QIndependentStudios.Obex.Header;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Header
{
    [TestClass]
    public class UInt32ObexHeaderConverterTests
    {
        private static readonly byte[] _testHeaderData = new byte[] { 0xCB, 0x00, 0x00, 0x09, 0x13 };
        private static readonly ObexHeader _testHeader =
            UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 2323);

        private readonly UInt32ObexHeaderConverter _converter = UInt32ObexHeaderConverter.Instance;

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
