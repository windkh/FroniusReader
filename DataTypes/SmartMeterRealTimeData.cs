
namespace FroniusReader.DataTypes
{
    using Newtonsoft.Json;

    public class SmartMeterRealTimeData
    {
        [JsonProperty("Details")]
        public Details Details { get; set; }

        [JsonProperty("TimeStamp")]
        public int TimeStamp { get; set; }

        [JsonProperty("Visible")]
        public bool Visible { get; set; }

        [JsonProperty("Enable")]
        public bool Enable { get; set; }

        [JsonProperty("Current_AC_Phase_1")]
        public double CurrentACPhase1 { get; set; }

        [JsonProperty("Current_AC_Phase_2")]
        public double CurrentACPhase2 { get; set; }

        [JsonProperty("Current_AC_Phase_3")]
        public double CurrentACPhase3 { get; set; }

        [JsonProperty("EnergyReactive_VArAC_Sum_Consumed")]
        public int EnergyReactiveVArACSumConsumed { get; set; }

        [JsonProperty("EnergyReactive_VArAC_Sum_Produced")]
        public int EnergyReactiveVArACSumProduced { get; set; }

        [JsonProperty("EnergyReal_WAC_Minus_Absolute")]
        public int EnergyRealWACMinusAbsolute { get; set; }

        [JsonProperty("EnergyReal_WAC_Plus_Absolute")]
        public int EnergyRealWACPlusAbsolute { get; set; }

        [JsonProperty("EnergyReal_WAC_Sum_Consumed")]
        public int EnergyRealWACSumConsumed { get; set; }

        [JsonProperty("EnergyReal_WAC_Sum_Produced")]
        public int EnergyRealWACSumProduced { get; set; }

        [JsonProperty("Frequency_Phase_Average")]
        public double FrequencyPhaseAverage { get; set; }

        [JsonProperty("Meter_Location_Current")]
        public double MeterLocationCurrent { get; set; }

        [JsonProperty("PowerApparent_S_Phase_1")]
        public double PowerApparentSPhase1 { get; set; }

        [JsonProperty("PowerApparent_S_Phase_2")]
        public double PowerApparentSPhase2 { get; set; }

        [JsonProperty("PowerApparent_S_Phase_3")]
        public double PowerApparentSPhase3 { get; set; }

        [JsonProperty("PowerApparent_S_Sum")]
        public double PowerApparentSSum { get; set; }

        [JsonProperty("PowerFactor_Phase_1")]
        public double PowerFactorPhase1 { get; set; }

        [JsonProperty("PowerFactor_Phase_2")]
        public double PowerFactorPhase2 { get; set; }

        [JsonProperty("PowerFactor_Phase_3")]
        public double PowerFactorPhase3 { get; set; }

        [JsonProperty("PowerFactor_Sum")]
        public double PowerFactorSum { get; set; }

        [JsonProperty("PowerReactive_Q_Phase_1")]
        public double PowerReactiveQPhase1 { get; set; }

        [JsonProperty("PowerReactive_Q_Phase_2")]
        public double PowerReactiveQPhase2 { get; set; }

        [JsonProperty("PowerReactive_Q_Phase_3")]
        public double PowerReactiveQPhase3 { get; set; }

        [JsonProperty("PowerReactive_Q_Sum")]
        public double PowerReactiveQSum { get; set; }

        [JsonProperty("PowerReal_P_Phase_1")]
        public double PowerRealPPhase1 { get; set; }

        [JsonProperty("PowerReal_P_Phase_2")]
        public double PowerRealPPhase2 { get; set; }

        [JsonProperty("PowerReal_P_Phase_3")]
        public double PowerRealPPhase3 { get; set; }

        [JsonProperty("PowerReal_P_Sum")]
        public double PowerRealPSum { get; set; }

        [JsonProperty("Voltage_AC_PhaseToPhase_12")]
        public double VoltageACPhaseToPhase12 { get; set; }

        [JsonProperty("Voltage_AC_PhaseToPhase_23")]
        public double VoltageACPhaseToPhase23 { get; set; }

        [JsonProperty("Voltage_AC_PhaseToPhase_31")]
        public double VoltageACPhaseToPhase31 { get; set; }

        [JsonProperty("Voltage_AC_Phase_1")]
        public double VoltageACPhase1 { get; set; }

        [JsonProperty("Voltage_AC_Phase_2")]
        public double VoltageACPhase2 { get; set; }

        [JsonProperty("Voltage_AC_Phase_3")]
        public double VoltageACPhase3 { get; set; }
    }
}
