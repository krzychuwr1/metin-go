using System;
using System.ComponentModel;
using MetinGo.AppStart;
using MetinGo.Infrastructure.Session;
using MetinGo.Services;
using MetinGo.Views;
using MetinGo.Views.Login;
using Unity;
using Xamarin.Forms;

namespace MetinGo
{
	public partial class App : Application
	{
		public new static App Current => Application.Current as App;
		public IUnityContainer Container { get; }

		public App ()
		{
			InitializeComponent();
			Container = ContainerInitialization.InitializeContainer();
			Container.Resolve<IAppStarter>().Start();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
