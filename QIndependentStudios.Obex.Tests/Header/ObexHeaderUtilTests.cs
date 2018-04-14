using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Tests.Header
{
    [TestClass]
    public class ObexHeaderUtilTests
    {
        [TestMethod]
        public void GetHeaderEncoding_GivenHeaderIds_ReturnsHeadersEncoding()
        {
            Assert.AreEqual(ObexHeaderEncoding.NullTermUnicodeWithLength,
                ObexHeaderUtil.GetHeaderEncoding(ObexHeaderId.Description));
            Assert.AreEqual(ObexHeaderEncoding.NullTermUnicodeWithLength,
                ObexHeaderUtil.GetHeaderEncoding(ObexHeaderId.Name));
            Assert.AreEqual(ObexHeaderEncoding.SingleByte,
                ObexHeaderUtil.GetHeaderEncoding(ObexHeaderId.SingleResponseMode));
            Assert.AreEqual(ObexHeaderEncoding.SingleByte,
                ObexHeaderUtil.GetHeaderEncoding(ObexHeaderId.SingleResponseModeParameter));
            Assert.AreEqual(ObexHeaderEncoding.FourBytes,
                ObexHeaderUtil.GetHeaderEncoding(ObexHeaderId.Count));
            Assert.AreEqual(ObexHeaderEncoding.FourBytes,
                ObexHeaderUtil.GetHeaderEncoding(ObexHeaderId.Length));
            Assert.AreEqual(ObexHeaderEncoding.FourBytes,
                ObexHeaderUtil.GetHeaderEncoding(ObexHeaderId.ConnectionId));
        }
    }
}
