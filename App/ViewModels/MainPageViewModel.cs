using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        [ObservableProperty] private string cityName = string.Empty;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasData), nameof(IconSource), nameof(WeatherDescription))]
        private CurrentWeatherModel currentWeatherData;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool isBusy = false;

        //Used for buttons enabled property
        public bool IsNotBusy => !IsBusy;

        public bool HasData => CurrentWeatherData is not null;

        public string IconSource => CurrentWeatherData is not null ? CurrentWeatherData.Weather.First().IconImage : string.Empty;

        public string WeatherDescription => CurrentWeatherData is not null ? CurrentWeatherData.Weather.First().ToString() : string.Empty;

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
                await CallWeatherService(Math.Round(location.Latitude, 3).ToString(), Math.Round(location.Longitude, 3).ToString());

            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        private async Task GetWeatherData()
        {
            if (IsBusy)
                return;

            if(string.IsNullOrWhiteSpace(CityName))
            {
                await Shell.Current.DisplayAlert("Missing required data", "Input field cannot be empty", "Ok");
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
                await CallWeatherService(CityName);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }
            finally
            {
                CityName = string.Empty;
                IsBusy = false;
            }
        }


        private async Task CallWeatherService(string lat, string lon)
        {
            var result = await m_weatherService.GetWeatherDataAsync(lat, lon);
            if (result.IsSuccess)
            {
                CurrentWeatherData = result.CurrentWeatherData;
                return;
            }
            await Shell.Current.DisplayAlert("Failed to get data", result.ErrorMessage, "Ok");
        }
        private async Task CallWeatherService(string cityName)
        {
            var result = await m_weatherService.GetWeatherDataByNameAsync(cityName);
            if (result.IsSuccess)
            {
                CurrentWeatherData = result.CurrentWeatherData;
                return;
            }
            await Shell.Current.DisplayAlert("Failed to get data", result.ErrorMessage, "Ok");
        }
    }
}
