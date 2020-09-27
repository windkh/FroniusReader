
namespace FroniusReader.DataTypes
{
    using Newtonsoft.Json;

    public class ApiVersion
    {
        [JsonProperty("APIVersion")]
        public int APIVersion { get; set; }

        [JsonProperty("BaseURL")]
        public string BaseURL { get; set; }

        [JsonProperty("CompatibilityRange")]
        public string CompatibilityRange { get; set; }
    }
}
