
namespace FroniusReader.Model
{
    using System;
    using System.Threading.Tasks;
    using DataTypes;

    public interface IFroniusModel
    {
        ApiVersion Connect(string address);

        string Address { get; }

        Task<SmartMeterRealTimeData> GetSmartMeterRealtimeDataAsync();

        Task<InverterRealTimeData> GetInverterRealtimeDataAsync();

        Task<ArchiveData> GetArchiveDataAsync(
            DateTime startDate,
            DateTime endDate,
            bool getChannelCurrentDCString1,
            bool getChannelCurrentDCString2,
            bool getChannelVoltageDCString1,
            bool getChannelVoltageDCString2,
            bool getChannelPowerDCString1,
            bool getChannelPowerDCString2,
            bool getChannelEnergyRealWACSumProduced,
            bool getChannelEnergyRealWACSumConsumed,
            bool getChannelTemperaturePowerstage,
            bool getChannelPowerRealSum
        );
    }
}
