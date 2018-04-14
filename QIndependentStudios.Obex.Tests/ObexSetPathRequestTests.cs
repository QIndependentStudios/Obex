using Microsoft.VisualStudio.TestTools.UnitTesting;
using QIndependentStudios.Obex.Header;
using QIndependentStudios.Obex.Path;
using System.Collections.Generic;
using System.Linq;

namespace QIndependentStudios.Obex.Tests
{
    [TestClass]
    public class ObexSetPathRequestTests
    {
        private const string TestFolder = "telecom";

        [TestMethod]
        public void Create_SetPathFlag_RequestCreatesCorrectly()
        {
            var actual = ObexSetPathRequest.Create(ObexSetPathFlag.DownToNameOrRoot);

            Assert.AreEqual(ObexOpCode.SetPath, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(ObexSetPathFlag.DownToNameOrRoot, actual.Flags);
            Assert.AreEqual(0x00, actual.Constant);
            Assert.IsTrue(!actual.Headers.Any());
        }

        [TestMethod]
        public void Create_SetPathFlag_HeaderParams_RequestCreatesCorrectly()
        {
            var header = TextObexHeader.Create(ObexHeaderId.Name, TestFolder);
            var actual = ObexSetPathRequest.Create(ObexSetPathFlag.DownToNameOrRoot, header);

            Assert.AreEqual(ObexOpCode.SetPath, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(ObexSetPathFlag.DownToNameOrRoot, actual.Flags);
            Assert.AreEqual(0x00, actual.Constant);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_SetPathFlag_HeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                TextObexHeader.Create(ObexHeaderId.Name, TestFolder)
            };
            var actual = ObexSetPathRequest.Create(ObexSetPathFlag.DownToNameOrRoot, headers);

            Assert.AreEqual(ObexOpCode.SetPath, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(ObexSetPathFlag.DownToNameOrRoot, actual.Flags);
            Assert.AreEqual(0x00, actual.Constant);
            Assert.IsTrue(headers.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_RequestLength_SetPathFlag_RequestCreatesCorrectly()
        {
            var actual = ObexSetPathRequest.Create(5, ObexSetPathFlag.DownToNameOrRoot);

            Assert.AreEqual(ObexOpCode.SetPath, actual.OpCode);
            Assert.AreEqual((ushort)5, actual.ActualLength);
            Assert.AreEqual(ObexSetPathFlag.DownToNameOrRoot, actual.Flags);
            Assert.AreEqual(0x00, actual.Constant);
            Assert.IsTrue(!actual.Headers.Any());
        }

        [TestMethod]
        public void Create_RequestLength_SetPathFlag_HeaderParams_RequestCreatesCorrectly()
        {
            var header = TextObexHeader.Create(ObexHeaderId.Name, TestFolder);
            var actual = ObexSetPathRequest.Create(11, ObexSetPathFlag.DownToNameOrRoot, header);

            Assert.AreEqual(ObexOpCode.SetPath, actual.OpCode);
            Assert.AreEqual((ushort)11, actual.ActualLength);
            Assert.AreEqual(ObexSetPathFlag.DownToNameOrRoot, actual.Flags);
            Assert.AreEqual(0x00, actual.Constant);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_RequestLength_SetPathFlag_HeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                TextObexHeader.Create(ObexHeaderId.Name, TestFolder)
            };
            var actual = ObexSetPathRequest.Create(11, ObexSetPathFlag.DownToNameOrRoot, headers);

            Assert.AreEqual(ObexOpCode.SetPath, actual.OpCode);
            Assert.AreEqual((ushort)11, actual.ActualLength);
            Assert.AreEqual(ObexSetPathFlag.DownToNameOrRoot, actual.Flags);
            Assert.AreEqual(0x00, actual.Constant);
            Assert.IsTrue(headers.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_SetPathFlag_Constant_RequestCreatesCorrectly()
        {
            var actual = ObexSetPathRequest.Create(ObexSetPathFlag.DownToNameOrRoot, 0x11);

            Assert.AreEqual(ObexOpCode.SetPath, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(ObexSetPathFlag.DownToNameOrRoot, actual.Flags);
            Assert.AreEqual(0x11, actual.Constant);
            Assert.IsTrue(!actual.Headers.Any());
        }

        [TestMethod]
        public void Create_SetPathFlag_Constant_HeaderParams_RequestCreatesCorrectly()
        {
            var header = TextObexHeader.Create(ObexHeaderId.Name, TestFolder);
            var actual = ObexSetPathRequest.Create(ObexSetPathFlag.DownToNameOrRoot, 0x11, header);

            Assert.AreEqual(ObexOpCode.SetPath, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(ObexSetPathFlag.DownToNameOrRoot, actual.Flags);
            Assert.AreEqual(0x11, actual.Constant);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_SetPathFlag_Constant_HeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                TextObexHeader.Create(ObexHeaderId.Name, TestFolder)
            };
            var actual = ObexSetPathRequest.Create(ObexSetPathFlag.DownToNameOrRoot, 0x11, headers);

            Assert.AreEqual(ObexOpCode.SetPath, actual.OpCode);
            Assert.IsNull(actual.ActualLength);
            Assert.AreEqual(ObexSetPathFlag.DownToNameOrRoot, actual.Flags);
            Assert.AreEqual(0x11, actual.Constant);
            Assert.IsTrue(headers.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_RequestLength_SetPathFlag_Constant_RequestCreatesCorrectly()
        {
            var actual = ObexSetPathRequest.Create(5, ObexSetPathFlag.DownToNameOrRoot, 0x11);

            Assert.AreEqual(ObexOpCode.SetPath, actual.OpCode);
            Assert.AreEqual((ushort)5, actual.ActualLength);
            Assert.AreEqual(ObexSetPathFlag.DownToNameOrRoot, actual.Flags);
            Assert.AreEqual(0x11, actual.Constant);
            Assert.IsTrue(!actual.Headers.Any());
        }

        [TestMethod]
        public void Create_RequestLength_SetPathFlag_Constant_HeaderParams_RequestCreatesCorrectly()
        {
            var header = TextObexHeader.Create(ObexHeaderId.Name, TestFolder);
            var actual = ObexSetPathRequest.Create(11, ObexSetPathFlag.DownToNameOrRoot, 0x11, header);

            Assert.AreEqual(ObexOpCode.SetPath, actual.OpCode);
            Assert.AreEqual((ushort)11, actual.ActualLength);
            Assert.AreEqual(ObexSetPathFlag.DownToNameOrRoot, actual.Flags);
            Assert.AreEqual(0x11, actual.Constant);
            Assert.IsTrue(new List<ObexHeader> { header }.SequenceEqual(actual.Headers));
        }

        [TestMethod]
        public void Create_RequestLength_SetPathFlag_Constant_HeaderEnumerable_RequestCreatesCorrectly()
        {
            var headers = new List<ObexHeader>
            {
                TextObexHeader.Create(ObexHeaderId.Name, TestFolder)
            };
            var actual = ObexSetPathRequest.Create(11, ObexSetPathFlag.DownToNameOrRoot, 0x11, headers);

            Assert.AreEqual(ObexOpCode.SetPath, actual.OpCode);
            Assert.AreEqual((ushort)11, actual.ActualLength);
            Assert.AreEqual(ObexSetPathFlag.DownToNameOrRoot, actual.Flags);
            Assert.AreEqual(0x11, actual.Constant);
            Assert.IsTrue(headers.SequenceEqual(actual.Headers));
        }
    }
}
