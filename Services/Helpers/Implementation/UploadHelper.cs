using System;
using Microsoft.Extensions.Options;
using mvc.Services.Helpers.Interface;
using mvc.Services.Helpers.Options;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace mvc.Services.Helpers.Implementation
{
	public class UploadHelper : IUploadHelper
	{
        private readonly AzureOptions _azureOptions;
		public UploadHelper(IOptions<AzureOptions> azureOptions)
		{
            _azureOptions = azureOptions.Value;
		}

        public string UploadToAzure(IFormFile File)
        {
            string fileExtension = Path.GetExtension(File.FileName);
            using MemoryStream fileUploadSream = new MemoryStream();
            File.CopyTo(fileUploadSream);
            fileUploadSream.Position = 0;

            var blobContainerClient = new BlobContainerClient(
                _azureOptions.ConnectionString,
                _azureOptions.Container);
            var uniqueName = Guid.NewGuid().ToString() + fileExtension;
            BlobClient blobClient = blobContainerClient.GetBlobClient(uniqueName);

            blobClient.Upload(fileUploadSream, new BlobUploadOptions()
            {
                  
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = "application/octet-stream"
                }
            }
            , cancellationToken: default);

            return blobClient.Uri.AbsoluteUri.ToString();
        }
    }
}

