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
using MetinGo.Views;
using Xamarin.Forms;

namespace MetinGo.ViewModels.Login
{
    public class LoginPageViewModel : BaseViewModel
    {
	    private readonly IApiClient _apiClient;
	    private readonly ISessionManager _sessionManager;
	    private readonly IAlertService _alertService;
	    private readonly INavigationManager _navigationManager;
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

	    public LoginPageViewModel(IApiClient apiClient, ISessionManager sessionManager, IAlertService alertService, INavigationManager navigationManager)
	    {
		    _apiClient = apiClient;
		    _sessionManager = sessionManager;
		    _alertService = alertService;
		    _navigationManager = navigationManager;
		    LoginCommand = new Command(Login);
	    }

	    private async void Login()
	    {
		    var response= await _apiClient.Post<LoginRequest, LoginResponse>(new LoginRequest {Username = Username, Password = Password}, Endpoints.Login);
		    if (response != null)
		    {
			    _sessionManager.User = new User {Id = response.UserId, Name = Username};
			    await _alertService.DisplayAlert("Success", "Login Successful", "OK");
			    await _navigationManager.SetCurrentPage<MainPage>();
		    }
	    }

	    public ICommand LoginCommand { get; }
    }
}
