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
        private readonly IConnectivity m_connectivity; //Platform connection interface
        private readonly IGeolocation m_geoLocation; //Platform GPS interface
        [ObservableProperty] private string cityNameInput = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasCurrentData))]
        private CurrentWeatherModel? currentWeatherData;


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasForecastData))]
        private ForecastDisplayModel? forecastWeatherData;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool isBusy = false;

        //Used for buttons enabled property
        public bool IsNotBusy => !IsBusy;

        public bool HasForecastData => ForecastWeatherData is not null;
        public bool HasCurrentData => CurrentWeatherData is not null;

        public bool HasInternet { get; private set; }



        public MainPageViewModel(IWeatherService weatherService, IConnectivity connectivity, IGeolocation geolocation)
        {
            m_weatherService = weatherService;
            m_connectivity = connectivity;
            m_geoLocation = geolocation;
            m_connectivity.ConnectivityChanged += NetworkConnectivityChanged;
            HasInternet = m_connectivity.NetworkAccess == NetworkAccess.Internet;
        }
        private void NetworkConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            HasInternet = e.NetworkAccess == NetworkAccess.Internet;
        }

        /// <summary>
        /// Relay command that when a user clicks a collectionview item it expands the content
        /// </summary>
        /// <param name="entry"></param>
        [RelayCommand]
        public static void ToggleExpand(ForecastEntry entry) => entry.IsExpanded = !entry.IsExpanded;

        /// <summary>
        /// Gets weather data based on geolocation using the platform geolocation interface
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task GetCurrentLocation()
        {
            if (IsBusy) return;

            if (!HasInternet)
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
                    {
                        await Shell.Current.DisplayAlert("Oh no", "Failed to get your geolocation data", "Ok");
                        return;
                    }
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
        /// <summary>
        /// Gets weather data based on the city name provided in the input field
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task GetWeatherData()
        {
            if (IsBusy)
                return;

            if (string.IsNullOrWhiteSpace(CityNameInput))
            {
                await Shell.Current.DisplayAlert("Missing required data", "Input field cannot be empty", "Ok");
                return;
            }
            if (!HasInternet)
            {
                await Shell.Current.DisplayAlert("Oh no", "Looks like you have no internet, please check your connection", "Ok");
                return;

            }
            IsBusy = true;
            try
            {
                await CallWeatherService(CityNameInput);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }
            finally
            {
                CityNameInput = string.Empty;
                IsBusy = false;
            }
        }

        /// <summary>
        /// Calls the weather service to get current and forecasts API data
        /// </summary>
        /// <param name="locationParams"></param>
        /// <returns></returns>
        private async Task CallWeatherService(params string[] locationParams)
        {
            var result = await m_weatherService.GetWeatherDataAsync(locationParams);
            CurrentWeatherData = result.CurrentWeatherData; //nullable
            ForecastWeatherData = result.ForecastWeatherData; //nullable
            if (!result.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Failed to get data", result.ErrorMessage, "Ok");
            }
        }
    }
}
