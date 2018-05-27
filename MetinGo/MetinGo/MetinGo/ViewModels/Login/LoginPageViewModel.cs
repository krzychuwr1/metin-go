using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Login;
using MetinGo.ApiModel.Registration;
using MetinGo.Infrastructure.Alerts;
using MetinGo.Infrastructure.Navigation;
using MetinGo.Infrastructure.RestApi;
using MetinGo.Infrastructure.Session;
using MetinGo.Models.User;
using MetinGo.Services;
using MetinGo.Views;
using MetinGo.Views.Popup;
using Xamarin.Forms;

namespace MetinGo.ViewModels.Login
{
    public class LoginPageViewModel : ObservableObject
    {
	    private readonly IApiClient _apiClient;
	    private readonly ISessionManager _sessionManager;
	    private readonly IAlertService _alertService;
	    private readonly INavigationManager _navigationManager;
        private readonly ILoginManager _loginManager;
        private string _username;
	    private string _password;

	    public string Username
	    {
		    get => _username;
		    set
		    {
			    _username = value;
			    OnPropertyChanged();
		    }
	    }

	    public string Password
	    {
		    get => _password;
		    set
		    {
			    _password = value;
			    OnPropertyChanged();
		    }
	    }

	    public LoginPageViewModel(IApiClient apiClient, ISessionManager sessionManager, IAlertService alertService, INavigationManager navigationManager, ILoginManager loginManager)
	    {
		    _apiClient = apiClient;
		    _sessionManager = sessionManager;
		    _alertService = alertService;
		    _navigationManager = navigationManager;
	        _loginManager = loginManager;
	        LoginCommand = new Command(Login);
	    }

	    private async void Login()
	    {
	        LoginResponse response = null;
	        //using (var indicator = new ActionActivityIndicator("Logging in..."))
	        //{
	        //    await indicator.Show();
	            response = await _apiClient.Post<LoginRequest, LoginResponse>(new LoginRequest { Username = Username, Password = Password }, Endpoints.Login);
            //}
		    if (response != null)
		    {
			    _sessionManager.User = new User {Id = response.UserId, Name = Username};
                await _loginManager.HandleLogin();
		    }
	    }

	    public ICommand LoginCommand { get; }
    }
}
