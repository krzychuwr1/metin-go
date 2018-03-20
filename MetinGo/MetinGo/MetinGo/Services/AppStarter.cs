using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MetinGo.Infrastructure.Navigation;
using MetinGo.Infrastructure.Session;
using MetinGo.Views;
using MetinGo.Views.Login;

namespace MetinGo.Services
{
    public class AppStarter : IAppStarter
	{
	    private readonly ISessionManager _sessionManager;
	    private readonly INavigationManager _navigationManager;

	    public AppStarter(ISessionManager sessionManager, INavigationManager navigationManager)
	    {
		    _sessionManager = sessionManager;
		    _navigationManager = navigationManager;
	    }

	    public async Task Start()
	    {
		    if (_sessionManager.User?.Id != null)
			    await _navigationManager.SetCurrentPage<MainPage>();
		    else
			    await _navigationManager.SetCurrentPage<StartPage>();
	    }
    }
}
