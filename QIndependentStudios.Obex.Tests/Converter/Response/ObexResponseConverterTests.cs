using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Converter.Response;
using QIndependentStudios.Obex.Header;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Response
{
    [TestClass]
    public class ObexResponseConverterTests
    {
        private static readonly byte[] TestResponseData =
        {
            0xA0, 0x00, 0x08, 0xCB, 0x00, 0x00, 0x00, 0x01
        };
        private static readonly ObexResponse TestResponse = ObexResponse.Create(ObexResponseCode.Ok,
            8,
            UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1));
        private readonly ObexResponseConverter _converter = ObexResponseConverter.Instance;

        [TestMethod]
        public void FromBytes_GivenResponseData_ReturnsResponseObject()
        {
            var actual = _converter.FromBytes(TestResponseData);

            Assert.AreEqual(TestResponse, actual);
        }

        [TestMethod]
        public void ToBytes_GivenResponseObject_ReturnsResponseData()
        {
            var actual = _converter.ToBytes(TestResponse);

            Assert.IsTrue(TestResponseData.SequenceEqual(actual));
        }
    }
}
