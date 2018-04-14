using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;
using System;

namespace QIndependentStudios.Obex.Tests.Header
{
    [TestClass]
    public class DateTimeObexHeaderTests
    {
        private static readonly DateTime TestValue = new DateTime(2018, 4, 14);

        [TestMethod]
        public void Create_TimeId_Value_HeaderCreatesSuccessfully()
        {
            var actual = DateTimeObexHeader.Create(ObexHeaderId.Time,
                TestValue);

            Assert.AreEqual(ObexHeaderId.Time, actual.Id);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(TestValue, actual.Value);
        }

        [TestMethod]
        public void Create_TimeId_HeaderLength_Value_HeaderCreatesSuccessfully()
        {
            var actual = DateTimeObexHeader.Create(ObexHeaderId.Time,
                18,
                TestValue);

            Assert.AreEqual(ObexHeaderId.Time, actual.Id);
            Assert.AreEqual((ushort)18, actual.ActualLength);
            Assert.AreEqual(TestValue, actual.Value);
        }

        [TestMethod]
        public void Create_Time4BytesId_Value_HeaderCreatesSuccessfully()
        {
            var actual = DateTimeObexHeader.Create(ObexHeaderId.Time4Byte,
                TestValue);

            Assert.AreEqual(ObexHeaderId.Time4Byte, actual.Id);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(TestValue, actual.Value);
        }

        [TestMethod]
        public void Create_Time4BytesId_HeaderLength_Value_HeaderCreatesSuccessfully()
        {
            var actual = DateTimeObexHeader.Create(ObexHeaderId.Time4Byte,
                5,
                TestValue);

            Assert.AreEqual(ObexHeaderId.Time4Byte, actual.Id);
            Assert.AreEqual((ushort)5, actual.ActualLength);
            Assert.AreEqual(TestValue, actual.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_UnsupportedId_Value_HeaderCreatesSuccessfully()
        {
            var actual = DateTimeObexHeader.Create(ObexHeaderId.Type,
                TestValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_UnsupportedId_HeaderLength_Value_HeaderCreatesSuccessfully()
        {
            var actual = DateTimeObexHeader.Create(ObexHeaderId.Type,
                18,
                TestValue);
        }
    }
}
