using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MetinGo.Infrastructure.Alerts
{
    public class AlertService : IAlertService
	{
	    public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel) 
			=> await App.Current.MainPage.DisplayAlert(title, message, accept, cancel);

	    public async Task DisplayAlert(string title, string message, string accept)
		    => await App.Current.MainPage.DisplayAlert(title, message, accept);

		public async Task DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
			=> await App.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
	}
}
