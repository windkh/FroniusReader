
namespace FroniusReader.ViewModel
{
    using System;
    using System.Linq;
    using OxyPlot.Series;
    using DataTypes;
    using Model;
    using OxyPlot;
    using OxyPlot.Axes;
    using Prism.Mvvm;
    using System.ComponentModel;
    using System.Windows.Threading;
    using System.Windows.Input;

    public class MainViewModel : BindableBase
    {
        #region Constante

        private const int TIMER_SECONDS = 20;

        #endregion

        #region Fields

        private DispatcherTimer _timer;

        private readonly OxyColor[] _seriesColors =
        {
            OxyColors.Red,
            OxyColors.Orange,
            OxyColors.Blue,
            OxyColors.LightBlue,
            OxyColors.Green,
            OxyColors.LightGreen,
            OxyColors.DarkGray,
            OxyColors.Gray,
            OxyColors.Pink
        };

        private DateTimeAxis _timeAxis;
        private string _address;

        #endregion

        #region Constructor

        public MainViewModel(IFroniusModel froniusModel)
        {
            _froniusModel = froniusModel;
            ApiVersion = _froniusModel.Connect("fronius.fritz.box");
            _address = _froniusModel.Address;

            ArchiveViewModel = new ArchiveViewModel(froniusModel);
            ArchiveViewModel.PropertyChanged += ArchiveViewModelPropertyChangedEventHandler;

            SmartMeterViewModel = new SmartMeterViewModel(froniusModel);
            SmartMeterViewModel.PropertyChanged += SmartMeterViewModelPropertyChangedEventHandler;

            InverterViewModel = new InverterViewModel(froniusModel);
            InverterViewModel.PropertyChanged += InverterViewModelPropertyChangedEventHandler;

            _timer = new DispatcherTimer(TimeSpan.FromSeconds(TIMER_SECONDS),DispatcherPriority.Normal, OnTimerElapsed, Dispatcher.CurrentDispatcher);
            PlotModel = new PlotModel();
            InitPlotModel();

            // trigger first update
            OnTimerElapsed(null, EventArgs.Empty);
        }

        private void OnTimerElapsed(object sender, EventArgs e)
        {
            SmartMeterViewModel.GetRealtimeDataCommand.Execute(null);
            InverterViewModel.GetRealtimeDataCommand.Execute(null);
        }

        private void SmartMeterViewModelPropertyChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Updating")
            {
                UpdateMouseCursor();
            }
        }

        private void InverterViewModelPropertyChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Updating")
            {
                UpdateMouseCursor();
            }
        }

        private void ArchiveViewModelPropertyChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Updating")
            {
                UpdateMouseCursor();
            }

            if (e.PropertyName == "ArchiveData")
            {
                ArchiveData archiveData = ArchiveViewModel.ArchiveData;
                UpdatePlot(archiveData);
            }
        }

        private void UpdateMouseCursor()
        {
            if (ArchiveViewModel.Updating || SmartMeterViewModel.Updating || InverterViewModel.Updating)
            {
                Mouse.OverrideCursor = Cursors.Wait;
            }
            else
            {
                Mouse.OverrideCursor = null;
            }
        }

        #endregion

        #region Properties

        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                if (value != _address)
                {
                    _address = value;
                    OnAddressChanged();
                    RaisePropertyChanged();
                }
            }
        }

        public ApiVersion ApiVersion
        {
            get
            {
                return _apiVersion;
            }

            set
            {
                if (value != _apiVersion)
                {
                    _apiVersion = value;
                    RaisePropertyChanged();
                }
            }
        }

        public PlotModel PlotModel
        {
            get;
        }

        private IFroniusModel _froniusModel;
        private ApiVersion _apiVersion;

        public ArchiveViewModel ArchiveViewModel
        {
            get;
        }

        public SmartMeterViewModel SmartMeterViewModel
        {
            get;
        }

        public InverterViewModel InverterViewModel
        {
            get;
        }

        #endregion

        #region Private 


        private void OnAddressChanged()
        {
            ApiVersion = _froniusModel.Connect(Address);
            if (ApiVersion != null)
            {
                // ok
            }
            else
            {
                //...
            }
        }

        private void InitPlotModel()
        {
            PlotModel.Title = "Archive Data";
            PlotModel.IsLegendVisible = true;
            PlotModel.Series.Clear();
            PlotModel.Axes.Clear();

            _timeAxis = new DateTimeAxis
            {
                Title = "Zeit",
                Position = AxisPosition.Bottom,
                IsZoomEnabled = true,
                IsPanEnabled = true,
                MajorGridlineStyle = LineStyle.Dash,
                MajorGridlineColor = OxyColors.Gray,
                //IntervalType = DateTimeIntervalType.Minutes,
                StringFormat = "(dd:MM) HH:mm:ss"
            };

            PlotModel.Axes.Add(_timeAxis);
        }

        private void UpdatePlot(ArchiveData archiveData)
        {
            InitPlotModel();

            if (archiveData != null)
            {
                int i = 0;
                foreach (Channel channel in archiveData.Channels)
                {
                    LineSeries series = new LineSeries
                    {
                        Color = _seriesColors[i++],
                        LineStyle = LineStyle.Solid,
                        Title = channel.Name + " [" + channel.Unit + "]",
                        YAxisKey = channel.Unit
                    };
                    series.Points.AddRange(channel.Points);

                    PlotModel.Series.Add(series);

                    if (!PlotModel.Axes.Any(item => item.Key == channel.Unit))
                    {
                        LinearAxis signalAxis = new LinearAxis
                        {
                            Title = channel.Unit,
                            Key = channel.Unit,
                            Position = AxisPosition.Left,
                            Minimum = 0,
                            AbsoluteMinimum = 0,
                            //IsZoomEnabled = true,
                            //IsPanEnabled = true,
                            MajorGridlineStyle = LineStyle.Dash,
                            MajorGridlineColor = OxyColors.Gray,
                            AxisDistance = 30,
                            PositionTier = i,
                        };
                        PlotModel.Axes.Add(signalAxis);
                    }
                }

                PlotModel.InvalidatePlot(true);
            }
        }
        #endregion
    }
}
