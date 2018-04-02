using System;
using System.Collections.Generic;
using System.Text;
using MetinGo.Infrastructure.Alerts;
using MetinGo.Infrastructure.Navigation;
using MetinGo.Infrastructure.Permission;
using MetinGo.Infrastructure.RestApi;
using MetinGo.Infrastructure.Session;
using MetinGo.Services;
using Unity;

namespace MetinGo.AppStart
{
    public static class ContainerInitialization
    {
	    public static IUnityContainer InitializeContainer()
	    {
		    var container = new UnityContainer();
		    container.RegisterType<ISessionManager, SessionManager>();
		    container.RegisterType<IApiClient, ApiClient>();
		    container.RegisterType<IAlertService, AlertService>();
		    container.RegisterType<INavigationManager, NavigationManager>();
		    container.RegisterType<IAppStarter, AppStarter>();
	        container.RegisterType<ILoginManager, LoginManager>();
	        container.RegisterType<IRestApiHeadersService, RestApiHeadersService>();
	        container.RegisterType<IPermissionManager, PermissionManager>();
		    return container;
	    }
    }
}
