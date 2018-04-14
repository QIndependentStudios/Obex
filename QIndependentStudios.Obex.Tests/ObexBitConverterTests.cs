using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text;

namespace QIndependentStudios.Obex.Tests
{
    [TestClass]
    public class ObexBitConverterTests
    {
        public const string TestString = "A1😊";

        public static readonly Guid TestGuid = Guid.Parse("bb582b40-420c-11db-b0de-0800200c9a66");


        public static readonly byte[] TestGuidBytes = {
            0xBB, 0x58, 0x2B, 0x40, 0x42, 0x0C, 0x11, 0xDB,
            0xB0, 0xDE, 0x08, 0x00, 0x20, 0x0C, 0x9A, 0x66
        };
        public static readonly byte[] TestStringBytes = { 0x41, 0x31, 0xF0, 0x9F, 0x98, 0x8A };
        public static readonly byte[] TestUnicodeStringBytes = { 0x00, 0x41, 0x00, 0x31, 0xD8, 0x3D, 0xDE, 0x0A };

        [TestMethod]
        public void GetBytes_Guid_ReturnsGuidBytes()
        {
            var actual = ObexBitConverter.GetBytes(TestGuid);
            Assert.IsTrue(TestGuidBytes.SequenceEqual(actual));
        }

        [TestMethod]
        public void GetBytes_StringWithNullEncoding_ReturnsAsciiBytes()
        {
            var expected = new byte[] { 0x41, 0x31, 0x3F, 0x3F };
            var actual = ObexBitConverter.GetBytes(TestString);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void GetBytes_StringWithUtf8Encoding_ReturnUtf8Bytes()
        {
            var actual = ObexBitConverter.GetBytes(TestString, Encoding.UTF8);
            Assert.IsTrue(TestStringBytes.SequenceEqual(actual));
        }

        [TestMethod]
        public void GetBytes_StringWithUnicodeEncoding_ReturnsBigEndianUnicodeBytes()
        {
            var actual = ObexBitConverter.GetBytes(TestString, Encoding.Unicode);
            Assert.IsTrue(TestUnicodeStringBytes.SequenceEqual(actual));
        }

        [TestMethod]
        public void GetBytes_StringWithBigEndianUnicodeEncoding_ReturnsBigEndianUnicodeBytes()
        {
            var actual = ObexBitConverter.GetBytes(TestString, Encoding.BigEndianUnicode);
            Assert.IsTrue(TestUnicodeStringBytes.SequenceEqual(actual));
        }

        [TestMethod]
        public void ToGuid_NetworkGuidBytes_ReturnsGuid()
        {
            var actual = ObexBitConverter.ToGuid(TestGuidBytes);
            Assert.AreEqual(TestGuid, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToGuid_GuidBytesTooLong_ThrowsException()
        {
            ObexBitConverter.ToGuid(TestGuidBytes.Concat(new byte[] { 0x00 }).ToArray());
        }

        [TestMethod]
        public void ToString_NoEncoding_ReturnsUtf8String()
        {
            var actual = ObexBitConverter.ToString(TestStringBytes);
            Assert.AreEqual(TestString, actual);
        }

        [TestMethod]
        public void ToString_Utf8Encoding_ReturnsUtf8String()
        {
            var actual = ObexBitConverter.ToString(TestStringBytes, Encoding.UTF8);
            Assert.AreEqual(TestString, actual);
        }

        [TestMethod]
        public void ToString_AsciiEncoding_ReturnsAsciiString()
        {
            var expected = "A1????";
            var actual = ObexBitConverter.ToString(TestStringBytes, Encoding.ASCII);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToString_UnicodeEncoding_ThrowsException()
        {
            ObexBitConverter.ToString(TestUnicodeStringBytes, Encoding.Unicode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToString_BigEndianUnicodeEncoding_ThrowsException()
        {
            ObexBitConverter.ToString(TestUnicodeStringBytes, Encoding.BigEndianUnicode);
        }

        [TestMethod]
        public void ToUnicodeString_UnicodeTextBytes_ReturnsUtf8String()
        {
            var actual = ObexBitConverter.ToUnicodeString(TestUnicodeStringBytes);
            Assert.AreEqual(TestString, actual);
        }
    }
}
