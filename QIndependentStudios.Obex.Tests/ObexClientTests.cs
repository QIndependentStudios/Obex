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
        public Mock<IObexConnection> _connection;
        public ObexClient _client;

        [TestInitialize]
        public void Init()
        {
            _connection = new Mock<IObexConnection>();
            _client = new ObexClient(_connection.Object);
        }

        [TestMethod]
        public async Task GetAsync_ReceivesResponseWithNoHeaders_ReadsResponseCorrectly()
        {
            var request = ObexRequest.Create(ObexOpCode.Get);
            _connection.Setup(c => c.ReadAsync(1)).Returns(Task.FromResult(new byte[] { (byte)ObexResponseCode.Ok }));
            _connection.Setup(c => c.ReadAsync(2)).Returns(Task.FromResult(new byte[] { 0x00, 0x03 }));

            var actual = await _client.GetAsync(request);

            _connection.Verify(c => c.EnsureInitAsync(), Times.Once);
            _connection.Verify(c => c.WriteAsync(new byte[] { 0x83, 0x00, 0x03 }), Times.Once);
            _connection.Verify(c => c.ReadAsync(1), Times.Once);
            _connection.Verify(c => c.ReadAsync(2), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_ReceivesResponseWithHeaders_ReadsResponseCorrectly()
        {
            var request = ObexConnectRequest.Create(255);
            _connection.Setup(c => c.ReadAsync(1)).Returns(Task.FromResult(new byte[] { (byte)ObexResponseCode.Ok }));
            _connection.Setup(c => c.ReadAsync(2)).Returns(Task.FromResult(new byte[] { 0x00, 0x0C }));
            _connection.Setup(c => c.ReadAsync(9))
                .Returns(Task.FromResult(new byte[] { 0x10, 0x00, 0x00, 0xFF, (byte)ObexHeaderId.ConnectionId, 0x00, 0x00, 0x00, 0x01 }));

            var actual = await _client.GetAsync(request);

            _connection.Verify(c => c.EnsureInitAsync(), Times.Once);
            _connection.Verify(c => c.WriteAsync(new byte[] { 0x80, 0x00, 0x07, 0x10, 0x00, 0x00, 0xFF }), Times.Once);
            _connection.Verify(c => c.ReadAsync(1), Times.Once);
            _connection.Verify(c => c.ReadAsync(2), Times.Once);
            _connection.Verify(c => c.ReadAsync(9), Times.Once);
        }
    }
}
