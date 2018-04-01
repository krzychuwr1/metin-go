using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Registration;
using MetinGo.Infrastructure.RestApi;
using MetinGo.Infrastructure.Session;
using Plugin.Geolocator;
using Unity;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace MetinGo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		private readonly Random _rand;
	    private ISessionManager _sessionManager;

	    public MapPage()
		{
			InitializeComponent();
			_rand = new Random();
		    _sessionManager = App.Current.Container.Resolve<ISessionManager>();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_sessionManager.Latitude ?? 0, _sessionManager.Longitude ?? 0), new Distance(500)));
            var position = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(5));
            _sessionManager.Latitude = position.Latitude;
            _sessionManager.Longitude = position.Longitude;
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), new Distance(500)));
            Map.MyLocationEnabled = true;
            await AddMonster();
            Map.InfoWindowClicked += Map_InfoWindowClicked;
        }

        private async Task AddMonster()
        {
            var position = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(10));
            var assembly = typeof(MapPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("MetinGo.Images.Dziki_Pies.png");
            var icon = BitmapDescriptorFactory.FromStream(stream);

            var monsterPosition = new Position(position.Latitude + (_rand.NextDouble() - 0.5) / 100, position.Longitude + (_rand.NextDouble() - 0.5) / 100);
            Map.Pins.Add(
                new Pin
                {
                    Label = "Dziki pies",
                    Position = monsterPosition,
                    IsVisible = true,
                    Icon = icon
                });


        }

        private async void Map_InfoWindowClicked(object sender, InfoWindowClickedEventArgs e)
        {
            var attack = await App.Current.MainPage.DisplayAlert("Dziki pies", "Czy chcesz zaatakować dzikiego psa?", "TAK", "NIE");
            if (attack)
            {
                await App.Current.MainPage.DisplayAlert("Zwycięstwo!", "Otrzymałeś 5 pkt doświadczenia", "OK");
                Map.Pins.Remove(e.Pin);
                await AddMonster();
            }
        }
    }
}