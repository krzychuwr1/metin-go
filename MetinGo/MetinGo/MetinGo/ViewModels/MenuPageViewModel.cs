using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MetinGo.Infrastructure.Navigation;
using MetinGo.Views;
using Xamarin.Forms;

namespace MetinGo.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        private readonly INavigationManager _navigationManager;

        public MenuPageViewModel(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            OpenMapCommand = new Command(OpenMap);
            OpenCharactersCommand = new Command(OpenCharacters);
        }

        private void OpenCharacters()
        {
            _navigationManager.SetNewDetailPage(new ItemsPage());
        }

        private void OpenMap()
        {
            _navigationManager.SetNewDetailPage(new MapPage());
        }

        public ICommand OpenMapCommand { get; }

        public ICommand OpenCharactersCommand { get; }
    }
}
