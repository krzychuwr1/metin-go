using MetinGo.Common;
using MetinGo.Models;
using MetinGo.Models.Item;
using MetinGo.ViewModels;
using MetinGo.ViewModels.Equipment;
using MetinGo.ViewModels.Item;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MetinGo.Views.Equipment
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
	    private readonly ItemType _itemType;
	    ItemsViewModel ViewModel => BindingContext as ItemsViewModel;

        public ItemsPage()
        {
            InitializeComponent();
        }

	    public ItemsPage(ItemType itemType) : this()
	    {
	        _itemType = itemType;
	    }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is CharacterItemViewModel item))
                return;

            await Navigation.PushAsync(new ItemDetailPage(item));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.ItemType = _itemType;
            if (ViewModel.Items.Count == 0)
                ViewModel.LoadItemsCommand.Execute(null);
        }

	    protected override void OnBindingContextChanged()
	    {
	        //ViewModel.ItemType = _itemType;
	    }
    }
}