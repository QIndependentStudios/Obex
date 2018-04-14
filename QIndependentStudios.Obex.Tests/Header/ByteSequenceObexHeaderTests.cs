using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Header
{
    [TestClass]
    public class ByteSequenceObexHeaderTests
    {
        private static readonly List<byte> TestValue = new List<byte> { 0x01, 0x02, 0x03 };

        [TestMethod]
        public void Create_IdWithValueParams_HeaderCreatesSuccessfully()
        {
            var actual = ByteSequenceObexHeader.Create(ObexHeaderId.Type,
                TestValue[0],
                TestValue[1],
                TestValue[2]);

            Assert.AreEqual(ObexHeaderId.Type, actual.Id);
            Assert.AreEqual((ushort)6, actual.ActualLength);
            Assert.IsTrue(TestValue.SequenceEqual(actual.Value));
        }

        [TestMethod]
        public void Create_IdWithValueEnumerable_HeaderCreatesSuccessfully()
        {
            var actual = ByteSequenceObexHeader.Create(ObexHeaderId.Type, TestValue);

            Assert.AreEqual(ObexHeaderId.Type, actual.Id);
            Assert.AreEqual((ushort)6, actual.ActualLength);
            Assert.IsTrue(TestValue.SequenceEqual(actual.Value));
        }

        [TestMethod]
        public void Create_IdWithStringValue_HeaderCreatesSuccessfully()
        {
            var expectedValue = new byte[]
            {
                0x78, 0x2D, 0x6F, 0x62, 0x65, 0x78, 0x2F, 0x66,
                0x6F, 0x6C, 0x64, 0x65, 0x72, 0x2D, 0x6C, 0x69,
                0x73, 0x74, 0x69, 0x6E, 0x67, 0x00
            };
            var actual = ByteSequenceObexHeader.Create(ObexHeaderId.Type, "x-obex/folder-listing");

            Assert.AreEqual(ObexHeaderId.Type, actual.Id);
            Assert.AreEqual((ushort)25, actual.ActualLength);
            Assert.IsTrue(expectedValue.SequenceEqual(actual.Value));
        }

        [TestMethod]
        public void Create_IdWithNullTermStringValue_NoNullTerminatorAppendedInValue()
        {
            var expectedValue = new byte[]
            {
                0x78, 0x2D, 0x6F, 0x62, 0x65, 0x78, 0x2F, 0x66,
                0x6F, 0x6C, 0x64, 0x65, 0x72, 0x2D, 0x6C, 0x69,
                0x73, 0x74, 0x69, 0x6E, 0x67, 0x00
            };
            var actual = ByteSequenceObexHeader.Create(ObexHeaderId.Type, "x-obex/folder-listing\0");

            Assert.AreEqual(ObexHeaderId.Type, actual.Id);
            Assert.AreEqual((ushort)25, actual.ActualLength);
            Assert.IsTrue(expectedValue.SequenceEqual(actual.Value));
        }
    }
}
