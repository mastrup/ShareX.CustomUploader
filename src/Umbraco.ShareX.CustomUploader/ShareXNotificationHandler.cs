using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;

namespace Umbraco.ShareX.CustomUploader
{
    public class ShareXNotificationHandler :
        INotificationHandler<ServerVariablesParsingNotification>,
        INotificationHandler<MenuRenderingNotification>
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IEntityService _entityService;
        private readonly IUserService _userService;
        private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;
        private readonly IMediaService _mediaService;

        public ShareXNotificationHandler(
            LinkGenerator linkGenerator,
            IEntityService entityService,
            IUserService userService,
            IBackOfficeSecurityAccessor backOfficeSercurityAccessor,
            IMediaService mediaService)
        {
            _linkGenerator = linkGenerator;
            _entityService = entityService;
            _userService = userService;
            _backOfficeSecurityAccessor = backOfficeSercurityAccessor;
            _mediaService = mediaService;
        }

        public void Handle(ServerVariablesParsingNotification notification)
        {
            notification.ServerVariables.Add("ShareX", new Dictionary<string, object>
            {
                //{ LinkedPages.Variables.ApiRoute, _linkGenerator.GetUmbracoApiServiceBaseUrl<LinkedPagesApiController>(c => c.GetApi()) },
                //{ LinkedPages.Variables.ShowRelationType, _config.ShowType },
                //{ LinkedPages.Variables.RelationTypeAlias, _config.RelationType },
                //{ LinkedPages.Variables.IgnoredTypes, _config.ignoredTypes }
            });
        }

        public void Handle(MenuRenderingNotification notification)
        {
            if (notification.TreeAlias != Constants.Trees.Media) return;

            var currentUser = _backOfficeSecurityAccessor.BackOfficeSecurity.CurrentUser;
            var showMenu = currentUser.Groups.Any(x => x.Alias.InvariantContains(Constants.Security.AdminGroupAlias));

            var node = _mediaService.GetById(int.Parse(notification.NodeId));
            if (node is null) return;
            bool isFolder = node.ContentType.Alias == Constants.Conventions.MediaTypes.Folder && !node.Trashed;

            if (showMenu && isFolder)
            {
                var item = new MenuItem("linkedPages", "ShareX Custom Uploader")
                {
                    Icon = "cloud-upload",
                    SeparatorBefore = true
                };

                item.AdditionalData.Add("actionView", "/App_Plugins/Umbraco.ShareX.CustomUploader/shareXDialog.html");

                notification.Menu.Items.Insert(notification.Menu.Items.Count - 1, item);
            }
        }
    }
}
