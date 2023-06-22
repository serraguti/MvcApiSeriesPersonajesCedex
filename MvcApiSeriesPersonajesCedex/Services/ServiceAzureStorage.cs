using Azure.Storage.Blobs;

namespace MvcApiSeriesPersonajesCedex.Services
{
    public class ServiceAzureStorage
    {
        private BlobContainerClient client;

        public ServiceAzureStorage(BlobContainerClient client)
        {
            this.client = client;
        }

        public async Task UploadBlobAsync
            (string fileName, Stream stream)
        {
            await this.client.UploadBlobAsync(fileName, stream);
        }
    }
}
