using System;

using MetinGo.Models;
using MetinGo.Models.Item;

namespace MetinGo.ViewModels
{
    public class ItemDetailViewModel : ObservableObject
    {
        public Models.Item.Item Item { get; set; }
        public ItemDetailViewModel(Models.Item.Item item = null)
        {
            Item = item;
        }
    }
}
