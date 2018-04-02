using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Character;
using MetinGo.Infrastructure.Navigation;
using MetinGo.Infrastructure.RestApi;
using MetinGo.Infrastructure.Session;
using MetinGo.Views;
using MetinGo.Views.Login;
using MetinGo.Views.Popup;
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

        public LoginManager(ISessionManager sessionManager, INavigationManager navigationManager, IApiClient apiClient)
        {
            _sessionManager = sessionManager;
            _navigationManager = navigationManager;
            _apiClient = apiClient;
        }

        public async Task HandleLogin()
        {
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
                    EntryPopup entryPopup = new EntryPopup("Provide character name");
                    entryPopup.OkAction = async result =>
                    {
                        await _apiClient.Post(new CreateCharacterRequest {Name = result}, Endpoints.Character);
                        await _navigationManager.CurrentPage.DisplayAlert("Character created",
                            "Character created", "OK");

                        characters = await _apiClient.Get<List<Character>>(Endpoints.Character);
                        _sessionManager.Character = new Models.Character.Character() { Id = characters[0].Id, Name = characters[0].Name};
                        await _navigationManager.SetCurrentPage(new NavigationPage(App.Current.Container.Resolve<MapPage>()));
                        await PopupNavigation.RemovePageAsync(entryPopup, true);
                    };
                    await PopupNavigation.PushAsync(entryPopup);
                }
                else
                {
                    _sessionManager.Character = new Models.Character.Character() { Id = characters[0].Id, Name = characters[0].Name };
                    await _navigationManager.SetCurrentPage(new NavigationPage(App.Current.Container.Resolve<MapPage>()));
                }
            }
        }
    }
}
