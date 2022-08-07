using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.ShareX.CustomUploader.Models
{
    public class UploadResponse
    {
        public string Url { get; set; } = "";
        public string ThumbnailUrl { get; set; } = "";
        public string DeletionUrl { get; set; } = "";
        public string ErrorMessage { get; set; } = "";
    }
}
