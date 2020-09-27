
namespace FroniusReader
{
    using FroniusReader.Model;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IFroniusModel model = new FroniusModel();
            DataContext = new ViewModel.MainViewModel(model);
        }
    }
}
