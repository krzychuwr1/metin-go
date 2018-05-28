using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MetinGo.Views.Popup
{

    public partial class ActionActivityIndicator : PopupPage, IDisposable
    {
        public ActionActivityIndicator(string message)
        {
            InitializeComponent();
            Message.Text = message;
            CloseWhenBackgroundIsClicked = false;
        }
        
        public async Task Show()
        {
                await PopupNavigation.PushAsync(this);
        }
        
        public async void Dispose()
        {
            await PopupNavigation.PopAsync(true);
        }

    }
}