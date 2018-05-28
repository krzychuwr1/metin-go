using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Character;
using MetinGo.ApiModel.Item;
using MetinGo.Infrastructure.Navigation;
using MetinGo.Infrastructure.RestApi;
using MetinGo.Infrastructure.Session;
using MetinGo.Services.Item;
using MetinGo.Views;
using MetinGo.Views.Login;
using MetinGo.Views.Popup;
using Realms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Unity;
using Xamarin.Forms;

namespace MetinGo.Services
{
    public interface ILoginManager
    {
        Task HandleLogin();
    }

    public class LoginManager : ILoginManager
    {
        private readonly ISessionManager _sessionManager;
        private readonly INavigationManager _navigationManager;
        private readonly IApiClient _apiClient;
        private readonly IItemService _itemService;

        public LoginManager(ISessionManager sessionManager, INavigationManager navigationManager, IApiClient apiClient, IItemService itemService)
        {
            _sessionManager = sessionManager;
            _navigationManager = navigationManager;
            _apiClient = apiClient;
            _itemService = itemService;
        }

        public async Task HandleLogin()
        {
            await _itemService.UpdateItems();
            if (_sessionManager.Character?.Id != null)
            {
                await _navigationManager.SetCurrentPage(new NavigationPage(App.Current.Container.Resolve<MapPage>()));
            }
            else
            {
                var characters = await _apiClient.Get<List<Character>>(Endpoints.Character);
                if (!characters.Any())
                {
                    await _navigationManager.SetCurrentPage<StartPage>();
                    await _apiClient.Post(new CreateCharacterRequest { Name = _sessionManager.User.Name }, Endpoints.Character);
                    await _navigationManager.CurrentPage.DisplayAlert("Character created",
                        "Character created", "OK");

                    characters = await _apiClient.Get<List<Character>>(Endpoints.Character);
                    var character = characters[0];
                    _sessionManager.Character = new Models.Character.Character { Id = character.Id, Name = character.Name, Level = character.Level, Experience = character.Experience, BaseAttack = character.BaseAttack, BaseDefence = character.BaseDefence, BaseMaxHP = character.BaseMaxHP, StatPoints = character.StatPoints };
                    await _navigationManager.SetCurrentPage(new NavigationPage(App.Current.Container.Resolve<MapPage>()));

                    //EntryPopup entryPopup = new EntryPopup("Provide character name");
                    //entryPopup.OkAction = async result =>
                    //{                        await PopupNavigation.RemovePageAsync(entryPopup);

                    //};
                    //Xamarin.Forms.Device.BeginInvokeOnMainThread(() => PopupNavigation.PushAsync(entryPopup)); 
                }
                else
                {
                    var character = characters[0];
                    _sessionManager.Character = new Models.Character.Character { Id = character.Id, Name = character.Name, Level = character.Level, Experience = character.Experience, BaseAttack = character.BaseAttack, BaseDefence = character.BaseDefence, BaseMaxHP = character.BaseMaxHP, StatPoints = character.StatPoints };
                    await _navigationManager.SetCurrentPage(new NavigationPage(App.Current.Container.Resolve<MapPage>()));
                }
            }
        }
    }
}
