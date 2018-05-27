using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MetinGo.Common;
using MetinGo.Infrastructure.Database;
using MetinGo.Infrastructure.Navigation;
using MetinGo.Infrastructure.Session;
using MetinGo.Models.Item;
using MetinGo.Views;
using MetinGo.Views.Login;
using Newtonsoft.Json;
using PCLAppConfig;
using Realms;
using Unity;
using Xamarin.Forms;

namespace MetinGo.Services
{
    public class AppStarter : IAppStarter
	{
	    private readonly ISessionManager _sessionManager;
	    private readonly INavigationManager _navigationManager;
	    private readonly ILoginManager _loginManager;

	    public AppStarter(ISessionManager sessionManager, INavigationManager navigationManager, ILoginManager loginManager)
	    {
		    _sessionManager = sessionManager;
		    _navigationManager = navigationManager;
	        _loginManager = loginManager;
	    }

	    public async Task Start()
        {
            Realm.GetInstance();
            SetupCulture();
            SetupAppConfig();
            if (_sessionManager.User?.Id != null && _sessionManager.Character?.Id != null)
            {
                await _navigationManager.SetCurrentPage(new NavigationPage(App.Current.Container.Resolve<MapPage>()));
            }
            if (_sessionManager.User?.Id != null)
            {
                await _navigationManager.SetCurrentPage<StartPage>();
                await _loginManager.HandleLogin();
            }
            else
            {
                await _navigationManager.SetCurrentPage<StartPage>();
            }
        }

        private static void SetupAppConfig()
        {
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            ConfigurationManager.Initialise(assembly.GetManifestResourceStream("MetinGo.app.config"));
        }

        private static void SetupCulture()
        {
            var currentCulture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = currentCulture;
            CultureInfo.CurrentUICulture = currentCulture;
            currentCulture.NumberFormat.NumberDecimalSeparator = ".";
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings { Culture = currentCulture };
        }
    }
}