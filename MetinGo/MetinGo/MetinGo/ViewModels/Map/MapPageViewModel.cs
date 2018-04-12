using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MetinGo.ApiModel.Monster;
using MetinGo.Infrastructure.Navigation;
using MetinGo.Infrastructure.Session;
using MetinGo.Models.Character;
using MetinGo.Views;
using MetinGo.Views.Character;
using MetinGo.Views.Equipment;
using MetinGo.Views.Login;
using Xamarin.Forms;

namespace MetinGo.ViewModels.Map
{
    public class MapPageViewModel : ObservableObject
    {
        private readonly INavigationManager _navigationManager;
        private readonly ISessionManager _sessionManager;

        public MapPageViewModel(INavigationManager navigationManager, ISessionManager sessionManager)
        {
            _navigationManager = navigationManager;
            _sessionManager = sessionManager;
            OpenCharactersCommand = new Command(OpenCharacters);
            LogoutCommand = new Command(Logout);
            OpenCharacterStatsCommand = new Command(OpenCharacterStats);
            OpenEquipmentCommand = new Command(OpenEquipment);
        }

        private void OpenEquipment() => _navigationManager.PushAsync(new EquipmentPage());

        private void OpenCharacterStats() => _navigationManager.PushAsync(new CharacterStatsPage());

        public int Level => _sessionManager.Character.Level;
        public int Experience => _sessionManager.Character.Experience;
        public string CharacterName => _sessionManager.Character.Name;

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

        public ICommand OpenCharacterStatsCommand { get; }

        public ICommand OpenEquipmentCommand { get; }

        public void RefreshCharacter()
        {
            OnPropertyChanged(nameof(Level));
            OnPropertyChanged(nameof(CharacterName));
            OnPropertyChanged(nameof(Experience));
        }
    }
}
