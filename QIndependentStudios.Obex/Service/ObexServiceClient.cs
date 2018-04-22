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
    public class ObexServiceClient
    {
        private const string FolderBrowsingService = "f9ec7bc4-953c-11d2-984e-525400dc9e09";
        private readonly ObexClient _client;
        private readonly Guid _serviceTarget;
        private uint _connectionId;

        public ObexServiceClient(IObexConnection connection, Guid serviceTarget)
        {
            _client = new ObexClient(connection);
            _serviceTarget = serviceTarget;
        }

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

        public async Task<bool> NavigateToFolderAsync(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
                throw new ArgumentException("Folder name cannot be null or empty.", nameof(folderName));

            return await NavigateFolderCoreAsync(ObexSetPathFlag.DownToNameOrRoot, folderName);
        }

        public async Task<bool> NavigateToRootFolderAsync()
        {
            return await NavigateFolderCoreAsync(ObexSetPathFlag.DownToNameOrRoot, null);
        }

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
