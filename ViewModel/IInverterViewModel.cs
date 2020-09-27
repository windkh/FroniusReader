
namespace FroniusReader.Model
{
    using DataTypes;
    using System.Windows.Input;

    public interface IInverterViewModel
    {
        ICommand GetRealtimeDataCommand { get; }

        InverterRealTimeData RealTimeData { get; }
    }
}
