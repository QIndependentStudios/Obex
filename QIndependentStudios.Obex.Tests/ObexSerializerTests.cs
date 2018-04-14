using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;
using QIndependentStudios.Obex.Path;
using System;
using System.Linq;

namespace QIndependentStudios.Obex.Tests
{
    [TestClass]
    public class ObexSerializerTests
    {
        private static readonly byte[] TestRequestData = {
            0x83, 0x00, 0x21, 0xCB, 0x00, 0x00, 0x00, 0x01,
            0x42, 0x00, 0x19, 0x78, 0x2D, 0x6F, 0x62, 0x65,
            0x78, 0x2F, 0x66, 0x6F, 0x6C, 0x64, 0x65, 0x72,
            0x2D, 0x6C, 0x69, 0x73, 0x74, 0x69, 0x6E, 0x67,
            0x00
        };
        private static readonly byte[] TestConnectRequestData = {
            0x80, 0x00, 0x1A, 0x10, 0x00, 0x14, 0x00, 0x46,
            0x00, 0x13, 0xBB, 0x58, 0x2B, 0x40, 0x42, 0x0C,
            0x11, 0xDB, 0xB0, 0xDE, 0x08, 0x00, 0x20, 0x0C,
            0x9A, 0x66
        };
        private static readonly byte[] TestSetPathRequestData = {
            0x85, 0x00, 0x1D, 0x02, 0x00, 0xCB, 0x00, 0x00,
            0x00, 0x01, 0x01, 0x00, 0x13, 0x00, 0x74, 0x00,
            0x65, 0x00, 0x6C, 0x00, 0x65, 0x00, 0x63, 0x00,
            0x6F, 0x00, 0x6D, 0x00, 0x00
        };
        private static readonly byte[] TestResponseData = {
            0xA0, 0x00, 0x08, 0xCB, 0x00, 0x00, 0x00, 0x01
        };
        private static readonly byte[] TestConnectResponseData = {
            0xA0, 0x00, 0x1F, 0x10, 0x00, 0x14, 0x00, 0xCB,
            0x00, 0x00, 0x00, 0x01, 0x4A, 0x00, 0x13, 0xBB,
            0x58, 0x2B, 0x40, 0x42, 0x0C, 0x11, 0xDB, 0xB0,
            0xDE, 0x08, 0x00, 0x20, 0x0C, 0x9A, 0x66
        };

        private static readonly ObexRequestBase TestRequest = ObexRequest.Create(ObexOpCode.Get,
            33,
            UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1),
            TextObexHeader.Create(ObexHeaderId.Type, 25, "x-obex/folder-listing"));
        private static readonly ObexRequestBase TestConnectRequest = ObexConnectRequest.Create(26,
            5120,
            GuidObexHeader.Create(ObexHeaderId.Target, 19, Guid.Parse("bb582b40-420c-11db-b0de-0800200c9a66")));
        private static readonly ObexRequestBase TestSetPathRequest = ObexSetPathRequest.Create(29,
            ObexSetPathFlag.DownToNameOrRoot,
            UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1),
            UnicodeTextObexHeader.Create(ObexHeaderId.Name, 19, "telecom"));
        private static readonly ObexResponseBase TestResponse = ObexResponse.Create(ObexResponseCode.Ok,
            8,
            UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1));
        private static readonly ObexResponseBase TestConnectResponse = ObexConnectResponse.Create(ObexResponseCode.Ok,
            31,
            0x10,
            0x00,
            5120,
            UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1),
            GuidObexHeader.Create(ObexHeaderId.Who, 19, Guid.Parse("bb582b40-420c-11db-b0de-0800200c9a66")));

        [TestMethod]
        public void DeserializeRequest_GivenRequestData_ReturnsRequestObject()
        {
            var actual = ObexSerializer.DeserializeRequest(TestRequestData);

            Assert.AreEqual(TestRequest, actual);
        }

        [TestMethod]
        public void DeserializeRequest_GivenConnectRequestData_ReturnsConnectRequestObject()
        {
            var actual = ObexSerializer.DeserializeRequest(TestConnectRequestData);

            Assert.AreEqual(TestConnectRequest, actual);
        }

        [TestMethod]
        public void DeserializeRequest_GivenSetPathRequestData_ReturnsSetPathRequestObject()
        {
            var actual = ObexSerializer.DeserializeRequest(TestSetPathRequestData);

            Assert.AreEqual(TestSetPathRequest, actual);
        }

        [TestMethod]
        public void SerializeRequest_GivenRequestObject_ReturnsRequestData()
        {
            var actual = ObexSerializer.SerializeRequest(TestRequest);

            Assert.IsTrue(TestRequestData.SequenceEqual(actual));
        }

        [TestMethod]
        public void SerializeRequest_GivenConnectRequestObject_ReturnsConnectRequestData()
        {
            var actual = ObexSerializer.SerializeRequest(TestConnectRequest);

            Assert.IsTrue(TestConnectRequestData.SequenceEqual(actual));
        }

        [TestMethod]
        public void SerializeRequest_GivenSetPathRequestObject_ReturnsSetPathRequestData()
        {
            var actual = ObexSerializer.SerializeRequest(TestSetPathRequest);

            Assert.IsTrue(TestSetPathRequestData.SequenceEqual(actual));
        }

        [TestMethod]
        public void DeserializeResponse_GivenResponseData_returnsResponseObject()
        {
            var actual = ObexSerializer.DeserializeResponse(TestResponseData, false);

            Assert.AreEqual(TestResponse, actual);
        }

        [TestMethod]
        public void DeserializeResponse_GivenConnectResponseData_returnsConnectResponseObject()
        {
            var actual = ObexSerializer.DeserializeResponse(TestConnectResponseData, true);

            Assert.AreEqual(TestConnectResponse, actual);
        }

        [TestMethod]
        public void SerializeResponse_GivenResponseObject_ReturnsResponseData()
        {
            var actual = ObexSerializer.SerializeResponse(TestResponse);

            Assert.IsTrue(TestResponseData.SequenceEqual(actual));
        }

        [TestMethod]
        public void SerializeResponse_GivenConnectResponseObject_ReturnsConnectResponseData()
        {
            var actual = ObexSerializer.SerializeResponse(TestConnectResponse);

            Assert.IsTrue(TestConnectResponseData.SequenceEqual(actual));
        }
    }
}
