using System;
using System.Collections.Generic;
using System.Text;
using MetinGo.ViewModels;
using MetinGo.ViewModels.Login;
using MetinGo.ViewModels.Map;
using Unity;

namespace MetinGo.Infrastructure.ViewModelLocator
{
    public class ViewModelLocator
    {
		public static ItemsViewModel ItemsViewModel => new ItemsViewModel();
	    public static LoginPageViewModel LoginPageViewModel => App.Current.Container.Resolve<LoginPageViewModel>();
	    public static RegistrationPageViewModel RegistrationPageViewModel => App.Current.Container.Resolve<RegistrationPageViewModel>();
        public static MenuPageViewModel MenuPageViewModel => App.Current.Container.Resolve<MenuPageViewModel>();

        public static MapPageViewModel MapPageViewModel => App.Current.Container.Resolve<MapPageViewModel>();
    }
}
