using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MetinGo.Infrastructure.Navigation;
using MetinGo.Infrastructure.Session;
using MetinGo.Views;
using MetinGo.Views.Login;
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
    }
}
