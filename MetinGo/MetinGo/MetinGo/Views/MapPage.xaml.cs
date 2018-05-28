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
using MetinGo.Models.Item;
using MetinGo.ViewModels.Map;
using Plugin.Geolocator;
using Plugin.Permissions.Abstractions;
using Realms;
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
	    private List<Item> _items;
	    private Realm _realm;
	    private readonly MonsterNameResolver _monsterNameResolver;

	    private MapPageViewModel ViewModel => BindingContext as MapPageViewModel;

	    public MapPage(ISessionManager sessionManager, IApiClient apiClient, IPermissionManager permissionManager)
		{
		    _sessionManager = sessionManager;
		    _apiClient = apiClient;
		    _permissionManager = permissionManager;
		    InitializeComponent();
			_rand = new Random();
		    _monsterNameResolver = new MonsterNameResolver();
		    Ma.InfoWindowClicked += Map_InfoWindowClicked;
        }

        protected override async void OnAppearing()
        {
            await _permissionManager.CheckAndAskIfNeeded("Location Permission is needed to play", "Cannot play without permission", Permission.Location);
            base.OnAppearing();
            Ma.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_sessionManager.Latitude ?? 0, _sessionManager.Longitude ?? 0), new Distance(100)));

            if (_items == null)
            {
                _realm = Realm.GetInstance();
                _items = _realm.All<Item>().ToList();
            }

            while (this.IsVisible)
            {
                var position = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(300));
                _sessionManager.Latitude = position.Latitude;
                _sessionManager.Longitude = position.Longitude;
                Ma.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), new Distance(100)));
                var monsters = await _apiClient.Get<List<Monster>>(Endpoints.Monster);
                Ma.Pins.Clear();
                Ma.Circles.Clear();
                foreach (var monster in monsters)
                {
                    AddMonster(monster);
                }
                AddMe(new Position(position.Latitude, position.Longitude));

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
	        var stream = assembly.GetManifestResourceStream("MetinGo.Images.ninjaSmall.png");
	        var icon = BitmapDescriptorFactory.FromStream(stream);

	        var monsterPosition = new Position(player.Latitude, player.Longitude);
	        Ma.Pins.Add(
	            new Pin
	            {
	                Tag = player,
	                Label = $"{player.Name} lv:{player.Level}",
	                Position = monsterPosition,
	                IsVisible = true,
	                Icon = icon
	            });
        }

	    private void AddMe(Position position)
	    {
	        var assembly = typeof(MapPage).GetTypeInfo().Assembly;
	        var stream = assembly.GetManifestResourceStream("MetinGo.Images.ninjaSmall.png");
	        var icon = BitmapDescriptorFactory.FromStream(stream);

	        Ma.Pins.Add(
	            new Pin
	            {
	                Label = $"{_sessionManager.Character.Name} lv:{_sessionManager.Character.Level} (me)",
	                Position = position,
	                IsVisible = true,
	                Icon = icon
	            });
	        Ma.Circles.Add(
	            new Circle()
	            {
                    Center = position,
                    Radius = Distance.FromKilometers(0.1),
                    FillColor = Color.FromRgba(0, 0, 255, 32),
                    ZIndex = 0
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
	            Ma.Pins.Add(
	                new Pin
	                {
	                    Tag = monster,
	                    Label = $"Wild dog lv:{monster.Level}",
	                    Position = monsterPosition,
	                    IsVisible = true,
	                    Icon = wildDogIcon
	                });
            }
            else
            {
                Ma.Pins.Add(
                    new Pin
                    {
                        Tag = monster,
                        Label = $"Hungry wolf lv:{monster.Level}",
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
                var latitudeDifferenceKm = (monster.Latitude - _sessionManager.Latitude) * 111;
                var longitudeDifferenceKm = (monster.Longitude - _sessionManager.Longitude) * 68;
                var distance = Math.Sqrt((double) (latitudeDifferenceKm*latitudeDifferenceKm + longitudeDifferenceKm * longitudeDifferenceKm));

                if (distance > 0.1)
                {
                    await App.Current.MainPage.DisplayAlert("Too far", $"The enemy is too far", "OK");
                    return;
                }

                var attack = await App.Current.MainPage.DisplayAlert(monster.MonsterType.ToString(), $"Do you want to attack {_monsterNameResolver.GetMonsterName(monster.MonsterType)}?", "YES", "NO");
                if (attack)
                {
                    var response = await _apiClient.Post<FightRequest, FightResponse>(new FightRequest() {MonsterId = monster.Id}, Endpoints.Fight);
                    if (response.PlayerWon)
                    {
                        await App.Current.MainPage.DisplayAlert("Victory!", $"You have obtained {response.Experience} experience points", "OK");
                        if (response.LevelAfterFight > _sessionManager.Character.Level)
                        {
                            await App.Current.MainPage.DisplayAlert("Level up!", $"You are now level {response.LevelAfterFight}", "OK");
                        }

                        if (response.Loot != null)
                        {
                            foreach (var characterItem in response.Loot)
                            {
                                var item = _items.Single(i => i.Id == characterItem.ItemId);
                                await App.Current.MainPage.DisplayAlert("New item!",
                                    $"You have obtainted {item.Name} level {characterItem.Level}", "OK");
                                _realm.Write(() => _realm.Add(new CharacterItem{CharacterId = _sessionManager.Character.Id.ToString(), Id = characterItem.Id.ToString(), Item = item, Level = characterItem.Level}));
                            }
                        }
                        Ma.Pins.Remove(e.Pin);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Defeat!", $"You have lost {-response.Experience} experience points", "OK");
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