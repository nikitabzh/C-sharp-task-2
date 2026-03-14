using System.Collections.Generic;
using System.Windows;
using Task2App.Models;
using Task2App.ViewModels;

namespace Task2App
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var devices = new List<LightingDevice>
            {
                new Lantern("Походный фонарь", 0.05),
                new DeskLamp("Рабочая лампа", 0.05),
                new Chandelier("Зал (Люстра 3 режима)", 0.20),
                new FloorLamp("Торшер в спальне", 0.20)
            };

            var viewModel = new MainViewModel(devices);
            var mainWindow = new MainWindow
            {
                DataContext = viewModel
            };

            mainWindow.Show();
        }
    }
}