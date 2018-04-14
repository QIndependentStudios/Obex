using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Request;
using QIndependentStudios.Obex.Header;
using QIndependentStudios.Obex.Path;
using System;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Request
{
    [TestClass]
    public class ObexRequestConverterTests
    {
        private static readonly byte[] TestRequestData =
        {
            0x83, 0x00, 0x21, 0xCB, 0x00, 0x00, 0x00, 0x01,
            0x42, 0x00, 0x19, 0x78, 0x2D, 0x6F, 0x62, 0x65,
            0x78, 0x2F, 0x66, 0x6F, 0x6C, 0x64, 0x65, 0x72,
            0x2D, 0x6C, 0x69, 0x73, 0x74, 0x69, 0x6E, 0x67,
            0x00
        };
        private static readonly ObexRequest TestRequest = ObexRequest.Create(ObexOpCode.Get,
            33,
            UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1),
            ByteSequenceObexHeader.Create(ObexHeaderId.Type, "x-obex/folder-listing"));
        private readonly ObexRequestConverter _converter = ObexRequestConverter.Instance;

        [TestMethod]
        public void FromBytes_GivenRequestData_ReturnsRequestObject()
        {
            var actual = _converter.FromBytes(TestRequestData);

            Assert.AreEqual(TestRequest, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FromBytes_GivenConnectData_ThrowsException()
        {
            var data = new byte[]
            {
                0x80, 0x00, 0x1A, 0x10, 0x00, 0x14, 0x00, 0x46,
                0x00, 0x13, 0xBB, 0x58, 0x2B, 0x40, 0x42, 0x0C,
                0x11, 0xDB, 0xB0, 0xDE, 0x08, 0x00, 0x20, 0x0C,
                0x9A, 0x66
            };
            _converter.FromBytes(data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FromBytes_GivenSetPathData_ThrowsException()
        {
            var data = new byte[]
            {
                0x85, 0x00, 0x1D, 0x02, 0x00, 0xCB, 0x00, 0x00,
                0x00, 0x01, 0x01, 0x00, 0x13, 0x00, 0x74, 0x00,
                0x65, 0x00, 0x6C, 0x00, 0x65, 0x00, 0x63, 0x00,
                0x6F, 0x00, 0x6D, 0x00, 0x00
            };
            _converter.FromBytes(data);
        }

        [TestMethod]
        public void ToBytes_GivenRequestObject_ReturnsRequestData()
        {
            var actual = _converter.ToBytes(TestRequest);

            Assert.IsTrue(TestRequestData.SequenceEqual(actual));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToBytes_GivenConnectRequestObject_ThrowsException()
        {
            _converter.ToBytes(ObexConnectRequest.Create(1000));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToBytes_GivenSetPathRequestObject_ThrowsExceptio()
        {
            _converter.ToBytes(ObexSetPathRequest.Create(ObexSetPathFlag.DownToNameOrRoot));
        }
    }
}
