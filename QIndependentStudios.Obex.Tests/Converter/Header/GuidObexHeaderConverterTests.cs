using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Header;
using QIndependentStudios.Obex.Header;
using System;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Header
{
    [TestClass]
    public class GuidObexHeaderConverterTests
    {
        private static readonly byte[] _testHeaderData = new byte[]
        {
            0x46, 0x00, 0x13, 0xBB, 0x58, 0x2B, 0x40, 0x42,
            0x0C, 0x11, 0xDB, 0xB0, 0xDE, 0x08, 0x00, 0x20,
            0x0C, 0x9A, 0x66
        };
        private static readonly ObexHeader _testHeader = GuidObexHeader.Create(ObexHeaderId.Target,
            19,
            Guid.Parse("bb582b40-420c-11db-b0de-0800200c9a66"));

        private readonly GuidObexHeaderConverter _converter = new GuidObexHeaderConverter();

        [TestMethod]
        public void FromBytes_GivenHeaderByteData_ReturnsHeaderObject()
        {
            var actual = _converter.FromBytes(_testHeaderData);

            Assert.AreEqual(ObexHeaderId.Target, actual.Id);
            Assert.IsInstanceOfType(actual, typeof(GuidObexHeader));

            var actualHeader = (GuidObexHeader)actual;
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
