using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private readonly WeatherApiService _weatherService;

        private string _city;
        private double _temperature;
        private string _description;
        private string _icon;

        public string City
        {
            get => _city;
            set { _city = value; OnPropertyChanged(); }
        }

        public double Temperature
        {
            get => _temperature;
            set { _temperature = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public string Icon
        {
            get => _icon;
            set { _icon = value; OnPropertyChanged(); }
        }

        public ICommand CurrentLocationCommand { get; }

        public WeatherViewModel()
        {
            _weatherService = new WeatherApiService();
            CurrentLocationCommand = new Command(async () => await LoadWeatherAsync());
        }

        public async Task LoadWeatherAsync()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    var weather = await _weatherService.GetWeatherAsync(location.Latitude, location.Longitude);

                    if (weather != null)
                    {
                        City = weather.Name;
                        Temperature = weather.Main.Temp;
                        Description = weather.weather.Description;
                        Icon = weather.weather.Icon;
                    }
                }
            }
            catch
            {
                City = "Location not found";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
