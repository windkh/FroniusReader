
namespace FroniusReader.DataTypes
{
    using Newtonsoft.Json;

    public class InverterRealTimeData
    {
        [JsonProperty("TOTAL_ENERGY")]
        public ValueAndUnit TotalEnergy { get; set; }

        [JsonProperty("Day_ENERGY")]
        public ValueAndUnit DayEnergy { get; set; }

        [JsonProperty("YEAR_ENERGY")]
        public ValueAndUnit YearEnergy { get; set; }

        [JsonProperty("PAC")]
        public ValueAndUnit PAC { get; set; }

        [JsonProperty("UAC")]
        public ValueAndUnit UAC { get; set; }

        [JsonProperty("UDC")]
        public ValueAndUnit UDC { get; set; }

        [JsonProperty("IAC")]
        public ValueAndUnit IAC { get; set; }

        [JsonProperty("IDC")]
        public ValueAndUnit IDC { get; set; }

        [JsonProperty("FAC")]
        public ValueAndUnit FAC { get; set; }


    }
}
