using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Header;
using QIndependentStudios.Obex.Header;
using System;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Header
{
    [TestClass]
    public class ObexHeaderSerializerTests
    {
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Serialize_UnsupportedHeaderId_ThrowsException()
        {
            var header = TextObexHeader.Create(ObexHeaderId.Permissions, "Unknown");

            ObexHeaderSerializer.Serialize(header);
        }

        [TestMethod]
        public void Serialize_RawObexHeader_SerializesAsRawIgnoringHeaderId()
        {
            // EndOfBody would normally be serialized using the TextObexHeaderConverter that would
            // automatically append a char.MinValue causing a 5th byte of 0x00. Testing to ensure
            // RawObexHeaders ignore other converters and just serialize as is.
            var header = RawObexHeader.Create(ObexHeaderId.EndOfBody, 0x30);
            var expected = new byte[] { 0x49, 0x00, 0x04, 0x30 };

            var actual = ObexHeaderSerializer.Serialize(header);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void Serialize_SupportedHeaderId_ReturnsSerializedData()
        {
            var header = TextObexHeader.Create(ObexHeaderId.EndOfBody, "0");
            var expected = new byte[] { 0x49, 0x00, 0x05, 0x30, 0x00 };

            var actual = ObexHeaderSerializer.Serialize(header);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void Deserialize_SupportedHeaderId_ReturnsCorrectHeaderObject()
        {
            var data = new byte[] { 0x49, 0x00, 0x05, 0x30, 0x00 };
            var expected = TextObexHeader.Create(ObexHeaderId.EndOfBody, 5, "0");

            var actual = ObexHeaderSerializer.Deserialize(data);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Deserialize_UnsupportedHeaderId_ReturnsRawHeaderObject()
        {
            var data = new byte[] { 0xD6, 0x00, 0x04, 0x30 };
            var expected = RawObexHeader.Create(ObexHeaderId.Permissions, 0x30);

            var actual = ObexHeaderSerializer.Deserialize(data);

            Assert.AreEqual(expected, actual);
        }
    }
}
