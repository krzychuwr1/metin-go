using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MetinGo.Views.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EntryPopup : PopupPage
	{
	    public Func<string, Task> OkAction { get; set; }
	    public Action CancelAction { get; set; }

	    public EntryPopup(string text)
	    {
	        InitializeComponent ();
	        Label.Text = text;
	    }

	    private void Cancel_OnClicked(object sender, EventArgs e)
	    {
	        CancelAction();
	    }

	    private async void Ok_OnClicked(object sender, EventArgs e)
	    {
	        await OkAction(Entry.Text);
	    }
	}
}