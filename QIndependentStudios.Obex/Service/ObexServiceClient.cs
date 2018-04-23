using QIndependentStudios.Obex.Connection;
using QIndependentStudios.Obex.Header;
using QIndependentStudios.Obex.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace QIndependentStudios.Obex.Service
{
    /// <summary>
    /// An Obex client that provides basic service implementations.
    /// </summary>
    public class ObexServiceClient
    {
        /// <summary>
        /// The folder browsing service Uuid defined in the specs.
        /// </summary>
        public static Guid FolderBrowsingServiceUuid = Guid.Parse("f9ec7bc4-953c-11d2-984e-525400dc9e09");

        /// <summary>
        /// The underlying Obex client to make requests and recieve responses.
        /// </summary>
        protected readonly ObexClient _client;

        /// <summary>
        /// The service Uuid of the service on the Obex server used when calling <see cref="ConnectAsync"/>.
        /// </summary>
        protected readonly Guid _serviceTarget;

        /// <summary>
        /// The connection id returned from the Obex server after calling <see cref="ConnectAsync"/>.
        /// </summary>
        protected uint _connectionId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObexServiceClient"/> class with a connection and a service target.
        /// </summary>
        /// <param name="connection">The connection to the Obex server.</param>
        /// <param name="serviceTarget">The service Uuid of the service on the Obex server to connect to.</param>
        public ObexServiceClient(IObexConnection connection, Guid serviceTarget)
        {
            _client = new ObexClient(connection);
            _serviceTarget = serviceTarget;
        }

        /// <summary>
        /// Connects to the specified service on the Obex server.
        /// </summary>
        /// <returns></returns>
        public async Task ConnectAsync()
        {
            var request = ObexConnectRequest.Create(5120,
                GuidObexHeader.Create(ObexHeaderId.Target, _serviceTarget));

            var response = await _client.RequestAsync(request);
            if (response.ResponseCode != ObexResponseCode.Ok)
                throw new Exception($"Attempt to connect failed: {response.ResponseCode}.");

            var connectionIdHeader = response.GetHeadersForId(ObexHeaderId.ConnectionId).FirstOrDefault() as UInt32ObexHeader;
            _connectionId = connectionIdHeader?.Value ?? 0;
        }

        /// <summary>
        /// Requests a listing of folders at the current folder location on the Obex server.
        /// </summary>
        /// <returns>A collection of Obex folder listing items.</returns>
        public async Task<IEnumerable<ObexFolderListingItem>> GetFoldersAsync()
        {
            var request = ObexRequest.Create(ObexOpCode.Get,
                UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, _connectionId),
                ByteSequenceObexHeader.Create(ObexHeaderId.Type, "x-obex/folder-listing"));

            var response = await _client.RequestAsync(request);
            var folderListingData = new StringBuilder(ObexHeaderUtil.GetBodyContent(response, Encoding.UTF8));
            request = ObexRequest.Create(ObexOpCode.Get);

            while (response.ResponseCode == ObexResponseCode.Continue)
            {
                response = await _client.RequestAsync(request);

                if ((response.ResponseCode != ObexResponseCode.Ok && response.ResponseCode != ObexResponseCode.Continue))
                    throw new Exception($"Attempt to get messages failed: {response.ResponseCode}.");

                folderListingData.Append(ObexHeaderUtil.GetBodyContent(response, Encoding.UTF8));
            }

            var serializer = new XmlSerializer(typeof(ObexFolderListingXmlData));
            using (var reader = new StringReader(folderListingData.ToString()))
            {
                var data = (ObexFolderListingXmlData)serializer.Deserialize(reader);
                if (data.Items == null)
                    return new List<ObexFolderListingItem>();

                return data.Items.Select(x => new ObexFolderListingItem(x.Name));
            }
        }

        /// <summary>
        /// Changes the folder location to a sub-folder located at the current location on the Obex server.
        /// </summary>
        /// <param name="folderName">The name of the sub-folder.</param>
        /// <returns>Whether the operation was successful.</returns>
        public async Task<bool> NavigateToFolderAsync(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
                throw new ArgumentException("Folder name cannot be null or empty.", nameof(folderName));

            return await NavigateFolderCoreAsync(ObexSetPathFlag.DownToNameOrRoot, folderName);
        }

        /// <summary>
        /// Changes the folder location to the root folder of the Obex server.
        /// </summary>
        /// <returns>Whether the operation was successful.</returns>
        public async Task<bool> NavigateToRootFolderAsync()
        {
            return await NavigateFolderCoreAsync(ObexSetPathFlag.DownToNameOrRoot, null);
        }

        /// <summary>
        /// Changes the folder location to be one level up from current location on the Obex server.
        /// </summary>
        /// <returns>Whether the operation was successful.</returns>
        public async Task<bool> NavigateUpFolderAsync()
        {
            return await NavigateFolderCoreAsync(ObexSetPathFlag.Up, null);
        }

        private async Task<bool> NavigateFolderCoreAsync(ObexSetPathFlag flag, string folderName)
        {
            var headers = new List<ObexHeader> { UInt32ObexHeader.Create(ObexHeaderId.ConnectionId, _connectionId) };

            if (folderName != null)
                headers.Add(UnicodeTextObexHeader.Create(ObexHeaderId.Name, folderName));

            var request = ObexSetPathRequest.Create(flag, headers);

            var response = await _client.RequestAsync(request);
            return response.ResponseCode == ObexResponseCode.Ok;
        }
    }
}
