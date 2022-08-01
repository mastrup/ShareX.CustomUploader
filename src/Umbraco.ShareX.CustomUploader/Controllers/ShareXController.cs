using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Serialization;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;
using Umbraco.ShareX.CustomUploader.Models;

namespace Umbraco.ShareX.CustomUploader.Controllers
{
    public class ShareXController : UmbracoApiController
    {
        private IMediaService _mediaService;
        private MediaFileManager _mediaFileManager;
        private readonly MediaUrlGeneratorCollection _mediaUrlGenerators;
        private IShortStringHelper _shortStringHelper;
        private IContentTypeBaseServiceProvider _contentTypeBaseServiceProvider;

        public ShareXController(IMediaService mediaService,
                                MediaFileManager mediaFileManager,
                                MediaUrlGeneratorCollection mediaUrlGenerators,
                                IShortStringHelper shortStringHelper,
                                IContentTypeBaseServiceProvider contentTypeBaseServiceProvider,
                                IJsonSerializer serializer)
        {
            _mediaService = mediaService;
            _mediaFileManager = mediaFileManager;
            _mediaUrlGenerators = mediaUrlGenerators;
            _shortStringHelper = shortStringHelper;
            _contentTypeBaseServiceProvider = contentTypeBaseServiceProvider;
        }

        [HttpPost]
        public UploadResponse Upload(Guid key)
        {
            var filePath = Path.GetTempFileName();
            var files = Request.Form.Files;

            var createdFiles = UploadFiles(key, files);

            return createdFiles;
        }

        private UploadResponse UploadFiles(Guid key, IFormFileCollection files)
        {
            var response = new UploadResponse();

            if(_mediaService.GetById(key) == null)
            {
                response.ErrorMessage = "Could not find upload folder.";
                return response;
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string extension = Path.GetExtension(file.FileName);
                    var extensions = new Dictionary<string[], string>()
                    {
                        { new[] { ".pdf", ".docx", ".doc" }, Constants.Conventions.MediaTypes.Article },
                        { new[] { ".mp3", ".weba", ".oga", ".opus" }, Constants.Conventions.MediaTypes.Audio},
                        { new[] { ".svg" }, Constants.Conventions.MediaTypes.VectorGraphics },
                        { new[] { ".mp4", ".webm", ".ogv" }, Constants.Conventions.MediaTypes.Video},
                        { new[] { ".jpeg", ".jpg", ".gif", ".bmp", ".png", ".tiff", ".tif" }, Constants.Conventions.MediaTypes.Image},
                    };

                    var mediaType = extensions.FirstOrDefault(x => x.Key.Contains(extension)).Value;
                    if (mediaType == null) mediaType = Constants.Conventions.MediaTypes.File;

                    IMedia mediaItem = _mediaService.CreateMedia(file.FileName, key, mediaType);
                    mediaItem.SetValue(_mediaFileManager, _mediaUrlGenerators, _shortStringHelper, _contentTypeBaseServiceProvider, Constants.Conventions.Media.File, file.FileName, file.OpenReadStream());
                    _mediaService.Save(mediaItem);

                    response.Url = mediaItem.GetUrl(Constants.Conventions.Media.File, _mediaUrlGenerators);
                    response.ThumbnailUrl = $"{response.Url}?width=500&height=500";
                    response.DeletionUrl = "deletion URL";
                }
            }

            return response;
        }
    }
}