using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.Common;
using MetinGo.Infrastructure.Database;
using MetinGo.Infrastructure.Session;
using MetinGo.Models;
using MetinGo.Models.Item;
using MetinGo.Services;
using MetinGo.Services.Item;
using MetinGo.ViewModels.Item;
using MetinGo.Views;
using MetinGo.Views.Popup;
using Xamarin.Forms;

namespace MetinGo.ViewModels.Equipment
{
    public class ItemsViewModel : ObservableObject
    {
        private readonly ISessionManager _sessionManager;
        private readonly IItemService _itemService;
        public ObservableCollection<CharacterItemViewModel> Items { get; set; }
        public Command LoadItemsCommand { get; }
        private bool _initialized = false;
        public Command EquipItemCommand { get; }
        public ItemsViewModel(ISessionManager sessionManager, IItemService itemService)
        {
            _sessionManager = sessionManager;
            _itemService = itemService;
            Items = new ObservableCollection<CharacterItemViewModel>();
            LoadItemsCommand = new Command(async () => await InitialLoadItems());
            EquipItemCommand = new Command<CharacterItemViewModel>(async item => await EquipItem(item));
        }

        private async Task EquipItem(CharacterItemViewModel item)
        {
            var characterItemGuid = Guid.Parse(item.CharacterItem.Id);
            await _itemService.EquipItem(characterItemGuid);
            await LoadItems();
        }

        public async Task InitialLoadItems()
        {
            if (_initialized)
                return;
            _initialized = true;
            await LoadItems();
        }

        private async Task LoadItems()
        {
            //using (var indicator = new ActionActivityIndicator("Loading items.."))
            //{
                //await indicator.Show();
                try
                {
                    await _itemService.UpdateCharacterItems();
                    Items.Clear();
                    var items = await _itemService.GetCharacterItems();
                    foreach (var item in items.Where(i => i.CharacterItem.Item.ItemType == ItemType).OrderByDescending(i => i.CharacterItem.IsEquipped).ThenByDescending(i => i.CharacterItem.Item.Rarity).ThenByDescending(i => i.CharacterItem.Level))
                    {
                        Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            //}
        }

        public ItemType ItemType { get; set; }
    }
}