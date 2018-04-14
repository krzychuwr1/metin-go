using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MetinGo.Models;
using MetinGo.Models.Item;
using MetinGo.ViewModels;
using MetinGo.ViewModels.Item;

namespace MetinGo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
	    private readonly CharacterItemViewModel _innerViewModel;

	    public ItemDetailPage(CharacterItemViewModel innerViewModel)
	    {
	        _innerViewModel = innerViewModel;
	        InitializeComponent();
	    }

	    protected override void OnBindingContextChanged()
	    {
            base.OnBindingContextChanged();
	        if (BindingContext is CharacterItemDetailsViewModel viewModel)
	            viewModel.Item = _innerViewModel;
	    }
    }
}