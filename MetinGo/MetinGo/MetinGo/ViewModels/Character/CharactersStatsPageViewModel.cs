using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MetinGo.ApiModel;
using MetinGo.ApiModel.CharacterStats;
using MetinGo.Common;
using MetinGo.Infrastructure.RestApi;
using MetinGo.Infrastructure.Session;
using MetinGo.Views.Popup;
using Realms;
using Xamarin.Forms;

namespace MetinGo.ViewModels.Character
{
    public class CharactersStatsPageViewModel : ObservableObject
    {
        private readonly ISessionManager _sessionManager;
        private readonly IApiClient _apiClient;
        private readonly IItemWithLevelStatsCalculator _itemWithLevelStatsCalculator;
        private int _availablePoints;
        private int _hp;
        private int _attack;
        private int _defence;
        private int _statPoints;
        private int _spentHpPoints;
        private int _spentAttackPoints;
        private int _spentDefencePoints;
        private int _itemsAttack;
        private int _itemsDefence;
        private int _itemsMaxHP;
        private int _hpWithItems;
        private int _attackWithItems;
        private int _defenceWithItems;

        public CharactersStatsPageViewModel(ISessionManager sessionManager, IApiClient apiClient, IItemWithLevelStatsCalculator itemWithLevelStatsCalculator)
        {
            _sessionManager = sessionManager;
            _apiClient = apiClient;
            _itemWithLevelStatsCalculator = itemWithLevelStatsCalculator;
            ChangeAttackPointCommand = new Command<int>(ChangeAttackPoint);
            ChangeDefencePointCommand = new Command<int>(ChangeDefencePoint);
            ChangeHpPointCommand = new Command<int>(ChangeHpPoint);
            SaveChangesCommand = new Command(SaveChanges);
            var db = Realm.GetInstance();
            var characterItems = db.All<Models.Item.CharacterItem>().Where(c => c.IsEquipped).ToList();
            var stats = characterItems.Select(i => _itemWithLevelStatsCalculator.Calculate(i)).ToList();
            _itemsAttack = stats.Sum(s => s.Attack);
            _itemsDefence = stats.Sum(s => s.Defence);
            _itemsMaxHP = stats.Sum(s => s.MaxHp);
            InitCharacter();
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
            //using (var indicator = new ActionActivityIndicator("Saving..."))
            //{
            //    await indicator.Show();
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
            //}
        }

        public int HP
        {
            get => _hp;
            set
            {
                _hp = value;
                HPWithItems = _hp + _itemsMaxHP;
                OnPropertyChanged();
            }
        }

        public int Attack
        {
            get => _attack;
            set
            {
                _attack = value;
                AttackWithItems = _attack + _itemsAttack;
                OnPropertyChanged();
            }
        }

        public int Defence
        {
            get => _defence;
            set
            {
                _defence = value;
                DefenceWithItems = _defence + _defenceWithItems;
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

        public int HPWithItems
        {
            get => _hpWithItems;
            set
            {
                _hpWithItems = value;
                OnPropertyChanged();
            }
        }

        public int AttackWithItems
        {
            get => _attackWithItems;
            set
            {
                _attackWithItems = value;
                OnPropertyChanged();
            }
        }

        public int DefenceWithItems
        {
            get => _defenceWithItems;
            set
            {
                _defenceWithItems = value;
                OnPropertyChanged();
            }
        }
    }
}
