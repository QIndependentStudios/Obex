using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Header;
using QIndependentStudios.Obex.Header;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Header
{
    [TestClass]
    public class ByteObexHeaderConverterTests
    {
        private const byte TestHeaderValue = 0x01;
        private static readonly byte[] _testHeaderData = new byte[] { 0x97, 0x01 };
        private static readonly ObexHeader _testHeader =
            ByteObexHeader.Create(ObexHeaderId.SingleResponseMode, TestHeaderValue);

        private readonly ByteObexHeaderConverter _converter = new ByteObexHeaderConverter();

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
