using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;
using System;

namespace QIndependentStudios.Obex.Tests.Header
{
    [TestClass]
    public class GuidObexHeaderTests
    {
        private static readonly Guid TestValue = Guid.Parse("bb582b40-420c-11db-b0de-0800200c9a66");

        [TestMethod]
        public void Create_Id_Value_HeaderCreatesSuccessfully()
        {
            var actual = GuidObexHeader.Create(ObexHeaderId.Target,
                TestValue);

            Assert.AreEqual(ObexHeaderId.Target, actual.Id);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(TestValue, actual.Value);
        }

        [TestMethod]
        public void Create_Id_HeaderLength_Value_HeaderCreatesSuccessfully()
        {
            var actual = GuidObexHeader.Create(ObexHeaderId.Target,
                19,
                TestValue);

            Assert.AreEqual(ObexHeaderId.Target, actual.Id);
            Assert.AreEqual((ushort)19, actual.ActualLength);
            Assert.AreEqual(TestValue, actual.Value);
        }
    }
}
