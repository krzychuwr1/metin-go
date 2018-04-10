using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MetinGo.ApiModel;
using MetinGo.ApiModel.CharacterStats;
using MetinGo.Infrastructure.RestApi;
using MetinGo.Infrastructure.Session;
using MetinGo.Views.Popup;
using Xamarin.Forms;

namespace MetinGo.ViewModels.Character
{
    public class CharactersStatsPageViewModel : ObservableObject
    {
        private readonly ISessionManager _sessionManager;
        private readonly IApiClient _apiClient;
        private int _availablePoints;
        private int _hp;
        private int _attack;
        private int _defence;
        private int _statPoints;
        private int _spentHpPoints;
        private int _spentAttackPoints;
        private int _spentDefencePoints;

        public CharactersStatsPageViewModel(ISessionManager sessionManager, IApiClient apiClient)
        {
            _sessionManager = sessionManager;
            _apiClient = apiClient;
            ChangeAttackPointCommand = new Command<int>(ChangeAttackPoint);
            ChangeDefencePointCommand = new Command<int>(ChangeDefencePoint);
            ChangeHpPointCommand = new Command<int>(ChangeHpPoint);
            InitCharacter();
            SaveChangesCommand = new Command(SaveChanges);
        }

        private void InitCharacter()
        {
            HP = _sessionManager.Character.BaseMaxHP;
            Attack = _sessionManager.Character.BaseAttack;
            Defence = _sessionManager.Character.BaseDefence;
            AvailablePoints = _sessionManager.Character.StatPoints;
        }

        private async void SaveChanges()
        {
            using (var indicator = new ActionActivityIndicator("Saving..."))
            {
                await indicator.Show();
                var character = await _apiClient.Post<IncreaseStatsRequest, ApiModel.Character.Character>(new IncreaseStatsRequest()
                {
                    Attack = SpentAttackPoints,
                    Defence = SpentDefencePoints,
                    HP = SpentHpPoints
                }, Endpoints.CharacterStats);
                var localCharacter = _sessionManager.Character;
                localCharacter.BaseAttack = character.BaseAttack;
                localCharacter.BaseDefence = character.BaseDefence;
                localCharacter.StatPoints = character.StatPoints;
                localCharacter.BaseMaxHP = character.BaseMaxHP;
                SpentDefencePoints = 0;
                SpentAttackPoints = 0;
                SpentHpPoints = 0;
                _sessionManager.Character = localCharacter;
                InitCharacter();
            }
        }

        public int HP
        {
            get => _hp;
            set
            {
                _hp = value;
                OnPropertyChanged();
            }
        }

        public int Attack
        {
            get => _attack;
            set
            {
                _attack = value;
                OnPropertyChanged();
            }
        }

        public int Defence
        {
            get => _defence;
            set
            {
                _defence = value;
                OnPropertyChanged();
            }
        }

        public int StatPoints
        {
            get => _statPoints;
            set
            {
                _statPoints = value;
                OnPropertyChanged();
            }
        }

        private void ChangeHpPoint(int amount)
        {
            HP += amount * 5;
            SpentHpPoints += amount;
            AvailablePoints -= amount;
        }

        private void ChangeDefencePoint(int amount)
        {
            Defence += amount;
            SpentDefencePoints += amount;
            AvailablePoints -= amount;
        }

        private void ChangeAttackPoint(int amount)
        {
            Attack += amount;
            SpentAttackPoints += amount;
            AvailablePoints -= amount;
        }

        public ICommand ChangeAttackPointCommand { get; }
        public ICommand ChangeDefencePointCommand { get; }
        public ICommand ChangeHpPointCommand { get; }

        public int SpentHpPoints
        {
            get => _spentHpPoints;
            set
            {
                _spentHpPoints = value;
                OnPropertyChanged();
            }
        }

        public int SpentAttackPoints
        {
            get => _spentAttackPoints;
            set
            {
                _spentAttackPoints = value;
                OnPropertyChanged();
            }
        }

        public int SpentDefencePoints
        {
            get => _spentDefencePoints;
            set
            {
                _spentDefencePoints = value;
                OnPropertyChanged();
            }
        }

        public Models.Character.Character Character => _sessionManager.Character;

        public int AvailablePoints
        {
            get => _availablePoints;
            set
            {
                _availablePoints = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveChangesCommand { get; }
    }
}
