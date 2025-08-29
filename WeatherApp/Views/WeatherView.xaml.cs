using WeatherApp.ViewModels;

namespace WeatherApp.Views
{
    public partial class WeatherView : ContentPage
    {
        private WeatherViewModel _viewModel;

        public WeatherView()
        {
            InitializeComponent();
          
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
