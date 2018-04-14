using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Header;
using QIndependentStudios.Obex.Header;
using System;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Header
{
    [TestClass]
    public class DateTimeObexHeaderConverterTests
    {
        private static readonly byte[] TimeTestHeaderData =
        {
            0x44, 0x00, 0x12, 0x32, 0x30, 0x31, 0x38, 0x30,
            0x34, 0x31, 0x34, 0x54, 0x30, 0x30, 0x30, 0x30,
            0x30, 0x30
        };
        private static readonly byte[] Time4BytesTestHeaderData =
        {
            0xC4, 0x5A, 0xD1, 0x44, 0x80
        };

        private static readonly DateTime TestValue = new DateTime(2018, 4, 14);
        private static readonly ObexHeader TimeTestHeader = DateTimeObexHeader.Create(ObexHeaderId.Time,
            18,
            TestValue);
        private static readonly ObexHeader Time4BytesTestHeader = DateTimeObexHeader.Create(ObexHeaderId.Time4Byte,
            5,
            TestValue);

        private readonly DateTimeObexHeaderConverter _converter = DateTimeObexHeaderConverter.Instance;

        [TestMethod]
        public void FromBytes_GivenTimeHeaderByteData_ReturnsHeaderObject()
        {
            var actual = _converter.FromBytes(TimeTestHeaderData);

            Assert.AreEqual(TimeTestHeader, actual);
        }

        [TestMethod]
        public void FromBytes_GivenTime4BytesHeaderByteData_ReturnsHeaderObject()
        {
            var actual = _converter.FromBytes(Time4BytesTestHeaderData);

            Assert.AreEqual(Time4BytesTestHeader, actual);
        }

        [TestMethod]
        public void ToBytes_GivenTimeHeaderObject_ReturnsHeaderByteData()
        {
            var actual = _converter.ToBytes(TimeTestHeader);

            Assert.IsTrue(TimeTestHeaderData.SequenceEqual(actual));
        }

        [TestMethod]
        public void ToBytes_GivenTime4BytesHeaderObject_ReturnsHeaderByteData()
        {
            var actual = _converter.ToBytes(Time4BytesTestHeader);

            Assert.IsTrue(Time4BytesTestHeaderData.SequenceEqual(actual));
        }
    }
}
