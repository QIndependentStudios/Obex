using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Tests.Header
{
    [TestClass]
    public class TextObexHeaderTests
    {
        private const string TestValue = "x-bt/MAP-msg-listing";

        [TestMethod]
        public void Create_Id_Value_HeaderCreatesSuccessfully()
        {
            var actual = TextObexHeader.Create(ObexHeaderId.Type,
                TestValue);

            Assert.AreEqual(ObexHeaderId.Type, actual.Id);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(TestValue, actual.Value);
        }

        [TestMethod]
        public void Create_Id_HeaderLength_Value_HeaderCreatesSuccessfully()
        {
            var actual = TextObexHeader.Create(ObexHeaderId.Type,
                24,
                TestValue);

            Assert.AreEqual(ObexHeaderId.Type, actual.Id);
            Assert.AreEqual((ushort)24, actual.ActualLength);
            Assert.AreEqual(TestValue, actual.Value);
        }
    }
}
