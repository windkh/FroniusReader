
namespace FroniusReader.Model
{
    using System.Windows.Input;
    using DataTypes;

    public interface IArchiveViewModel
    {
        ICommand GetArchiveDataCommand { get; }
        bool Updating { get; }

        ArchiveData ArchiveData { get; }

        double LastHours { get; }
        bool GetChannelEnergyRealWACSumProduced { get; }
        bool GetChannelEnergyRealWACSumConsumed { get; }
        bool GetChannelPowerRealSum { get; }
        bool GetChannelTemperaturePowerstage { get; }
        bool GetChannelCurrentDCString1 { get; }
        bool GetChannelCurrentDCString2 { get; }
        bool GetChannelVoltageDCString1 { get; }
        bool GetChannelVoltageDCString2 { get; }
        bool GetChannelPowerDCString1 { get; }
        bool GetChannelPowerDCString2 { get; }
    }
}
