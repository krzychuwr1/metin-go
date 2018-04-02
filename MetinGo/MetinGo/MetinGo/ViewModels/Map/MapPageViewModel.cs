using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MetinGo.Infrastructure.Navigation;
using MetinGo.Views;
using MetinGo.Views.Login;
using Xamarin.Forms;

namespace MetinGo.ViewModels.Map
{
    public class MapPageViewModel : BaseViewModel
    {
        private readonly INavigationManager _navigationManager;

        public MapPageViewModel(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            OpenCharactersCommand = new Command(OpenCharacters);
            LogoutCommand = new Command(Logout);
        }

        private async void Logout()
        {
            App.Current.Properties.Clear();
            await _navigationManager.SetCurrentPage<StartPage>();
        }

        private void OpenCharacters()
        {
            _navigationManager.PushAsync(new ItemsPage());
        }

        public ICommand OpenCharactersCommand { get; }

        public ICommand LogoutCommand { get; }
    }
}
