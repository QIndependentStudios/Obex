using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Header;
using QIndependentStudios.Obex.Header;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Header
{
    [TestClass]
    public class UInt32ObexHeaderConverterTests
    {
        private static readonly byte[] TestHeaderData = { 0xCB, 0x00, 0x00, 0x09, 0x13 };
        private static readonly ObexHeader TestHeader =
            UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 2323);

        private readonly UInt32ObexHeaderConverter _converter = UInt32ObexHeaderConverter.Instance;

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
