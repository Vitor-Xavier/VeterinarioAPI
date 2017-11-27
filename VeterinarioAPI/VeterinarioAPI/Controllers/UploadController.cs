using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace VeterinarioAPI.Controllers
{
    public class UploadController : ApiController
    {
        public List<string> AllowedFileTypes { get; set; } = new List<string> { "image/jpeg", "image/png", "image/bmp" };

        [HttpPost]
        [Route("Upload1/{filename}")]
        public async Task<IHttpActionResult> PostImage(string filename)
        {
            if(!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var content = await Request.Content.ReadAsMultipartAsync();
            if(!(AllowedFileTypes.Contains(content.Contents[0].Headers.ContentType.MediaType)))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            filename = String.Concat(filename, ".png");

            var storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("images");
            container.CreateIfNotExists();

            var blockBlob = container.GetBlockBlobReference(filename);
            blockBlob.Properties.ContentType = content.Contents[0].Headers.ContentType.ToString();
            using (var fileStream = await content.Contents[0].ReadAsStreamAsync()) 
            {
                blockBlob.UploadFromStream(fileStream);
            }

            return Ok(blockBlob.Uri.AbsoluteUri);
        }

        [HttpPost]
        [Route("Upload/{filename}")]
        public async Task<IHttpActionResult> PostByteArray(string filename)
        {
            var byteArray = await Request.Content.ReadAsByteArrayAsync();
            var stream = new MemoryStream(byteArray);

            filename = String.Concat(filename, ".png");

            var storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("images");
            container.CreateIfNotExists();

            var blockBlob = container.GetBlockBlobReference(filename);
            blockBlob.Properties.ContentType = "image/jpeg";
            using (var fileStream = new MemoryStream(byteArray))
            {
                blockBlob.UploadFromStream(fileStream);
            }

            return Ok(blockBlob.Uri.AbsoluteUri);
        }

    }
}
