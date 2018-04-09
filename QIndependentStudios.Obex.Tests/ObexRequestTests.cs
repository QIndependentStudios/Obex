using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Tests
{
    [TestClass]
    public class ObexRequestTests
    {
        [TestMethod]
        public void Create_GivenSupportedOpCode_RequestCreatesCorrectly()
        {
            foreach (var opCode in Enum.GetValues(typeof(ObexOpCode))
                .Cast<ObexOpCode>()
                .Where(x => x != ObexOpCode.Connect && x != ObexOpCode.SetPath))
            {
                var actual = ObexRequest.Create(opCode);
                Assert.AreEqual(opCode, actual.OpCode);
                Assert.IsNull(actual.ActualLength);
                Assert.IsTrue(!actual.Headers.Any());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GivenConnectOpCode_ThrowsException()
        {
            ObexRequest.Create(ObexOpCode.Connect);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GivenSetPathOpCode_ThrowsException()
        {
            ObexRequest.Create(ObexOpCode.SetPath);
        }

        [TestMethod]
        public void Create_OpCode_HeaderParams_RequestCreatesCorrectly()
        {
            var header = UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1);
            var actual = ObexRequest.Create(ObexOpCode.Get, header);

            Assert.AreEqual(ObexOpCode.Get, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_OpCode_HeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1)
            };
            var actual = ObexRequest.Create(ObexOpCode.Get, headers);

            Assert.AreEqual(ObexOpCode.Get, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.IsTrue(headers.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_OpCode_OpCodeLength_HeaderParams_RequestCreatesCorrectly()
        {
            var header = UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1);
            var actual = ObexRequest.Create(ObexOpCode.Get, 8, header);

            Assert.AreEqual(ObexOpCode.Get, actual.OpCode);
            Assert.AreEqual((ushort)8, actual.ActualLength);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_OpCode_OpCodeLength_HeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1)
            };
            var actual = ObexRequest.Create(ObexOpCode.Get, 8, headers);

            Assert.AreEqual(ObexOpCode.Get, actual.OpCode);
            Assert.AreEqual((ushort)8, actual.ActualLength);
            Assert.IsTrue(headers.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void GetHeadersForId_GivenHeaderId_ReturnsOnlyHeadersWithThatId()
        {
            var headers = new List<ObexHeader>
            {
                UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1),
                UInt32ObexHeader.Create(ObexHeaderId.Length, 1)
            };
            var request = ObexRequest.Create(ObexOpCode.Get, headers);

            var actual = request.GetHeadersForId(ObexHeaderId.ConnectionId);

            Assert.IsTrue(actual.All(x => x.Id == ObexHeaderId.ConnectionId));
        }
    }
}
