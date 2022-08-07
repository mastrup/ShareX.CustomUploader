using System.Text.Json.Serialization;

namespace ShareX.CustomUploader.Models
{
    public class CustomUploaderModel
    {
        [JsonPropertyName("Version")]
        public string Version { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("DestinationType")]
        public string DestinationType { get; set; }

        [JsonPropertyName("RequestType")]
        public string RequestType { get; set; }

        [JsonPropertyName("RequestURL")]
        public string RequestURL { get; set; }

        [JsonPropertyName("Body")]
        public string Body { get; set; }

        [JsonPropertyName("Arguments")]
        public Arguments Arguments { get; set; } = new Arguments();

        [JsonPropertyName("FileFormName")]
        public string FileFormName { get; set; }

        [JsonPropertyName("URL")]
        public string URL { get; set; }

        [JsonPropertyName("ThumbnailURL")]
        public string ThumbnailURL { get; set; }

        [JsonPropertyName("DeletionURL")]
        public string DeletionURL { get; set; }

        [JsonPropertyName("ErrorMessage")]
        public string ErrorMessage { get; set; }
    }

    public class Arguments
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);