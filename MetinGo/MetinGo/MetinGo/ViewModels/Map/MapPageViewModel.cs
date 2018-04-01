using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MetinGo.Infrastructure.Navigation;
using MetinGo.Views;
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
        }

        private void OpenCharacters()
        {
            _navigationManager.PushAsync(new ItemsPage());
        }

        public ICommand OpenCharactersCommand { get; }
    }
}
