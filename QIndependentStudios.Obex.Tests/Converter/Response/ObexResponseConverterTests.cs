using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Response;
using QIndependentStudios.Obex.Header;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Response
{
    [TestClass]
    public class ObexResponseConverterTests
    {
        private static readonly byte[] _testResponseData = new byte[]
        {
            0xA0, 0x00, 0x08, 0xCB, 0x00, 0x00, 0x00, 0x01
        };
        private static readonly ObexResponse _testResponse = ObexResponse.Create(ObexResponseCode.Ok,
            8,
            UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1));
        private readonly ObexResponseConverter _converter = ObexResponseConverter.Instance;

        [TestMethod]
        public void FromBytes_GivenResponseData_ReturnsResponseObject()
        {
            var actual = _converter.FromBytes(_testResponseData);

            Assert.AreEqual(_testResponse, actual);
        }

        [TestMethod]
        public void ToBytes_GivenResponseObject_ReturnsResponseData()
        {
            var actual = _converter.ToBytes(_testResponse);

            Assert.IsTrue(_testResponseData.SequenceEqual(actual));
        }
    }
}
