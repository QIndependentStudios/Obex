using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Tests.Header
{
    [TestClass]
    public class ByteObexHeaderTests
    {
        [TestMethod]
        public void Create_IdAndValue_HeaderCreatesSuccessfully()
        {
            var actual = ByteObexHeader.Create(ObexHeaderId.SingleResponseMode, 0x01);

            Assert.AreEqual(ObexHeaderId.SingleResponseMode, actual.Id);
            Assert.AreEqual((ushort)2, actual.ActualLength);
            Assert.AreEqual(0x01, actual.Value);
        }
    }
}
