
namespace FroniusReader.ViewModel
{
    using System;

    using System.Threading.Tasks;
    using System.Windows.Input;
    using DataTypes;
    using Model;
    using Prism.Commands;
    using Prism.Mvvm;

    public class ArchiveViewModel : BindableBase, IArchiveViewModel
    {
        #region Fields

        private readonly IFroniusModel _froniusModel;
        private readonly DelegateCommand _getArchiveDataCommand;
        private ArchiveData _archiveData;

        private double _lastHours = 12;
        private bool _getChannelEnergyRealWACSumProduced;
        private bool _getChannelEnergyRealWACSumConsumed;
        private bool _getChannelTemperaturePowerstage;
        private bool _getChannelCurrentDCString1;
        private bool _getChannelCurrentDCString2;
        private bool _getChannelPowerRealSum = true;
        private bool _getChannelVoltageDCString1;
        private bool _getChannelVoltageDCString2;
        private bool _getChannelPowerDCString1;
        private bool _getChannelPowerDCString2;

        private bool _updating;
        #endregion

        #region Constructor

        public ArchiveViewModel(IFroniusModel froniusModel)
        {
            _froniusModel = froniusModel;
            _getArchiveDataCommand = new DelegateCommand(async () => await ExecuteGetArchiveDataCommandAsync(), CanExecuteGetArchiveDataCommand);
        }

        #endregion

        #region Properties

        public ICommand GetArchiveDataCommand
        {
            get
            {
                return _getArchiveDataCommand;
            }
        }

        public ArchiveData ArchiveData
        {
            get
            {
                return _archiveData;
            }
            private set
            {
                if (_archiveData != value)
                {
                    _archiveData = value;
                    RaisePropertyChanged(nameof(ArchiveData));
                }
            }
        }

        public bool Updating
        {
            get
            {
                return _updating;
            }

            set
            {
                if (_updating != value)
                {
                    _updating = value;
                    OnUpdatingChanged();
                    RaisePropertyChanged(nameof(Updating));
                }
            }
        }

        public double LastHours
        {
            get
            {
                return _lastHours;
            }
            
            set
            {
                if (_lastHours != value)
                {
                    _lastHours = value;
                    RaisePropertyChanged(nameof(LastHours));
                }
            }
        }

        public bool GetChannelEnergyRealWACSumProduced
        {
            get
            {
                return _getChannelEnergyRealWACSumProduced;
            }

            set
            {
                if (_getChannelEnergyRealWACSumProduced != value)
                {
                    _getChannelEnergyRealWACSumProduced = value;
                    RaisePropertyChanged(nameof(GetChannelEnergyRealWACSumProduced));
                }
            }
        }

        public bool GetChannelEnergyRealWACSumConsumed
        {
            get
            {
                return _getChannelEnergyRealWACSumConsumed;
            }

            set
            {
                if (_getChannelEnergyRealWACSumConsumed != value)
                {
                    _getChannelEnergyRealWACSumConsumed = value;
                    RaisePropertyChanged(nameof(GetChannelEnergyRealWACSumConsumed));
                }
            }
        }

        public bool GetChannelPowerRealSum
        {
            get
            {
                return _getChannelPowerRealSum;
            }

            set
            {
                if (_getChannelPowerRealSum != value)
                {
                    _getChannelPowerRealSum = value;
                    RaisePropertyChanged(nameof(GetChannelPowerRealSum));
                }
            }
        }

        public bool GetChannelTemperaturePowerstage
        {
            get
            {
                return _getChannelTemperaturePowerstage;
            }

            set
            {
                if (_getChannelTemperaturePowerstage != value)
                {
                    _getChannelTemperaturePowerstage = value;
                    RaisePropertyChanged(nameof(GetChannelTemperaturePowerstage));
                }
            }
        }

        public bool GetChannelCurrentDCString1
        {
            get
            {
                return _getChannelCurrentDCString1;
            }

            set
            {
                if (_getChannelCurrentDCString1 != value)
                {
                    _getChannelCurrentDCString1 = value;
                    RaisePropertyChanged(nameof(GetChannelCurrentDCString1));
                }
            }
        }

        public bool GetChannelCurrentDCString2
        {
            get
            {
                return _getChannelCurrentDCString2;
            }

            set
            {
                if (_getChannelCurrentDCString2 != value)
                {
                    _getChannelCurrentDCString2 = value;
                    RaisePropertyChanged(nameof(GetChannelCurrentDCString2));
                }
            }
        }

        public bool GetChannelVoltageDCString1
        {
            get
            {
                return _getChannelVoltageDCString1;
            }

            set
            {
                if (_getChannelVoltageDCString1 != value)
                {
                    _getChannelVoltageDCString1 = value;
                    RaisePropertyChanged(nameof(GetChannelVoltageDCString1));
                }
            }
        }

        public bool GetChannelVoltageDCString2
        {
            get
            {
                return _getChannelVoltageDCString2;
            }

            set
            {
                if (_getChannelVoltageDCString2 != value)
                {
                    _getChannelVoltageDCString2 = value;
                    RaisePropertyChanged(nameof(GetChannelVoltageDCString2));
                }
            }
        }

        public bool GetChannelPowerDCString1
        {
            get
            {
                return _getChannelPowerDCString1;
            }

            set
            {
                if (_getChannelPowerDCString1 != value)
                {
                    _getChannelPowerDCString1 = value;
                    RaisePropertyChanged(nameof(GetChannelPowerDCString1));
                }
            }
        }

        public bool GetChannelPowerDCString2
        {
            get
            {
                return _getChannelPowerDCString2;
            }

            set
            {
                if (_getChannelPowerDCString2 != value)
                {
                    _getChannelPowerDCString2 = value;
                    RaisePropertyChanged(nameof(GetChannelPowerDCString2));
                }
            }
        }

        #endregion

        #region Private 

        private async Task ExecuteGetArchiveDataCommandAsync()
        {
            Updating = true;

            DateTime startDate = DateTime.Now - TimeSpan.FromHours(LastHours);
            DateTime endDate = DateTime.Now;

            ArchiveData = await _froniusModel.GetArchiveDataAsync(
                startDate,
                endDate,
                GetChannelCurrentDCString1,
                GetChannelCurrentDCString2,
                GetChannelVoltageDCString1,
                GetChannelVoltageDCString2,
                GetChannelPowerDCString1,
                GetChannelPowerDCString2,
                GetChannelEnergyRealWACSumProduced,
                GetChannelEnergyRealWACSumConsumed,
                GetChannelTemperaturePowerstage,
                GetChannelPowerRealSum);

            Updating = false;
        }

        private bool CanExecuteGetArchiveDataCommand()
        {
            return !Updating;
        }

        private void OnUpdatingChanged()
        {
            _getArchiveDataCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}
