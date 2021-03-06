﻿using System;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MetinGo.Views;
using PCLAppConfig;
using Plugin.Permissions;
using Xamarin.Forms;

namespace MetinGo.Droid
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            global::Xamarin.Forms.Forms.Init(this, bundle);
			Xamarin.FormsGoogleMaps.Init(this, bundle);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;
            LoadApplication(new App());
        }

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var exception =  e.ExceptionObject as Exception;
		}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //public override void OnBackPressed()
        //{
        //    if (Xamarin.Forms.Application.Current.MainPage is MasterDetailPage masterDetail)
        //    {
        //        if (!(masterDetail.Detail is NavigationPage navigationPage && navigationPage.Pages.First() is MapPage))
        //        {
        //            masterDetail.Detail = new NavigationPage(new MapPage());
        //        }
        //        else
        //            base.OnBackPressed();
        //    }
        //    else
        //        base.OnBackPressed();
        //}

    }
}

