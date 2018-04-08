using System;

using MetinGo.Models;

namespace MetinGo.ViewModels
{
    public class ItemDetailViewModel : ObservableObject
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Item = item;
        }
    }
}
