using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Task2App.Commands;
using Task2App.Models;

namespace Task2App.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private LightingDevice? _selectedDevice;

        public ObservableCollection<LightingDevice> Devices { get; }

        public MainViewModel(IEnumerable<LightingDevice> devices)
        {
            Devices = new ObservableCollection<LightingDevice>(devices);

            foreach (var device in Devices)
            {
                device.Broken += Device_Broken;
            }

            TurnOnCommand = new RelayCommand(_ => TurnOn(), _ => SelectedDevice != null && !SelectedDevice.IsBroken);
            TurnOffCommand = new RelayCommand(_ => TurnOff(), _ => SelectedDevice != null && !SelectedDevice.IsBroken);
            TogglePlugCommand = new RelayCommand(_ => TogglePlug(), _ => SelectedDevice is IPluggable);
            RepairCommand = new RelayCommand(_ => Repair(), _ => SelectedDevice != null && SelectedDevice.IsBroken);
        }

        public LightingDevice? SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                _selectedDevice = value;
                OnPropertyChanged();
                RefreshDeviceProperties();
            }
        }

        public string SelectedDeviceStatus => SelectedDevice?.StatusText ?? "Не выбран";
        public string IsBrokenText => SelectedDevice != null ? (SelectedDevice.IsBroken ? "СЛОМАН" : "Исправен") : "";
        public string PlugStatusText
        {
            get
            {
                if (SelectedDevice is IPluggable pluggable)
                    return pluggable.IsPluggedIn ? "Подключено к сети" : "Отключено от сети";
                return "Работает от батарей / Напрямую";
            }
        }

        public RelayCommand TurnOnCommand { get; }
        public RelayCommand TurnOffCommand { get; }
        public RelayCommand TogglePlugCommand { get; }
        public RelayCommand RepairCommand { get; }

        private void TurnOn()
        {
            SelectedDevice?.TurnOn();
            RefreshDeviceProperties();
        }

        private void TurnOff()
        {
            SelectedDevice?.TurnOff();
            RefreshDeviceProperties();
        }

        private void TogglePlug()
        {
            if (SelectedDevice is IPluggable pluggable)
            {
                pluggable.TogglePlug();
                RefreshDeviceProperties();
            }
        }

        private void Repair()
        {
            SelectedDevice?.Repair();
            RefreshDeviceProperties();
        }

        private void Device_Broken(object? sender, EventArgs e)
        {
            MessageBox.Show($"Внимание! Прибор {((LightingDevice)sender!).Name} сломался!", "Поломка", MessageBoxButton.OK, MessageBoxImage.Warning);
            RefreshDeviceProperties();
        }

        private void RefreshDeviceProperties()
        {
            OnPropertyChanged(nameof(SelectedDeviceStatus));
            OnPropertyChanged(nameof(IsBrokenText));
            OnPropertyChanged(nameof(PlugStatusText));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}