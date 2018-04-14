using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Header
{
    [TestClass]
    public class TlvCollectionObexHeaderTests
    {
        [TestMethod]
        public void Create_ParameterParams_HeaderCreatesSuccessfully()
        {
            var firstAppParam = TlvTriplet.Create(0x01, 0x02);
            var secondAppParam = TlvTriplet.Create(0x03, 0x04);
            var actual = TlvCollectionObexHeader.Create(ObexHeaderId.ApplicationParameter,
                firstAppParam,
                secondAppParam);

            Assert.AreEqual(ObexHeaderId.ApplicationParameter, actual.Id);
            Assert.IsNull(actual.ActualLength);
            Assert.IsTrue(new List<TlvTriplet> { firstAppParam, secondAppParam }.SequenceEqual(actual.Tlvs));
        }

        [TestMethod]
        public void Create_ParameterEnumerable_HeaderCreatesSuccessfully()
        {
            var parameters = new List<TlvTriplet>
            {
                TlvTriplet.Create(0x01, 0x02),
                TlvTriplet.Create(0x03, 0x04)
            };
            var actual = TlvCollectionObexHeader.Create(ObexHeaderId.ApplicationParameter,
                parameters);

            Assert.AreEqual(ObexHeaderId.ApplicationParameter, actual.Id);
            Assert.IsNull(actual.ActualLength);
            Assert.IsTrue(parameters.SequenceEqual(actual.Tlvs));
        }

        [TestMethod]
        public void Create_HeaderLength_ParameterParams_HeaderCreatesSuccessfully()
        {
            var firstAppParam = TlvTriplet.Create(0x01, 0x02);
            var secondAppParam = TlvTriplet.Create(0x03, 0x04);
            var actual = TlvCollectionObexHeader.Create(ObexHeaderId.ApplicationParameter,
                9,
                firstAppParam,
                secondAppParam);

            Assert.AreEqual(ObexHeaderId.ApplicationParameter, actual.Id);
            Assert.AreEqual((ushort)9, actual.ActualLength);
            Assert.IsTrue(new List<TlvTriplet> { firstAppParam, secondAppParam }.SequenceEqual(actual.Tlvs));
        }

        [TestMethod]
        public void Create_HeaderLength_ParameterEnumerable_HeaderCreatesSuccessfully()
        {
            var parameters = new List<TlvTriplet>
            {
                TlvTriplet.Create(0x01, 0x02),
                TlvTriplet.Create(0x03, 0x04)
            };
            var actual = TlvCollectionObexHeader.Create(ObexHeaderId.ApplicationParameter,
                9,
                parameters);

            Assert.AreEqual(ObexHeaderId.ApplicationParameter, actual.Id);
            Assert.AreEqual((ushort)9, actual.ActualLength);
            Assert.IsTrue(parameters.SequenceEqual(actual.Tlvs));
        }
    }
}
