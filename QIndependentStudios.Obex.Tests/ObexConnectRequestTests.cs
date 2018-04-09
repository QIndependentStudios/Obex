using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Tests
{
    [TestClass]
    public class ObexConnectRequestTests
    {
        private const ushort TestMaxPacketSize = 1000;

        private readonly Guid _testGuid = Guid.Parse("bb582b40-420c-11db-b0de-0800200c9a66");

        [TestMethod]
        public void Create_MaxPacketSize_RequestCreatesCorrectly()
        {
            var actual = ObexConnectRequest.Create(TestMaxPacketSize);

            Assert.AreEqual(ObexOpCode.Connect, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(0x10, actual.ObexVersion);
            Assert.AreEqual(0x00, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(!actual.Headers.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_MaxPacketSizeTooLarge_ThrowsException()
        {
            var actual = ObexConnectRequest.Create(ObexConnectRequest.MaxPacketSizeUpperBound + 1); ;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_MaxPacketSizeTooSmall_ThrowsException()
        {
            var actual = ObexConnectRequest.Create(ObexConnectRequest.MaxPacketSizeLowerBound - 1); ;
        }

        [TestMethod]
        public void Create_MaxPacketSize_HeaderParams_RequestCreatesCorrectly()
        {
            var header = GuidObexHeader.Create(ObexHeaderId.Target, _testGuid);
            var actual = ObexConnectRequest.Create(TestMaxPacketSize,
                header);

            Assert.AreEqual(ObexOpCode.Connect, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(0x10, actual.ObexVersion);
            Assert.AreEqual(0x00, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_MaxPacketSize_HeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                GuidObexHeader.Create(ObexHeaderId.Target, _testGuid)
            };
            var actual = ObexConnectRequest.Create(TestMaxPacketSize,
                headers);

            Assert.AreEqual(ObexOpCode.Connect, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(0x10, actual.ObexVersion);
            Assert.AreEqual(0x00, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(headers.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_RequestLength_MaxPacketSize_RequestCreatesCorrectly()
        {
            var actual = ObexConnectRequest.Create(7, TestMaxPacketSize);

            Assert.AreEqual(ObexOpCode.Connect, actual.OpCode);
            Assert.AreEqual((ushort)7, actual.ActualLength);
            Assert.AreEqual(0x10, actual.ObexVersion);
            Assert.AreEqual(0x00, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(!actual.Headers.Any());
        }

        [TestMethod]
        public void Create_RequestLength_MaxPacketSize_HeaderParams_RequestCreatesCorrectly()
        {
            var header = GuidObexHeader.Create(ObexHeaderId.Target, _testGuid);
            var actual = ObexConnectRequest.Create(26,
                TestMaxPacketSize,
                header);

            Assert.AreEqual(ObexOpCode.Connect, actual.OpCode);
            Assert.AreEqual((ushort)26, actual.ActualLength);
            Assert.AreEqual(0x10, actual.ObexVersion);
            Assert.AreEqual(0x00, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_RequestLength_MaxPacketSize_HeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                GuidObexHeader.Create(ObexHeaderId.Target, _testGuid)
            };
            var actual = ObexConnectRequest.Create(26,
                TestMaxPacketSize,
                headers);

            Assert.AreEqual(ObexOpCode.Connect, actual.OpCode);
            Assert.AreEqual((ushort)26, actual.ActualLength);
            Assert.AreEqual(0x10, actual.ObexVersion);
            Assert.AreEqual(0x00, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(headers.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_Version_Flags_MaxPacketSize_RequestCreatesCorrectly()
        {
            var actual = ObexConnectRequest.Create(0x11, 0x11, TestMaxPacketSize);

            Assert.AreEqual(ObexOpCode.Connect, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(0x11, actual.ObexVersion);
            Assert.AreEqual(0x11, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(!actual.Headers.Any());
        }

        [TestMethod]
        public void Create_Version_Flags_MaxPacketSize_HeaderParams_RequestCreatesCorrectly()
        {
            var header = GuidObexHeader.Create(ObexHeaderId.Target, _testGuid);
            var actual = ObexConnectRequest.Create(0x11,
                0x11,
                TestMaxPacketSize,
                header);

            Assert.AreEqual(ObexOpCode.Connect, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(0x11, actual.ObexVersion);
            Assert.AreEqual(0x11, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_Version_Flags_MaxPacketSize_HeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                GuidObexHeader.Create(ObexHeaderId.Target, _testGuid)
            };
            var actual = ObexConnectRequest.Create(0x11,
                0x11,
                TestMaxPacketSize,
                headers);

            Assert.AreEqual(ObexOpCode.Connect, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(0x11, actual.ObexVersion);
            Assert.AreEqual(0x11, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(headers.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_RequestLength_Version_Flags_MaxPacketSize_RequestCreatesCorrectly()
        {
            var actual = ObexConnectRequest.Create(7, 0x11, 0x11, TestMaxPacketSize);

            Assert.AreEqual(ObexOpCode.Connect, actual.OpCode);
            Assert.AreEqual((ushort)7, actual.ActualLength);
            Assert.AreEqual(0x11, actual.ObexVersion);
            Assert.AreEqual(0x11, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(!actual.Headers.Any());
        }

        [TestMethod]
        public void Create_RequestLength_Version_Flags_MaxPacketSize_HeaderParams_RequestCreatesCorrectly()
        {
            var header = GuidObexHeader.Create(ObexHeaderId.Target, _testGuid);
            var actual = ObexConnectRequest.Create(26,
                0x11,
                0x11,
                TestMaxPacketSize,
                header);

            Assert.AreEqual(ObexOpCode.Connect, actual.OpCode);
            Assert.AreEqual((ushort)26, actual.ActualLength);
            Assert.AreEqual(0x11, actual.ObexVersion);
            Assert.AreEqual(0x11, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_RequestLength_Version_Flags_MaxPacketSize_HeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                GuidObexHeader.Create(ObexHeaderId.Target, _testGuid)
            };
            var actual = ObexConnectRequest.Create(26,
                0x11,
                0x11,
                TestMaxPacketSize,
                headers);

            Assert.AreEqual(ObexOpCode.Connect, actual.OpCode);
            Assert.AreEqual((ushort)26, actual.ActualLength);
            Assert.AreEqual(0x11, actual.ObexVersion);
            Assert.AreEqual(0x11, actual.Flags);
            Assert.AreEqual(TestMaxPacketSize, actual.MaxPacketSize);
            Assert.IsTrue(headers.SequenceEqual(actual.Headers));
        }
    }
}
