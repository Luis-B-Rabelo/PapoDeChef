using PapoDeChef.MVVM.ViewModels;
using PapoDeChef.MVVM.Views.Windows;
using System.Windows;

namespace PapoDeChef
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow _mainWindow = new MainWindow();

            DevWindow _devWindow = new DevWindow();

            _mainWindow.DataContext = new MainViewModel();

            _devWindow.DataContext = new DevViewModel();

            _mainWindow.Show();

            _devWindow.Show();

            base.OnStartup(e);
        }
    }

}
