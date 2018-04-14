using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Header;
using QIndependentStudios.Obex.Header;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Header
{
    [TestClass]
    public class UnicodeTextObexHeaderConverterTests
    {
        private static readonly byte[] TestHeaderData =
        {
            0x01, 0x00, 0x13, 0x00, 0x74, 0x00, 0x65, 0x00,
            0x6C, 0x00, 0x65, 0x00, 0x63, 0x00, 0x6F, 0x00,
            0x6D, 0x00, 0x00
        };
        private static readonly ObexHeader TestHeader = UnicodeTextObexHeader.Create(ObexHeaderId.Name,
            19,
            "telecom");
        private readonly UnicodeTextObexHeaderConverter _converter = UnicodeTextObexHeaderConverter.Instance;

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
