using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Header
{
    [TestClass]
    public class TlvTripletTests
    {
        [TestMethod]
        public void Create_TagAndValues_ParameterCreatesSuccessfully()
        {
            var actual = TlvTriplet.Create(0x01, 0x02, 0x03);

            Assert.AreEqual(0x01, actual.Tag);
            Assert.IsTrue(new byte[] { 0x02, 0x03 }.SequenceEqual(actual.Value));
        }
    }
}
