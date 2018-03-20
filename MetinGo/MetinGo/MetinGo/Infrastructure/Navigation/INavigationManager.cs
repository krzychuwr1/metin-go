using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MetinGo.Infrastructure.Navigation
{
    public interface INavigationManager
    {
		Task SetCurrentPage(Page page);
	    Task SetCurrentPage<TPage>() where TPage : Page, new();
    }
}
