
using Microsoft.AspNetCore.Http;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.ShareX.CustomUploader.Models;

namespace Umbraco.ShareX.CustomUploader.Controllers
{
    public class CustomUploaderResponseController : UmbracoAuthorizedApiController
    {
        private IMediaService _mediaService;
        public CustomUploaderResponseController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        public CustomUploaderModel GetCustomUploader(int nodeId)
        {
            var node = _mediaService.GetById(nodeId);
            var request = HttpContext.Request;

            var model = new CustomUploaderModel()
            {
                Version = "14.0.1",
                Name = node.Name,
                DestinationType = "ImageUploader, TextUploader, FileUploader",
                RequestType = "POST",
                RequestURL = $"{request.Scheme}://{request.Host.Value}/umbraco/api/sharex/upload",
                Body = "MultipartFormData",
                Arguments =
                {
                    Key = node.Key.ToString()
                },
                FileFormName = "file",
                URL = "{json:url}",
                ThumbnailURL = "{json:thumbnailUrl}",
                DeletionURL = "{json:deletionUrl}",
                ErrorMessage = "{json:errorMessage}"
            };

            return model;
        }
    }
}