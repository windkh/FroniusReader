
namespace FroniusReader.DataTypes
{
    using Newtonsoft.Json;

    public class Details
    {
        [JsonProperty("Manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("Serial")]
        public string Serial { get; set; }
    }
}
