using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MetinGo.Infrastructure.Session;
using MetinGo.Models;
using MetinGo.Services;
using MetinGo.Views;
using Xamarin.Forms;

namespace MetinGo.ViewModels.Equipment
{
    public class ItemsViewModel : ObservableObject
    {
        private readonly ISessionManager _sessionManager;
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {

            try
            {
                Items.Clear();
                var items = new List<Item>{new Item(){Id = "1", Description = "Description", Text = "Text"}};
                foreach (var item in items)
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
}