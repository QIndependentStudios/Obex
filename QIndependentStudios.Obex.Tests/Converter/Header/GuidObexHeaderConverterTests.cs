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
        private static readonly byte[] TestHeaderData =
        {
            0x46, 0x00, 0x13, 0xBB, 0x58, 0x2B, 0x40, 0x42,
            0x0C, 0x11, 0xDB, 0xB0, 0xDE, 0x08, 0x00, 0x20,
            0x0C, 0x9A, 0x66
        };
        private static readonly ObexHeader TestHeader = GuidObexHeader.Create(ObexHeaderId.Target,
            19,
            Guid.Parse("bb582b40-420c-11db-b0de-0800200c9a66"));

        private readonly GuidObexHeaderConverter _converter = GuidObexHeaderConverter.Instance;

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
