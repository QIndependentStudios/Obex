using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var header = new UnsupportedObexHeader();

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
        public void Deserialize_UnsupportedUnicodeHeaderId_ReturnsUnicodeTextHeaderObject()
        {
            var data = new byte[] { 0x3F, 0x00, 0x07, 0x00, 0x30, 0x00, 0x00 };
            var expected = UnicodeTextObexHeader.Create((ObexHeaderId)0x3F, 7, "0");

            var actual = ObexHeaderSerializer.Deserialize(data);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Deserialize_UnsupportedByteSequenceHeaderId_ReturnsRawHeaderObject()
        {
            var data = new byte[] { 0x7F, 0x00, 0x04, 0x30 };
            var expected = RawObexHeader.Create((ObexHeaderId)0x7F, 0x30);

            var actual = ObexHeaderSerializer.Deserialize(data);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Deserialize_UnsupportedSingleByteHeaderId_ReturnsByteHeaderObject()
        {
            var data = new byte[] { 0xBF, 0x30 };
            var expected = ByteObexHeader.Create((ObexHeaderId)0xBF, 0x30);

            var actual = ObexHeaderSerializer.Deserialize(data);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Deserialize_UnsupportedFourBytesHeaderId_ReturnsUInt32HeaderObject()
        {
            var data = new byte[] { 0xFF, 0x00, 0x00, 0x00, 0x01 };
            var expected = UInt32ObexHeader.Create((ObexHeaderId)0xFF, 1);

            var actual = ObexHeaderSerializer.Deserialize(data);

            Assert.AreEqual(expected, actual);
        }
    }

    public class UnsupportedObexHeader : ObexHeader
    {

    }
}
