using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text;

namespace QIndependentStudios.Obex.Tests
{
    [TestClass]
    public class ObexBitConverterTests
    {
        public const string _testString = "A1😊";

        public static readonly Guid _testGuid = Guid.Parse("bb582b40-420c-11db-b0de-0800200c9a66");


        public static readonly byte[] _testGuidBytes = new byte[]
        {
            0xBB, 0x58, 0x2B, 0x40, 0x42, 0x0C, 0x11, 0xDB,
            0xB0, 0xDE, 0x08, 0x00, 0x20, 0x0C, 0x9A, 0x66
        };
        public static readonly byte[] _testStringBytes = new byte[] { 0x41, 0x31, 0xF0, 0x9F, 0x98, 0x8A };
        public static readonly byte[] _testUnicodeStringBytes = new byte[] { 0x00, 0x41, 0x00, 0x31, 0xD8, 0x3D, 0xDE, 0x0A };

        [TestMethod]
        public void GetBytes_Guid_ReturnsGuidBytes()
        {
            var actual = ObexBitConverter.GetBytes(_testGuid);
            Assert.IsTrue(_testGuidBytes.SequenceEqual(actual));
        }

        [TestMethod]
        public void GetBytes_StringWithNullEncoding_ReturnsUtf8Bytes()
        {
            var actual = ObexBitConverter.GetBytes(_testString);
            Assert.IsTrue(_testStringBytes.SequenceEqual(actual));
        }

        [TestMethod]
        public void GetBytes_StringWithAsciiEncoding_ReturnAsciiBytes()
        {
            var expected = new byte[] { 0x41, 0x31, 0x3F, 0x3F };
            var actual = ObexBitConverter.GetBytes(_testString, Encoding.ASCII);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void GetBytes_StringWithUnicodeEncoding_ReturnsBigEndianUnicodeBytes()
        {
            var actual = ObexBitConverter.GetBytes(_testString, Encoding.Unicode);
            Assert.IsTrue(_testUnicodeStringBytes.SequenceEqual(actual));
        }

        [TestMethod]
        public void GetBytes_StringWithBigEndianUnicodeEncoding_ReturnsBigEndianUnicodeBytes()
        {
            var actual = ObexBitConverter.GetBytes(_testString, Encoding.BigEndianUnicode);
            Assert.IsTrue(_testUnicodeStringBytes.SequenceEqual(actual));
        }

        [TestMethod]
        public void ToGuid_NetworkGuidBytes_ReturnsGuid()
        {
            var actual = ObexBitConverter.ToGuid(_testGuidBytes);
            Assert.AreEqual(_testGuid, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToGuid_GuidBytesTooLong_ThrowsException()
        {
            ObexBitConverter.ToGuid(_testGuidBytes.Concat(new byte[] { 0x00 }).ToArray());
        }

        [TestMethod]
        public void ToString_NoEncoding_ReturnsUtf8String()
        {
            var actual = ObexBitConverter.ToString(_testStringBytes);
            Assert.AreEqual(_testString, actual);
        }

        [TestMethod]
        public void ToString_Utf8Encoding_ReturnsUtf8String()
        {
            var actual = ObexBitConverter.ToString(_testStringBytes, Encoding.UTF8);
            Assert.AreEqual(_testString, actual);
        }

        [TestMethod]
        public void ToString_AsciiEncoding_ReturnsAsciiString()
        {
            var expected = "A1????";
            var actual = ObexBitConverter.ToString(_testStringBytes, Encoding.ASCII);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToString_UnicodeEncoding_ThrowsException()
        {
            ObexBitConverter.ToString(_testUnicodeStringBytes, Encoding.Unicode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToString_BigEndianUnicodeEncoding_ThrowsException()
        {
            ObexBitConverter.ToString(_testUnicodeStringBytes, Encoding.BigEndianUnicode);
        }

        [TestMethod]
        public void ToUnicodeString_UnicodeTextBytes_ReturnsUtf8String()
        {
            var actual = ObexBitConverter.ToUnicodeString(_testUnicodeStringBytes);
            Assert.AreEqual(_testString, actual);
        }
    }
}
