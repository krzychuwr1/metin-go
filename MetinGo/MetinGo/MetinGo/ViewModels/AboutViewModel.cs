﻿using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace MetinGo.ViewModels
{
    public class AboutViewModel : ObservableObject
    {
        public AboutViewModel()
        {
            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}