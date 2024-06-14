using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Core.Providers
{
    public class CustomUploadFile : MultipartFormDataStreamProvider
    {
        public CustomUploadFile(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            // override the filename which is stored by the provider (by default is bodypart_x)
            string oldfileName = headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            return string.Concat(
                Path.GetFileNameWithoutExtension(oldfileName),
                " - ",
                DateTime.Now.ToString("yyyyMMddHHmmss"),
                Path.GetExtension(oldfileName)
                );
        }
    }
}
