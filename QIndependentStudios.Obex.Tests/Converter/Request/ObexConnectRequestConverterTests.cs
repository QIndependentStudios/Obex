using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Request;
using QIndependentStudios.Obex.Header;
using System;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Request
{
    [TestClass]
    public class ObexConnectRequestConverterTests
    {
        private static readonly byte[] _testConnectRequestData = new byte[]
        {
            0x80, 0x00, 0x1A, 0x10, 0x00, 0x14, 0x00, 0x46,
            0x00, 0x13, 0xBB, 0x58, 0x2B, 0x40, 0x42, 0x0C,
            0x11, 0xDB, 0xB0, 0xDE, 0x08, 0x00, 0x20, 0x0C,
            0x9A, 0x66
        };
        private static readonly ObexRequestBase _testConnectRequest = ObexConnectRequest.Create(26,
            5120,
            GuidObexHeader.Create(ObexHeaderId.Target, 19, Guid.Parse("bb582b40-420c-11db-b0de-0800200c9a66")));
        private readonly ObexConnectRequestConverter _converter = ObexConnectRequestConverter.Instance;

        [TestMethod]
        public void FromBytes_GivenConnectRequestData_ReturnsConnectRequestObject()
        {
            var actual = _converter.FromBytes(_testConnectRequestData);

            Assert.AreEqual(_testConnectRequest, actual);
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
        public void ToBytes_GivenConnectRequestObject_ReturnsConnectRequestData()
        {
            var actual = _converter.ToBytes(_testConnectRequest);

            Assert.IsTrue(_testConnectRequestData.SequenceEqual(actual));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToBytes_GivenNonConnectRequestObject_ThrowsException()
        {
            _converter.ToBytes(ObexRequest.Create(ObexOpCode.Get));
        }
    }
}
