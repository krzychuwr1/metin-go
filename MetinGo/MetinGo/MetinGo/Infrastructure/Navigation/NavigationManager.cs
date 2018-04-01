using System.Threading.Tasks;
using Xamarin.Forms;

namespace MetinGo.Infrastructure.Navigation
{
    public class NavigationManager : INavigationManager
    {
        public INavigation Navigation => CurrentPage.Navigation;

        public Page CurrentPage => App.Current.MainPage is MasterDetailPage masterDetail
            ? masterDetail.Detail
            : App.Current.MainPage;

        public async Task SetCurrentPage(Page page)
        {
            App.Current.MainPage = page;    
        }

        public async Task SetCurrentPage<TPage>() where TPage : Page, new()
        {
            App.Current.MainPage = new TPage();
        }

        public void SetNewDetailPage(Page page)
        {
            var navigationPage = new NavigationPage(page);
            ((MasterDetailPage)App.Current.MainPage).Detail = navigationPage;
            ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
        }

        public async Task PushAsync(Page page)
        {
            await Navigation.PushAsync(page);
        }
    }
}