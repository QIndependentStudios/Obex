using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Converter.Response
{
    [TestClass]
    public class ObexResponseTests
    {
        [TestMethod]
        public void Create_ResponseCode_RequestCreatesCorrectly()
        {
            var actual = ObexResponse.Create(ObexResponseCode.Ok);

            Assert.AreEqual(ObexResponseCode.Ok, actual.ResponseCode);
            Assert.IsNull(actual.ActualLength);
            Assert.IsTrue(!actual.Headers.Any());
        }

        [TestMethod]
        public void Create_ResponseCode_HeaderParams_RequestCreatesCorrectly()
        {
            var header = UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1);
            var actual = ObexResponse.Create(ObexResponseCode.Ok, header);

            Assert.AreEqual(ObexResponseCode.Ok, actual.ResponseCode);
            Assert.IsNull(actual.ActualLength);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_ResponseCode_HeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1)
            };
            var actual = ObexResponse.Create(ObexResponseCode.Ok, headers);

            Assert.AreEqual(ObexResponseCode.Ok, actual.ResponseCode);
            Assert.IsNull(actual.ActualLength);
            Assert.IsTrue(headers.SequenceEqual(actual.Headers));
        }
        [TestMethod]
        public void Create_ResponseCode_ResponseLength_RequestCreatesCorrectly()
        {
            var actual = ObexResponse.Create(ObexResponseCode.Ok, 3);

            Assert.AreEqual(ObexResponseCode.Ok, actual.ResponseCode);
            Assert.AreEqual((ushort)3, actual.ActualLength);
            Assert.IsTrue(!actual.Headers.Any());
        }

        [TestMethod]
        public void Create_ResponseCode_ResponseLength_HeaderParams_RequestCreatesCorrectly()
        {
            var header = UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1);
            var actual = ObexResponse.Create(ObexResponseCode.Ok, 8, header);

            Assert.AreEqual(ObexResponseCode.Ok, actual.ResponseCode);
            Assert.AreEqual((ushort)8, actual.ActualLength);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_ResponseCode_ResponseLength_HeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1)
            };
            var actual = ObexResponse.Create(ObexResponseCode.Ok, 8, headers);

            Assert.AreEqual(ObexResponseCode.Ok, actual.ResponseCode);
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
            var request = ObexResponse.Create(ObexResponseCode.Ok, headers);

            var actual = request.GetHeadersForId(ObexHeaderId.ConnectionId);

            Assert.IsTrue(actual.All(x => x.Id == ObexHeaderId.ConnectionId));
        }
    }
}
