using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Response;
using QIndependentStudios.Obex.Header;
using System;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Response
{
    [TestClass]
    public class ObexConnectResponseConverterTests
    {
        private static readonly byte[] TestConnectResponseData =
        {
            0xA0, 0x00, 0x1F, 0x10, 0x00, 0x14, 0x00, 0xCB,
            0x00, 0x00, 0x00, 0x01, 0x4A, 0x00, 0x13, 0xBB,
            0x58, 0x2B, 0x40, 0x42, 0x0C, 0x11, 0xDB, 0xB0,
            0xDE, 0x08, 0x00, 0x20, 0x0C, 0x9A, 0x66
        };
        private static readonly ObexConnectResponse TestConnectResponse = ObexConnectResponse.Create(ObexResponseCode.Ok,
            31,
            0x10,
            0x00,
            5120,
            UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1),
            GuidObexHeader.Create(ObexHeaderId.Who, 19, Guid.Parse("bb582b40-420c-11db-b0de-0800200c9a66")));
        private readonly ObexConnectResponseConverter _converter = ObexConnectResponseConverter.Instance;

        [TestMethod]
        public void FromBytes_GivenConnectResponseData_ReturnsConnectResponseObject()
        {
            var actual = _converter.FromBytes(TestConnectResponseData);

            Assert.AreEqual(TestConnectResponse, actual);
        }

        [TestMethod]
        public void ToBytes_GivenConnectResponseObject_ReturnsConnectResponseData()
        {
            var actual = _converter.ToBytes(TestConnectResponse);

            Assert.IsTrue(TestConnectResponseData.SequenceEqual(actual));
        }
    }
}
