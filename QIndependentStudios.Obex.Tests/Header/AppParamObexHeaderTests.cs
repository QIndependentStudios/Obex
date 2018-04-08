using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Tests.Header
{
    [TestClass]
    public class AppParamObexHeaderTests
    {
        [TestMethod]
        public void Create_ParameterParams_HeaderCreatesSuccessfully()
        {
            ObexAppParameter firstAppParam = ObexAppParameter.Create(0x01, 0x02);
            ObexAppParameter secondAppParam = ObexAppParameter.Create(0x03, 0x04);
            var actual = AppParamObexHeader.Create(firstAppParam,
                secondAppParam);

            Assert.AreEqual(ObexHeaderId.ApplicationParameter, actual.Id);
            Assert.IsNull(actual.ActualLength);
            Assert.IsTrue(new List<ObexAppParameter> { firstAppParam, secondAppParam }.SequenceEqual(actual.Parameters));
        }

        [TestMethod]
        public void Create_ParameterEnumerable_HeaderCreatesSuccessfully()
        {
            var parameters = new List<ObexAppParameter>
            {
                ObexAppParameter.Create(0x01, 0x02),
                ObexAppParameter.Create(0x03, 0x04)
            };
            var actual = AppParamObexHeader.Create(parameters);

            Assert.AreEqual(ObexHeaderId.ApplicationParameter, actual.Id);
            Assert.IsNull(actual.ActualLength);
            Assert.IsTrue(parameters.SequenceEqual(actual.Parameters));
        }

        [TestMethod]
        public void Create_HeaderLength_ParameterParams_HeaderCreatesSuccessfully()
        {
            ObexAppParameter firstAppParam = ObexAppParameter.Create(0x01, 0x02);
            ObexAppParameter secondAppParam = ObexAppParameter.Create(0x03, 0x04);
            var actual = AppParamObexHeader.Create(9,
                firstAppParam,
                secondAppParam);

            Assert.AreEqual(ObexHeaderId.ApplicationParameter, actual.Id);
            Assert.AreEqual((ushort)9, actual.ActualLength);
            Assert.IsTrue(new List<ObexAppParameter> { firstAppParam, secondAppParam }.SequenceEqual(actual.Parameters));
        }

        [TestMethod]
        public void Create_HeaderLength_ParameterEnumerable_HeaderCreatesSuccessfully()
        {
            var parameters = new List<ObexAppParameter>
            {
                ObexAppParameter.Create(0x01, 0x02),
                ObexAppParameter.Create(0x03, 0x04)
            };
            var actual = AppParamObexHeader.Create(9, parameters);

            Assert.AreEqual(ObexHeaderId.ApplicationParameter, actual.Id);
            Assert.AreEqual((ushort)9, actual.ActualLength);
            Assert.IsTrue(parameters.SequenceEqual(actual.Parameters));
        }
    }
}
