using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Serialization;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;

namespace Umbraco.ShareX.CustomUploader.Controllers
{
    public class ShareXController : UmbracoApiController
    {
        private IMediaTypeService _mediaTypeService;
        private IMediaService _mediaService;
        private MediaFileManager _mediaFileManager;
        private readonly MediaUrlGeneratorCollection _mediaUrlGenerators;
        private IShortStringHelper _shortStringHelper;
        private IContentTypeBaseServiceProvider _contentTypeBaseServiceProvider;
        private IJsonSerializer _serializer;

        public ShareXController(IMediaTypeService mediaTypeService, IMediaService mediaService, MediaFileManager mediaFileManager, MediaUrlGeneratorCollection mediaUrlGenerators, IShortStringHelper shortStringHelper, IContentTypeBaseServiceProvider contentTypeBaseServiceProvider, IJsonSerializer serializer)
        {
            _mediaTypeService = mediaTypeService;
            _mediaService = mediaService;
            _mediaFileManager = mediaFileManager;
            _mediaUrlGenerators = mediaUrlGenerators;
            _shortStringHelper = shortStringHelper;
            _contentTypeBaseServiceProvider = contentTypeBaseServiceProvider;
            _serializer = serializer;
        }

        [HttpPost]
        public string Upload(Guid key)
        {
            var filePath = Path.GetTempFileName();
            var files = Request.Form.Files;

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var asdasd = file.OpenReadStream();
                    IMedia mediaItem = _mediaService.CreateMedia(file.FileName, key, Constants.Conventions.MediaTypes.Image);
                    mediaItem.SetValue(_mediaFileManager, _mediaUrlGenerators, _shortStringHelper, _contentTypeBaseServiceProvider, Constants.Conventions.Media.File, file.FileName, asdasd);
                    _mediaService.Save(mediaItem);

                    return mediaItem.GetUrl(Constants.Conventions.Media.File, _mediaUrlGenerators) + "?key=" + mediaItem.Key;
                    //IMedia mediaItem = _mediaService.CreateMedia(file.FileName, key, "Image");
                    //mediaItem.SetValue(mediaFileManager, mediaUrlGenerators, shortStringHelper, contentTypeBaseServiceProvider, Constants.Conventions.Media.File, model.myfile.FileName, stream);
                    //mediaItem.SetValue(_contentTypeBaseServiceProvider, "umbracoFile", file.FileName, file);
                    //_mediaService.Save(mediaItem);
                }
            }

            //f4672296-b015-419d-97f5-9fd97cc9e1ff
            return key.ToString();
        }
    }
}
