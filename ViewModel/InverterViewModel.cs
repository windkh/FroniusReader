
namespace FroniusReader.ViewModel
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using DataTypes;
    using Model;
    using Prism.Commands;
    using Prism.Mvvm;
 
    public class InverterViewModel : BindableBase, IInverterViewModel
    {
        #region Fields

        private readonly IFroniusModel _froniusModel;
        private readonly DelegateCommand _getRealtimeDataCommand;
        private InverterRealTimeData _realTimeData;
        private bool _updating;

        #endregion

        #region Constructor

        public InverterViewModel(IFroniusModel froniusModel)
        {
            _froniusModel = froniusModel;
            _getRealtimeDataCommand = new DelegateCommand(async () => await ExecuteGetRealtimeDataCommandAsync(), CanExecuteGetRealtimeDataCommand);
        }

        #endregion

        #region Properties

        public ICommand GetRealtimeDataCommand
        {
            get
            {
                return _getRealtimeDataCommand;
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

        public InverterRealTimeData RealTimeData
        {
            get
            {
                return _realTimeData;
            }
            private set
            {
                if (_realTimeData != value)
                {
                    _realTimeData = value;
                    RaisePropertyChanged(nameof(RealTimeData));
                }
            }
        }
        #endregion

        #region Private 

        private async Task ExecuteGetRealtimeDataCommandAsync()
        {
            Updating = true;
            RealTimeData = await _froniusModel.GetInverterRealtimeDataAsync();
            Updating = false;
        }

        private bool CanExecuteGetRealtimeDataCommand()
        {
            return !Updating;
        }

        private void OnUpdatingChanged()
        {
            _getRealtimeDataCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}
