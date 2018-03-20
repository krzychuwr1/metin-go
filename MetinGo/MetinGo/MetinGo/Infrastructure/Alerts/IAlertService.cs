using System.Threading.Tasks;

namespace MetinGo.Infrastructure.Alerts
{
	public interface IAlertService
	{
		Task DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
		Task DisplayAlert(string title, string message, string accept);
		Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
	}
}