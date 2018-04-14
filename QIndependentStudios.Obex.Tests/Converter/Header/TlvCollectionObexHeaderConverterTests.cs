using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Header;
using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Header
{
    [TestClass]
    public class TlvCollectionObexHeaderConverterTests
    {
        private static readonly List<TlvTriplet> TestHeaderValue = new List<TlvTriplet>
        {
            TlvTriplet.Create(0x01, 0xFF, 0xFF),
            TlvTriplet.Create(0x02, 0x00, 0x00)
        };
        private static readonly byte[] TestHeaderData =
        {
            0x4C, 0x00, 0x0B, 0x01, 0x02, 0xFF, 0xFF, 0x02,
            0x02, 0x00, 0x00
        };
        private static readonly ObexHeader TestHeader = TlvCollectionObexHeader.Create(ObexHeaderId.ApplicationParameter, 11, TestHeaderValue);

        private readonly TlvCollectionObexHeaderConverter _converter = TlvCollectionObexHeaderConverter.Instance;

        [TestMethod]
        public void FromBytes_GivenHeaderByteData_ReturnsHeaderObject()
        {
            var actual = _converter.FromBytes(TestHeaderData);

            Assert.AreEqual(TestHeader, actual);
        }

        [TestMethod]
        public void ToBytes_GivenHeaderObject_ReturnsHeaderByteData()
        {
            var actual = _converter.ToBytes(TestHeader);

            Assert.IsTrue(TestHeaderData.SequenceEqual(actual));
        }
    }
}
