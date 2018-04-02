using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Character;
using MetinGo.ApiModel.Monster;
using MetinGo.ApiModel.Registration;
using MetinGo.Infrastructure.Permission;
using MetinGo.Infrastructure.RestApi;
using MetinGo.Infrastructure.Session;
using Plugin.Geolocator;
using Plugin.Permissions.Abstractions;
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
	    private readonly ISessionManager _sessionManager;
	    private readonly IApiClient _apiClient;
	    private readonly IPermissionManager _permissionManager;

	    public MapPage(ISessionManager sessionManager, IApiClient apiClient, IPermissionManager permissionManager)
		{
		    _sessionManager = sessionManager;
		    _apiClient = apiClient;
		    _permissionManager = permissionManager;
		    InitializeComponent();
			_rand = new Random();
		}

        protected override async void OnAppearing()
        {
            await _permissionManager.CheckAndAskIfNeeded("Location Permission is needed to play", "Cannot play without permission", Permission.Location);
            base.OnAppearing();
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_sessionManager.Latitude ?? 0, _sessionManager.Longitude ?? 0), new Distance(100)));
            Map.InfoWindowClicked += Map_InfoWindowClicked;
            Map.MyLocationEnabled = true;

            while (this.IsVisible)
            {
                var position = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(300));
                _sessionManager.Latitude = position.Latitude;
                _sessionManager.Longitude = position.Longitude;
                Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), new Distance(100)));
                var monsters = await _apiClient.Get<List<Monster>>(Endpoints.Monster);
                Map.Pins.Clear();
                foreach (var monster in monsters)
                {
                    AddMonster(monster);
                }

                var players = await _apiClient.Get<List<Character>>(Endpoints.NearbyCharacters);
                foreach (var player in players.Where(p => p.Id != _sessionManager.Character.Id))
                {
                    AddPlayer(player);
                }
                await Task.Delay(15000);
            }
        }

	    private void AddPlayer(Character player)
	    {
	        var assembly = typeof(MapPage).GetTypeInfo().Assembly;
	        var stream = assembly.GetManifestResourceStream("MetinGo.Images.ninja.png");
	        var icon = BitmapDescriptorFactory.FromStream(stream);

	        var monsterPosition = new Position(player.Latitude, player.Longitude);
	        Map.Pins.Add(
	            new Pin
	            {
	                Tag = player,
	                Label = $"{player.Name} lv:{player.Level}",
	                Position = monsterPosition,
	                IsVisible = true,
	                Icon = icon
	            });
        }

	    private void AddMonster(Monster monster)
	    {
	        var assembly = typeof(MapPage).GetTypeInfo().Assembly;
	        var stream = assembly.GetManifestResourceStream("MetinGo.Images.Dziki_Pies.png");
	        var icon = BitmapDescriptorFactory.FromStream(stream);

	        var monsterPosition = new Position(monster.Latitude, monster.Longitude);
	        Map.Pins.Add(
	            new Pin
	            {
                    Tag = monster,
	                Label = $"Dziki pies lv:{monster.Level}",
	                Position = monsterPosition,
	                IsVisible = true,
	                Icon = icon
	            });
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
            if (e.Pin.Tag is Monster)
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
}