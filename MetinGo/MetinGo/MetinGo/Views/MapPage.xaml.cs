using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Character;
using MetinGo.ApiModel.Fight;
using MetinGo.ApiModel.Monster;
using MetinGo.ApiModel.Registration;
using MetinGo.Common;
using MetinGo.Infrastructure.Permission;
using MetinGo.Infrastructure.RestApi;
using MetinGo.Infrastructure.Session;
using MetinGo.ViewModels.Map;
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

        private MapPageViewModel ViewModel => BindingContext as MapPageViewModel;

	    public MapPage(ISessionManager sessionManager, IApiClient apiClient, IPermissionManager permissionManager)
		{
		    _sessionManager = sessionManager;
		    _apiClient = apiClient;
		    _permissionManager = permissionManager;
		    InitializeComponent();
			_rand = new Random();
		    Map.InfoWindowClicked += Map_InfoWindowClicked;
        }

        protected override async void OnAppearing()
        {
            await _permissionManager.CheckAndAskIfNeeded("Location Permission is needed to play", "Cannot play without permission", Permission.Location);
            base.OnAppearing();
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_sessionManager.Latitude ?? 0, _sessionManager.Longitude ?? 0), new Distance(100)));
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

                var players = await _apiClient.Get<List<ApiModel.Character.Character>>(Endpoints.NearbyCharacters);
                foreach (var player in players.Where(p => p.Id != _sessionManager.Character.Id))
                {
                    AddPlayer(player);
                }
                await Task.Delay(15000);
            }
        }

	    private void AddPlayer(ApiModel.Character.Character player)
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
	        var wildDogIcon = BitmapDescriptorFactory.FromStream(assembly.GetManifestResourceStream("MetinGo.Images.Dziki_Pies.png"));
            var hungryWolfIcon = BitmapDescriptorFactory.FromStream(assembly.GetManifestResourceStream("MetinGo.Images.Glodny_Wilk.png"));

	        var monsterPosition = new Position(monster.Latitude, monster.Longitude);
            if (monster.MonsterType == MonsterType.WildDog)
	        {
	            Map.Pins.Add(
	                new Pin
	                {
	                    Tag = monster,
	                    Label = $"Dziki pies lv:{monster.Level}",
	                    Position = monsterPosition,
	                    IsVisible = true,
	                    Icon = wildDogIcon
	                });
            }
            else
            {
                Map.Pins.Add(
                    new Pin
                    {
                        Tag = monster,
                        Label = $"Głodny wilk lv:{monster.Level}",
                        Position = monsterPosition,
                        IsVisible = true,
                        Icon = hungryWolfIcon
                    });
            }
        }

        private async void Map_InfoWindowClicked(object sender, InfoWindowClickedEventArgs e)
        {
            if (e.Pin.Tag is Monster monster)
            {
                var attack = await App.Current.MainPage.DisplayAlert("Dziki pies", "Czy chcesz zaatakować dzikiego psa?", "TAK", "NIE");
                if (attack)
                {
                    var response = await _apiClient.Post<FightRequest, FightResponse>(new FightRequest() {MonsterId = monster.Id}, Endpoints.Fight);
                    if (response.PlayerWon)
                    {
                        await App.Current.MainPage.DisplayAlert("Zwycięstwo!", $"Otrzymałeś {response.Experience} pkt doświadczenia", "OK");
                        if (response.LevelAfterFight > _sessionManager.Character.Level)
                        {
                            await App.Current.MainPage.DisplayAlert("Level up!", $"Osiągnąłeś poziom {response.LevelAfterFight}", "OK");
                        }
                        Map.Pins.Remove(e.Pin);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Porażka!", $"Utraciłeś {-response.Experience} pkt doświadczenia", "OK");
                    }
                    SaveFightResult(response);
                }
            }
        }

        private void SaveFightResult(FightResponse response)
        {
            var character = _sessionManager.Character;
            if (response.LevelAfterFight > character.Level)
                character.StatPoints += 4;
            character.Level = response.LevelAfterFight;
            character.Experience = character.Experience + response.Experience;
            _sessionManager.Character = character;
            ViewModel.RefreshCharacter();
        }
    }
}