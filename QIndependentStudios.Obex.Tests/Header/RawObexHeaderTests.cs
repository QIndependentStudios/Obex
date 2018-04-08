using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Header
{
    [TestClass]
    public class RawObexHeaderTests
    {
        private static readonly List<byte> _testValue = new List<byte> { 0x01, 0x02, 0x03 };

        [TestMethod]
        public void Create_Id_ValueParams_HeaderCreatesSuccessfully()
        {
            var actual = RawObexHeader.Create(ObexHeaderId.Type,
                _testValue[0],
                _testValue[1],
                _testValue[2]);

            Assert.AreEqual(ObexHeaderId.Type, actual.Id);
            Assert.AreEqual((ushort)6, actual.ActualLength);
            Assert.IsTrue(_testValue.SequenceEqual(actual.Value));
        }

        [TestMethod]
        public void Create_Id_HeaderLength_Value_HeaderCreatesSuccessfully()
        {
            var actual = RawObexHeader.Create(ObexHeaderId.Type, _testValue);

            Assert.AreEqual(ObexHeaderId.Type, actual.Id);
            Assert.AreEqual((ushort)6, actual.ActualLength);
            Assert.IsTrue(_testValue.SequenceEqual(actual.Value));
        }
    }
}
