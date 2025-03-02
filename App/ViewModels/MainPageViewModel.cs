using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Devices.Sensors;
using System.Diagnostics;
using WeatherApp.Logic.Interfaces;
using WeatherApp.Logic.Models;

namespace App.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IWeatherService m_weatherService;
        private readonly IConnectivity m_connectivity;
        private readonly IGeolocation m_geoLocation;
        [ObservableProperty] private string latitude = string.Empty;
        [ObservableProperty] private string longitude = string.Empty;
        [ObservableProperty] private WeatherModel weatherData;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool isBusy = false;

        //Used for buttons enabled property
        public bool IsNotBusy => !IsBusy;
        public MainPageViewModel(IWeatherService weatherService, IConnectivity connectivity, IGeolocation geolocation)
        {
            m_weatherService = weatherService;
            m_connectivity = connectivity;
            m_geoLocation = geolocation;
        }
        [RelayCommand]
        public async Task GetCurrentLocation()
        {
            if (IsBusy) return;

            if (m_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Oh no", "Looks like you have no internet, please check your connection", "Ok");
                return;
            }
            IsBusy = true;
            try
            {
                var location = await m_geoLocation.GetLastKnownLocationAsync();
                if (location is null)
                {
                    location = await m_geoLocation.GetLocationAsync(
                        new GeolocationRequest
                        {
                            DesiredAccuracy = GeolocationAccuracy.Medium,
                            Timeout = TimeSpan.FromSeconds(30)
                        });
                    //If location is still null then return
                    if (location is null)
                        return;
                }
                Latitude = Math.Round(location.Latitude, 3).ToString();
                Longitude = Math.Round(location.Longitude, 3).ToString();
                await CallWeatherService(Latitude, Longitude);

            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }
            finally
            {
                Latitude = string.Empty;
                Longitude = string.Empty;
                IsBusy = false;
            }
        }
        [RelayCommand]
        private async Task GetWeatherData()
        {
            if (IsBusy)
                return;

            if(string.IsNullOrWhiteSpace(Latitude) || string.IsNullOrWhiteSpace(Longitude))
            {
                await Shell.Current.DisplayAlert("Missing required data", "Input fields cannot be empty", "Ok");
                return;
            }
            if(m_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Oh no", "Looks like you have no internet, please check your connection", "Ok");
                return;
            }
            IsBusy = true;
            try
            {
                await CallWeatherService(Latitude, Longitude);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }
            finally
            {
                Latitude = string.Empty;
                Longitude = string.Empty;
                IsBusy = false;
            }
        }


        private async Task CallWeatherService(string lat, string lon)
        {
            var result = await m_weatherService.GetWeatherDataAsync(lat, lon);
            if (result.IsSuccess)
            {
                WeatherData = result.WeatherData;
                return;
            }
            await Shell.Current.DisplayAlert("Failed to get data", "Could not get weather data", "Ok");
        }
    }
}
