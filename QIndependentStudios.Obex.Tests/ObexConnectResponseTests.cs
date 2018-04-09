using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Tests
{
    [TestClass]
    public class ObexConnectResponseTests
    {
        private const ushort TestMaxPacketSize = 1000;

        [TestMethod]
        public void Create_WithoutResponseLengthOrHeaders_RequestCreatesCorrectly()
        {
            var actual = ObexConnectResponse.Create(ObexResponseCode.Ok,
                0x11,
                0x11,
                TestMaxPacketSize);

            Assert.AreEqual(ObexResponseCode.Ok, actual.ResponseCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(0x11, actual.ObexVersion);
            Assert.AreEqual(0x11, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(!actual.Headers.Any());
        }

        [TestMethod]
        public void Create_WithoutResponseLengthWithHeaderParams_RequestCreatesCorrectly()
        {
            var header = UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1);
            var actual = ObexConnectResponse.Create(ObexResponseCode.Ok,
                0x11,
                0x11,
                TestMaxPacketSize,
                header);

            Assert.AreEqual(ObexResponseCode.Ok, actual.ResponseCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(0x11, actual.ObexVersion);
            Assert.AreEqual(0x11, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_WithoutResponseLengthWithHeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1)
            };
            var actual = ObexConnectResponse.Create(ObexResponseCode.Ok,
                0x11,
                0x11,
                TestMaxPacketSize,
                headers);

            Assert.AreEqual(ObexResponseCode.Ok, actual.ResponseCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(0x11, actual.ObexVersion);
            Assert.AreEqual(0x11, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(headers.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_WithoutHeaders_RequestCreatesCorrectly()
        {
            var actual = ObexConnectResponse.Create(ObexResponseCode.Ok,
                7,
                0x11,
                0x11,
                TestMaxPacketSize);

            Assert.AreEqual(ObexResponseCode.Ok, actual.ResponseCode);
            Assert.AreEqual((ushort)7, actual.ActualLength);
            Assert.AreEqual(0x11, actual.ObexVersion);
            Assert.AreEqual(0x11, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(!actual.Headers.Any());
        }

        [TestMethod]
        public void Create_WithHeaderParams_RequestCreatesCorrectly()
        {
            var header = UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1);
            var actual = ObexConnectResponse.Create(ObexResponseCode.Ok,
                13,
                0x11,
                0x11,
                TestMaxPacketSize,
                header);

            Assert.AreEqual(ObexResponseCode.Ok, actual.ResponseCode);
            Assert.AreEqual((ushort)13, actual.ActualLength);
            Assert.AreEqual(0x11, actual.ObexVersion);
            Assert.AreEqual(0x11, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_WithHeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1)
            };
            var actual = ObexConnectResponse.Create(ObexResponseCode.Ok,
                13,
                0x11,
                0x11,
                TestMaxPacketSize,
                headers);

            Assert.AreEqual(ObexResponseCode.Ok, actual.ResponseCode);
            Assert.AreEqual((ushort)13, actual.ActualLength);
            Assert.AreEqual(0x11, actual.ObexVersion);
            Assert.AreEqual(0x11, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(headers.SequenceEqual(actual.Headers));
        }
    }
}
