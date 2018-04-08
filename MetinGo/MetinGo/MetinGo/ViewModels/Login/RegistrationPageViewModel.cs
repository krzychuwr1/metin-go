using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Registration;
using MetinGo.Infrastructure.Alerts;
using MetinGo.Infrastructure.RestApi;
using Xamarin.Forms;

namespace MetinGo.ViewModels.Login
{
    public class RegistrationPageViewModel : ObservableObject
    {
	    private readonly IApiClient _apiClient;
	    private readonly IAlertService _alertService;
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

	    public RegistrationPageViewModel(IApiClient apiClient, IAlertService alertService)
	    {
		    _apiClient = apiClient;
		    _alertService = alertService;
		    RegisterCommand = new Command(Register);
	    }

	    private async void Register()
	    {
		    await _apiClient.Post(new RegistrationRequest {Username = Username, Password = Password}, Endpoints.Registration);
		    await _alertService.DisplayAlert("Registration Successful", "You can now log in", "OK");
	    }

	    public ICommand RegisterCommand { get; }
    }
}
