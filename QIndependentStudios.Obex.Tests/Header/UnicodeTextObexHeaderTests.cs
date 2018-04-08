using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;

namespace QIndependentStudios.Obex.Tests.Header
{
    [TestClass]
    public class UnicodeTextObexHeaderTests
    {
        private const string _testValue = "telecom";

        [TestMethod]
        public void Create_Id_Value_HeaderCreatesSuccessfully()
        {
            var actual = UnicodeTextObexHeader.Create(ObexHeaderId.Name,
                _testValue);

            Assert.AreEqual(ObexHeaderId.Name, actual.Id);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(_testValue, actual.Value);
        }

        [TestMethod]
        public void Create_Id_HeaderLength_Value_HeaderCreatesSuccessfully()
        {
            var actual = UnicodeTextObexHeader.Create(ObexHeaderId.Name,
                19,
                _testValue);

            Assert.AreEqual(ObexHeaderId.Name, actual.Id);
            Assert.AreEqual((ushort)19, actual.ActualLength);
            Assert.AreEqual(_testValue, actual.Value);
        }
    }
}
