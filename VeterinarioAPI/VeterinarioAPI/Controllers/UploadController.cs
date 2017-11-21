using System;
using System.Collections.Generic;
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
        [Route("Upload1")]
        public async Task<IHttpActionResult> PostImage()
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

            //var uploadPath = HostingEnvironment.MapPath("/") + @"/Uploads";
            //Directory.CreateDirectory(uploadPath);
            //var provider = new MultipartFormDataStreamProvider(uploadPath);
            //await Request.Content.ReadAsMultipartAsync(provider);

            //var file = provider.FileData;
            return Ok();
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IHttpActionResult> PostByteArray()
        {
            var tst = await Request.Content.ReadAsByteArrayAsync();
            var tst2 = new MemoryStream(tst);
            var tst3 = Image.FromStream(tst2);

            //try
            //{
            //    tst3.Save("C:\\Users\\Public\\Documents\\tst.png", System.Drawing.Imaging.ImageFormat.Png);
            //}
            //catch (Exception e)
            //{
            //    throw;
            //}
            
            return Ok();
        }

    }
}
