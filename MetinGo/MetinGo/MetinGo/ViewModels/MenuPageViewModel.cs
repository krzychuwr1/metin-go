using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MetinGo.Infrastructure.Navigation;
using MetinGo.Views;
using MetinGo.Views.Login;
using Xamarin.Forms;

namespace MetinGo.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        private readonly INavigationManager _navigationManager;

        public MenuPageViewModel(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            OpenCharactersCommand = new Command(OpenCharacters);
            LogoutCommand = new Command(Logout);
        }

        private void Logout()
        {
            App.Current.Properties.Clear();
            App.Current.MainPage = new StartPage();
        }

        private void OpenCharacters()
        {
            _navigationManager.SetNewDetailPage(new ItemsPage());
        }

        public ICommand OpenCharactersCommand { get; }
        public ICommand LogoutCommand { get; }
    }
}
