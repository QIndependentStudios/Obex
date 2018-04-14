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
        private static readonly byte[] TestHeaderData = { 0x97, 0x01 };
        private static readonly ObexHeader TestHeader =
            ByteObexHeader.Create(ObexHeaderId.SingleResponseMode, TestHeaderValue);

        private readonly ByteObexHeaderConverter _converter = ByteObexHeaderConverter.Instance;

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
