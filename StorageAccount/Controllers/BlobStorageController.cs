using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StorageAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobStorageController : ControllerBase
    {
        [HttpPost("uploadBlob")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var ConnectionString = "DefaultEndpointsProtocol=https;AccountName=projektname1sanefirst;AccountKey=VoyPAqBLi8C7xCvakuwVn+e82sHTuUapl2Gb0+siV11SvoMD9F6bHjcm0d2iWyOaVW5bQXh8q9pW+AStAQwnKQ==;EndpointSuffix=core.windows.net";
            BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);

            var containerName = "mydoc";
            BlobContainerClient blobContainerClient =  blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync();
            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
            // await blobClient.UploadAsync(file.OpenReadStream(), overwrite:true);
            var BlobHttpHeaders = new BlobHttpHeaders();
            BlobHttpHeaders.ContentType = file.ContentType;
            await blobClient.UploadAsync(file.OpenReadStream(), BlobHttpHeaders);
            return Ok();
        }
        [HttpGet("DownloadBlob")]
        public async Task<IActionResult> Download([FromQuery] string blobName)
        {
            var ConnectionString = "DefaultEndpointsProtocol=https;AccountName=projektname1sanefirst;AccountKey=VoyPAqBLi8C7xCvakuwVn+e82sHTuUapl2Gb0+siV11SvoMD9F6bHjcm0d2iWyOaVW5bQXh8q9pW+AStAQwnKQ==;EndpointSuffix=core.windows.net";
            BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);

            var containerName = "mydoc";
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync();
            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
            // await blobClient.UploadAsync(file.OpenReadStream(), overwrite:true);
            var BlobHttpHeaders = new BlobHttpHeaders();
            var Download = await blobClient.DownloadContentAsync();
           var Content =  Download.Value.Content.ToStream();
            var ContentType = blobClient.GetProperties().Value.ContentType;
            return File(Content, ContentType, fileDownloadName: blobName);
           
        }
    }
}
