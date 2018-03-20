using System;
using System.Collections.Generic;
using System.Text;
using MetinGo.ViewModels;
using MetinGo.ViewModels.Login;
using Unity;

namespace MetinGo.Infrastructure.ViewModelLocator
{
    public class ViewModelLocator
    {
		public static ItemsViewModel ItemsViewModel => new ItemsViewModel();
	    public static LoginPageViewModel LoginPageViewModel => App.Current.Container.Resolve<LoginPageViewModel>();
	    public static RegistrationPageViewModel RegistrationPageViewModel => App.Current.Container.Resolve<RegistrationPageViewModel>();
	}
}
