using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QIndependentStudios.Obex.Connection;
using QIndependentStudios.Obex.Header;
using System.Threading.Tasks;

namespace QIndependentStudios.Obex.Tests
{
    [TestClass]
    public class ObexClientTests
    {
        public Mock<IObexConnection> Connection;
        public ObexClient Client;

        [TestInitialize]
        public void Init()
        {
            Connection = new Mock<IObexConnection>();
            Client = new ObexClient(Connection.Object);
        }

        [TestMethod]
        public async Task GetAsync_ReceivesResponseWithNoHeaders_ReadsResponseCorrectly()
        {
            var request = ObexRequest.Create(ObexOpCode.Get);
            Connection.Setup(c => c.ReadAsync(1)).Returns(Task.FromResult(new[] { (byte)ObexResponseCode.Ok }));
            Connection.Setup(c => c.ReadAsync(2)).Returns(Task.FromResult(new byte[] { 0x00, 0x03 }));

            await Client.RequestAsync(request);

            Connection.Verify(c => c.EnsureInitAsync(), Times.Once);
            Connection.Verify(c => c.WriteAsync(new byte[] { 0x83, 0x00, 0x03 }), Times.Once);
            Connection.Verify(c => c.ReadAsync(1), Times.Once);
            Connection.Verify(c => c.ReadAsync(2), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_ReceivesResponseWithHeaders_ReadsResponseCorrectly()
        {
            var request = ObexConnectRequest.Create(255);
            Connection.Setup(c => c.ReadAsync(1)).Returns(Task.FromResult(new[] { (byte)ObexResponseCode.Ok }));
            Connection.Setup(c => c.ReadAsync(2)).Returns(Task.FromResult(new byte[] { 0x00, 0x0C }));
            Connection.Setup(c => c.ReadAsync(9))
                .Returns(Task.FromResult(new byte[] { 0x10, 0x00, 0x00, 0xFF, (byte)ObexHeaderId.ConnectionId, 0x00, 0x00, 0x00, 0x01 }));

            await Client.RequestAsync(request);

            Connection.Verify(c => c.EnsureInitAsync(), Times.Once);
            Connection.Verify(c => c.WriteAsync(new byte[] { 0x80, 0x00, 0x07, 0x10, 0x00, 0x00, 0xFF }), Times.Once);
            Connection.Verify(c => c.ReadAsync(1), Times.Once);
            Connection.Verify(c => c.ReadAsync(2), Times.Once);
            Connection.Verify(c => c.ReadAsync(9), Times.Once);
        }
    }
}
