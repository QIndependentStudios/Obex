using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Request;
using QIndependentStudios.Obex.Header;
using QIndependentStudios.Obex.Path;
using System;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Request
{
    [TestClass]
    public class ObexSetPathRequestConverterTests
    {
        private static readonly byte[] TestSetPathRequestData =
        {
            0x85, 0x00, 0x1D, 0x02, 0x00, 0xCB, 0x00, 0x00,
            0x00, 0x01, 0x01, 0x00, 0x13, 0x00, 0x74, 0x00,
            0x65, 0x00, 0x6C, 0x00, 0x65, 0x00, 0x63, 0x00,
            0x6F, 0x00, 0x6D, 0x00, 0x00
        };
        private static readonly ObexRequestBase TestSetPathRequest = ObexSetPathRequest.Create(29,
            ObexSetPathFlag.DownToNameOrRoot,
            UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1),
            UnicodeTextObexHeader.Create(ObexHeaderId.Name, 19, "telecom"));
        private readonly ObexSetPathRequestConverter _converter = ObexSetPathRequestConverter.Instance;

        [TestMethod]
        public void FromBytes_GivenSetPathRequestData_ReturnsSetPatRequestObject()
        {
            var actual = _converter.FromBytes(TestSetPathRequestData);

            Assert.AreEqual(TestSetPathRequest, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FromBytes_GivenNonConnectData_ThrowsException()
        {
            var data = new byte[]
            {
                0x83, 0x00, 0x21, 0xCB, 0x00, 0x00, 0x00, 0x01,
                0x42, 0x00, 0x19, 0x78, 0x2D, 0x6F, 0x62, 0x65,
                0x78, 0x2F, 0x66, 0x6F, 0x6C, 0x64, 0x65, 0x72,
                0x2D, 0x6C, 0x69, 0x73, 0x74, 0x69, 0x6E, 0x67,
                0x00
            };
            _converter.FromBytes(data);
        }

        [TestMethod]
        public void ToBytes_GivenSetPatRequestObject_ReturnsSetPatRequestData()
        {
            var actual = _converter.ToBytes(TestSetPathRequest);

            Assert.IsTrue(TestSetPathRequestData.SequenceEqual(actual));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToBytes_GivenNonSetPathRequestObject_ThrowsException()
        {
            _converter.ToBytes(ObexRequest.Create(ObexOpCode.Get));
        }
    }
}
