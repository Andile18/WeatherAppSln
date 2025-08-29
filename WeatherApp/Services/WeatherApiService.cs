using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json; 
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherApiService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "d0fb080483e20b38f546f3709468468e";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public WeatherApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherResponse> GetWeatherAsync(double latitude, double longitude)
        {
            try
            {
                string url = $"{BaseUrl}?lat={latitude}&lon={longitude}&units=metric&appid={ApiKey}";

                var json= await _httpClient.GetStringAsync(url);

                var weatherData = JsonConvert.DeserializeObject<WeatherResponse>(json);

                return weatherData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching weather: {ex.Message}");
                return null;
            }
        }
    }
}
