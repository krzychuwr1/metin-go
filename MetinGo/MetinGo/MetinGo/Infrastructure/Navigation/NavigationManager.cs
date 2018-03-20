using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MetinGo.Infrastructure.Navigation
{
    public class NavigationManager : INavigationManager
    {
	    public async Task SetCurrentPage(Page page) => App.Current.MainPage = page;

	    public async Task SetCurrentPage<TPage>() where TPage : Page, new() => App.Current.MainPage = new TPage();
    }
}
