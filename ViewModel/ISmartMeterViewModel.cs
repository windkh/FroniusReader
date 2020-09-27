
namespace FroniusReader.Model
{
    using DataTypes;
    using System.Windows.Input;

    public interface ISmartMeterViewModel
    {
        ICommand GetRealtimeDataCommand { get; }

        SmartMeterRealTimeData RealTimeData { get; }
    }
}
