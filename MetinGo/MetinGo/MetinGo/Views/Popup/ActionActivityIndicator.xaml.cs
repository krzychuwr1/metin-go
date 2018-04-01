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
        private static int _indicatorCounter = 0;

        public ActionActivityIndicator(string message)
        {
            InitializeComponent();
            Message.Text = message;
            CloseWhenBackgroundIsClicked = false;
        }
        
        public async Task Show()
        {
            System.Threading.Interlocked.Increment(ref _indicatorCounter);
            if (_indicatorCounter == 1)
                await PopupNavigation.PushAsync(this);
        }
        
        public async void Dispose()
        {
            System.Threading.Interlocked.Decrement(ref _indicatorCounter);
            if (_indicatorCounter == 0 && PopupNavigation.PopupStack.Count > 0)
                await PopupNavigation.PopAsync(true);
        }

    }
}