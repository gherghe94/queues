using Queues.Desktop.ViewModels;
using System.Windows;

namespace Queues.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            _mainViewModel = new MainViewModel();
            DataContext = _mainViewModel;

            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _mainViewModel.Dispose();
        }
    }
}
