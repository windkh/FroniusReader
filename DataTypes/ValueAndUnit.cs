
namespace FroniusReader.DataTypes
{
    using Newtonsoft.Json;

    public class ValueAndUnit
    {
        [JsonProperty("Unit")]
        public string Unit { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}
