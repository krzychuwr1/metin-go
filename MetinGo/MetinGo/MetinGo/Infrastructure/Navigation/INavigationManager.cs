using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MetinGo.Infrastructure.Navigation
{
    public interface INavigationManager
    {
        INavigation Navigation { get; }

        Page CurrentPage { get; }

        Task SetCurrentPage(Page page);

        Task SetCurrentPage<TPage>() where TPage : Page, new();

        void SetNewDetailPage(Page page);

        Task PushAsync(Page page);
    }
}
