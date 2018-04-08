using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Tests.Header
{
    [TestClass]
    public class UInt32ObexHeaderTests
    {
        [TestMethod]
        public void Create_IdAndValue_HeaderCreatesSuccessfully()
        {
            var actual = UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, 1);

            Assert.AreEqual(ObexHeaderId.ConnectionId, actual.Id);
            Assert.AreEqual((ushort)5, actual.ActualLength);
            Assert.AreEqual((uint)1, actual.Value);
        }
    }
}
