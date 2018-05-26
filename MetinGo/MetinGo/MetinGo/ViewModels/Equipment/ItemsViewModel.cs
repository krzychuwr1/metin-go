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
        public Command LoadItemsCommand { get; set; }
        private bool _initialized = false;
        public ItemsViewModel(ISessionManager sessionManager, IItemService itemService)
        {
            _sessionManager = sessionManager;
            _itemService = itemService;
            Items = new ObservableCollection<CharacterItemViewModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (_initialized)
                return;
            _initialized = true;
            using (var indicator = new ActionActivityIndicator("Loading items.."))
            {
                await indicator.Show();
                try
                {
                    await _itemService.UpdateCharacterItems();
                    Items.Clear();
                    var items = await _itemService.GetCharacterItems();
                    foreach (var item in items.Where(i => i.CharacterItem.Item.ItemType == ItemType))
                    {
                        Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        public ItemType ItemType { get; set; }
    }
}