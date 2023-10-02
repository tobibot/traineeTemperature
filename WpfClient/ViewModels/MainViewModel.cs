using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Win32;

namespace WpfClient.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Timer timer;
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri(GetBaseUrl()),
        };


        private double currentTemperature;
        public double CurrentTemperature
        {
            get => this.currentTemperature;
            set
            {
                this.currentTemperature = value;
                this.OnPropertyChanged();
            }
        }

        private DateTime lastRefresh;
        public DateTime LastRefresh
        {
            get => this.lastRefresh;
            set
            {
                this.lastRefresh = value;
                this.OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            GetData();
            this.timer = new Timer();
            timer.Interval = 60_000;
            timer.Elapsed += (object? sender, ElapsedEventArgs e) => GetData();
            timer.Start();
        }

        private void GetData()
        {
            using HttpResponseMessage response = sharedClient.GetAsync("currenttemperature").GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            this.CurrentTemperature = double.Parse(jsonResponse.Replace(".", ","));
            this.LastRefresh = DateTime.Now;
        }

        private static string GetBaseUrl()
        {
            // ToDo: if debug
            return "https://currenttemperatureapi.azurewebsites.net";
        }

        #region INotifyPropertyCanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}
