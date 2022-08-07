# ShareX.CustomUploader
[![Nuget](https://img.shields.io/nuget/v/ShareX.CustomUploader)](https://www.nuget.org/packages/ShareX.CustomUploader/)

## Prerequisites
Requires at least Umbraco 10.0.0.

## How To Use
### Installation

Install ShareX.CustomUploader via NuGet: `Install-Package ShareX.CustomUploader`.

After restarting your site, you'll find a new context menu item on your media folders - 'ShareX Custom Uploader'.

The dialog generates the needed JSON to use with ShareX. After configuring ShareX, you will be able to upload images, text and video directly to your Umbraco media archive.

![ShareX Custom Uploader](https://raw.githubusercontent.com/mastrup/ShareX.CustomUploader/main/assets/screenshot.png "ShareX Custom Uploader screnshot")



### Configuring ShareX

If you haven't already installed ShareX, you can [get it from here](https://getsharex.com/).

1. Get the code from the ShareX Custom Uploader dialog in Umbraco and copy the code to your clipboard.
2. Open the ShareX window.
3. Click the "Destinations" drop-down then click "Custom uploader settings...".
4. Click the "Import" drop-down then click "From clipboard".
5. Make sure the uploaders are set to your newly created custom uploader.

After configuring ShareX, all of your captures through ShareX will be uploaded directly to Umbraco.<br>
When uploading, Umbraco will reply with 3 URLs that will be saved within ShareX:
1. A direct URL for the uploaded file
2. A thumbnail URL
3. A deletion URL, that lets your delete the uploaded file directly from ShareX.
